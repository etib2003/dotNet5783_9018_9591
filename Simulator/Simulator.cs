using DocumentFormat.OpenXml.Bibliography;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
namespace Simulator;

public static class Simulator
{
    private static readonly BlApi.IBl? bl = BlApi.Factory.Get();
    private static volatile bool _shouldStop = false;
    private static readonly Random random = new Random();
    private const int SEC = 1000;
    private static int delay = 0;
    private static bool finishAll = false;

    private static event Action s_stopSimulator;

    public static event Action s_StopSimulator
    {
        add => s_stopSimulator += value;
        remove => s_stopSimulator -= value;
    }

    private static event EventHandler? s_report;

    public static event EventHandler? s_Report
    {
        add => s_report += value;
        remove => s_report -= value;
    }

    public static void stopSim()
    {
        _shouldStop = true;
        s_stopSimulator();
    }

    public static void simulatorActivate()
    {
        new Thread(()=>
        {
            try
            {
                _shouldStop = false;
                while (!_shouldStop)
                {
                    int? orderId = bl?.Order.GetOldestOrder();
                    if (orderId != null)
                    {
                        BO.Order order = bl?.Order.GetOrderDetails((int)orderId)!;
                        delay = random.Next(3, 11);
                        s_report!(Thread.CurrentThread, new ReportArgs(delay, order));
                        Thread.Sleep(delay * 1000);
                        if (order.ShipDate == null)
                            bl?.Order.UpdateOrderShip((int)orderId);
                        else
                            bl?.Order.UpdateOrderDelivery((int)orderId);
                        if (!_shouldStop)
                            s_report(Thread.CurrentThread, new ReportArgs("Finish order progress"));
                    }
                    else
                    {
                        _shouldStop = true;
                        finishAll = true;
                    }
                    Thread.Sleep(SEC);
                }
                if (_shouldStop && finishAll)
                    s_report!(Thread.CurrentThread, new ReportArgs("Finish simulation"));
            }
            catch (Exception)
            {
                //s_report(Thread.CurrentThread, new ReportArgs("Finish simulation"));
            }
        }).Start();
       
    }
}