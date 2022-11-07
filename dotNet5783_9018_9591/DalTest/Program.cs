// See https://aka.ms/new-console-template for more information
using DO;
using System;

string choice;
string ch;
do
{ choice = Console.ReadLine();
    switch (choice)
    {
        case "finish":
            break;
        case "Order":
            {
                ch = Console.ReadLine();
                switch (ch)
                {
                    case "a"://להוסיף
                        {
                            Order newOrder = new Order();
                            //newOrder.GetHashCode();
                            newOrder.CustomerName = Console.ReadLine();
                            newOrder.CustomerEmail = Console.ReadLine();
                            newOrder.CustomerAdress = Console.ReadLine();
                            newOrder.OrderDate=DateTime.Now;
                            newOrder.ShipDate = newOrder.OrderDate.AddDays(2);
                            newOrder.DeliveryDate = newOrder.ShipDate.AddDays(7);
                            break;      
                        }
                    case "b":
                    {

                        break;
                    }

                }
                
            }
        case "2":
            {
                ch = Console.ReadLine();
                switch (ch)
                {
                    case "a":
                        {
                            break;
                        }

                }
                break;
            }
        case "3":
            {
                ch = Console.ReadLine();
                switch (ch)
                {
                    case "a":
                        {
                            break;
                        }

                }
                break;
            }
    }
}
while (choice != "0");
