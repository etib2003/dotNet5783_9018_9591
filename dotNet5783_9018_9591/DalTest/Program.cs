
// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static DO.Enums;

string choice;
string ch;
 
choice = Console.ReadLine();
ch = Console.ReadLine();
switch (choice)
{
    case "finish":
        break;

    case "Order":
        {

            ChoiceOrder();
            break;
        }
    case "Product":
        {
            ChoiceProduct();
            break;
        }
    case "OrderItem":
        {
            ChoiceOrderItem();
            break;
        }

}
void ChoiceOrder()
{
    switch (ch)
    {
        case "add":
            {
                Order newOrder = new Order() { 
                    CustomerName = Console.ReadLine(),
                    CustomerEmail = Console.ReadLine(),
                    CustomerAdress = Console.ReadLine()};
                 
                newOrder.OrderDate = DateTime.Now;
                newOrder.ShipDate = newOrder.OrderDate.AddDays(2);
                newOrder.DeliveryDate = newOrder.ShipDate.AddDays(7);
                DalOrder dalOrder = new DalOrder();
                dalOrder.Create(newOrder);

                break;
            }
        case "request All":
            {
                DalOrder dalOrder=new DalOrder();
                Console.WriteLine(dalOrder.RequestAll());


                break;
            }
        case "request By Id":
            {
                int id = Convert.ToInt32(Console.ReadLine());
                DalOrder dalOrder = new DalOrder();
                Console.WriteLine(dalOrder.RequestById(id)); 
                break;
            }
        case "update":
            {
                Order newOrder = new Order()
                {
                    CustomerName = Console.ReadLine(),
                    CustomerEmail = Console.ReadLine(),
                    CustomerAdress = Console.ReadLine()
                };

                newOrder.OrderDate = DateTime.Now;
                newOrder.ShipDate = newOrder.OrderDate.AddDays(2);
                newOrder.DeliveryDate = newOrder.ShipDate.AddDays(7);
                DalOrder dalOrder = new DalOrder();
                dalOrder.Update(newOrder);
                break;
            }
        case "delete":
            {
                int id = Convert.ToInt32(Console.ReadLine());
                DalOrder dalOrder = new DalOrder();
                dalOrder.Delete(id);
                break;
            }

        default:
            break;
    }
}
void ChoiceProduct()
{
    switch (ch)
    {
        case "add":
            {
                Product newProduct = new Product();
                newProduct.ID = Convert.ToInt32(Console.ReadLine());
                newProduct.Name = Console.ReadLine();
                newProduct.Price = Convert.ToInt32(Console.ReadLine());
                string ctgr;
                ctgr = Console.ReadLine();
                switch (ctgr)
                {
                    case "Percussions":
                        {
                            newProduct.Category = category.Percussions;
                            break;
                        }
                    case "StringInstrument":
                        {
                            newProduct.Category = category.StringInstrument;
                            break;
                        }
                    case "WindInstrument":
                        {
                            newProduct.Category = category.WindInstrument;
                            break;
                        }
                    case "KeyBoard":
                        {
                            newProduct.Category = category.KeyBoard;

                            break;
                        }
                    case "BowInstrument":
                        {
                            newProduct.Category = category.BowInstrument;
                            break;
                        }
                    default:
                        break;
                }
                newProduct.InStock = Convert.ToInt32(Console.ReadLine());

                DalProduct dalProduct = new DalProduct();
                dalProduct.Create(newProduct);
                break;
            }
        case "request All":
            {
                DalProduct dalProduct = new DalProduct();
                Console.WriteLine(dalProduct.RequestAll()); //מדפיס את כל הרשימה
                break;
            }
        case "request By Id":
            {
                int id = Convert.ToInt32(Console.ReadLine());
                DalProduct dalProduct = new DalProduct();
                Console.WriteLine(dalProduct.RequestById(id));//מדפיס את כל המוצר
                break;
            }
        case "update":
            {
                Product newProduct = new Product();
                newProduct.ID = Convert.ToInt32(Console.ReadLine());
                newProduct.Name = Console.ReadLine();
                newProduct.Price = Convert.ToInt32(Console.ReadLine());
                string ctgr;
                ctgr = Console.ReadLine();
                switch (ctgr)
                {
                    case "Percussions":
                        {
                            newProduct.Category = category.Percussions;
                            break;
                        }
                    case "StringInstrument":
                        {
                            newProduct.Category = category.StringInstrument;
                            break;
                        }
                    case "WindInstrument":
                        {
                            newProduct.Category = category.WindInstrument;
                            break;
                        }
                    case "KeyBoard":
                        {
                            newProduct.Category = category.KeyBoard;

                            break;
                        }
                    case "BowInstrument":
                        {
                            newProduct.Category = category.BowInstrument;
                            break;
                        }
                    default:
                        break;
                }
                newProduct.InStock = Convert.ToInt32(Console.ReadLine());

                DalProduct dalProduct = new DalProduct();
                dalProduct.Update(newProduct);
                break;
            }
        case "delete":
            {
                int id = Convert.ToInt32(Console.ReadLine());
                DalProduct dalProduct = new DalProduct();
                dalProduct.Delete(id);
                break;
            }
        default:
            break;
    }
}
void ChoiceOrderItem()
{
    switch (ch)
    {
        case "add":
            {
                OrderItem newOrderItem = new OrderItem()
                {
                    OrderID = Convert.ToInt32(Console.ReadLine()),
                    ProductID = Convert.ToInt32(Console.ReadLine()),
                    Amount=Convert.ToInt32(Console.ReadLine()),
                    Price=Convert.ToDouble(Console.ReadLine())
            };

                DalOrderItem dalOrderItem = new DalOrderItem();
                dalOrderItem.Create(newOrderItem);

                break;
            }
        case "request All":
            {

                DalOrderItem dalOrderItem = new DalOrderItem();
                Console.WriteLine(dalOrderItem.RequestAll()); 

                break;
            }
        case "RequestBySeqNum":
            {
                int num = Convert.ToInt32(Console.ReadLine());
                DalOrderItem dalOrderItem = new DalOrderItem();
                Console.WriteLine(dalOrderItem.RequestBySeqNum(num)); 
                break;
            }
        case "RequestByOrderIDProductID":
            {
                int P_ID = Convert.ToInt32(Console.ReadLine());
                int O_ID = Convert.ToInt32(Console.ReadLine());
                DalOrderItem dalOrderItem = new DalOrderItem();
                Console.WriteLine(dalOrderItem.RequestByOrderIDProductID(O_ID, P_ID)); 
                break;
            }
        case "request By OrderId":
            {
                int O_ID = Convert.ToInt32(Console.ReadLine());
                DalOrderItem dalOrderItem = new DalOrderItem();
                Console.WriteLine(dalOrderItem.RequestByOrderId(O_ID)); 
                break;
            }
        case "update":
            {
                OrderItem newOrderItem = new OrderItem()
                {
                    OrderID = Convert.ToInt32(Console.ReadLine()),
                    ProductID = Convert.ToInt32(Console.ReadLine()),
                    Amount = Convert.ToInt32(Console.ReadLine()),
                    Price = Convert.ToDouble(Console.ReadLine())
                };

                DalOrderItem dalOrderItem = new DalOrderItem();
                dalOrderItem.Update(newOrderItem);

                break;
            }
        case "delete":
            {
                int id = Convert.ToInt32(Console.ReadLine());
                DalOrderItem dalOrderItem = new DalOrderItem();
                dalOrderItem.Delete(id);
                break;
            }

        default:
            break;
    }
}




 