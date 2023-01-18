using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Threading;
namespace Simulator;

public static class Simulator
{
    static readonly BlApi.IBl? bl = BlApi.Factory.Get();
    private static volatile bool _shouldStop = true;
     //התכונות בעיקרון אמורות להיות פרטיות..
    public delegate void MyEventHandler(object sender, EventArgs e); //
    public static event MyEventHandler report; //
    
    static readonly Random random = new Random();

    public static void stopSim() //הוספתי בזמן השיעור, צריך להפעיל מתוך פיאל כנראה
    {
        _shouldStop = false;
    }
    public static void startSimulator()
    {
        new Thread(simulatorActivate).Start();
    }
    private static void simulatorActivate()
    {
        try
        {
            while (_shouldStop)
            {

                //הפונקציה מחזירה את האחרונה שלא שולחה ואם כולן שולחו אז את האחרונה שלא סופקה. ובסוף זה יהיה נאל
                int? orderId = bl?.Order.GetOldestOrder();//לא בטוח שלא יהיה Null 
                if (orderId != null)
                {
                    ////_shouldStop = false; צריך לחשוב על זה
                    ////break;

                    var order = bl?.Order.GetOrderDetails((int)orderId);
                    int time = random.Next(3, 11);
                    Tuple<int, int?, string, string, DateTime, DateTime> tuple =
                        new(time, orderId, order?.Status.ToString()!, order?.Status.ToString() == "confirmed" ? "shipped" : "provided", DateTime.Now, DateTime.Now.AddSeconds(time));
                    //report(tuple);
                    if (bl?.Order.GetOrderDetails((int)orderId).ShipDate == null)
                        bl?.Order.UpdateOrderShip((int)orderId);
                    else//צריך לעדכן תאריך אספקה
                        bl?.Order.UpdateOrderDelivery((int)orderId);
                    //report("finish order progress");
                }
                Thread.Sleep(1000);
            }
            //report("finish simulation");
        }
        catch (Exception)
        {
            throw new Exception("Error");
        }

    }


}