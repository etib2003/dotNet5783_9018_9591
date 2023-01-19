using DocumentFormat.OpenXml.Bibliography;
using System;
using System.ComponentModel;
using System.Threading;
namespace Simulator;

public static class Simulator
{
    static readonly BlApi.IBl? bl = BlApi.Factory.Get();
    private static volatile bool _shouldStop = true;
     //התכונות בעיקרון אמורות להיות פרטיות..
    public static event EventHandler? Report;
    static readonly Random random = new Random();
    private const int SEC = 1000;
    private static int delay = 0;

    public static void stopSim() //הוספתי בזמן השיעור, צריך להפעיל מתוך פיאל כנראה
    {
        _shouldStop = false;
    }

    public static void simulatorActivate()
    {
        new Thread(()=>
        {
            try
            {
                while (_shouldStop)
                {

                    //הפונקציה מחזירה את האחרונה שלא שולחה ואם כולן שולחו אז את האחרונה שלא סופקה. ובסוף זה יהיה נאל
                    int? orderId = bl?.Order.GetOldestOrder();//לא בטוח שלא יהיה Null 
                    if (orderId != null)
                    {
                        BO.Order order = bl?.Order.GetOrderDetails((int)orderId);
                        delay = random.Next(3, 11);
                        Report(Thread.CurrentThread, new ReportArgs(delay, order));
                        Thread.Sleep(delay * 1000);
                        if (order.ShipDate == null)
                            bl?.Order.UpdateOrderShip((int)orderId);
                        else
                            bl?.Order.UpdateOrderDelivery((int)orderId);
                        Report(Thread.CurrentThread, new ReportArgs("Finish order progress"));
                    }
                    Thread.Sleep(SEC);
                }
            }
            catch (Exception ex)
            {
                Report(Thread.CurrentThread, new ReportArgs("Finish simulation"));
            }
        }).Start();
       
    }
}