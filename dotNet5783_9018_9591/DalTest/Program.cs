
// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static DO.Enums;

string choice;
int action;
bool flag = true;

do
{
    Console.WriteLine("Enter the Topic - finish / order / product / orderItem:");
    choice = Console.ReadLine();
    switch (choice)
    {
        case "finish":
            flag = false;
            break;
        case "order":
            {
                Console.WriteLine(
@"    For add a new order, press: 1 
    For request all the orders, press: 2
    For request an order by ID, press: 3
    For update an order, press: 4
    For delete an order, press: 5");
                action = Convert.ToInt32(Console.ReadLine());
                ChoiceOrder(action);
            }
            break;

        case "product":
            {
                Console.WriteLine(
@"    For add a new Product, press: 1 
    For request all the Products, press: 2
    For request a Product by ID, press: 3
    For update a Product, press: 4
    For delete a Product, press: 5");
                action = Convert.ToInt32(Console.ReadLine());
                ChoiceProduct(action);
            }
            break;

        case "orderItem":
            {
                Console.WriteLine(
@"    For add a new Order Item, press: 1 
    For request all the Order Items, press: 2
    For request an Order Item by seqNum, press: 3
    For request an Order Item by order ID and product ID, press: 4
    For request an Order Item by order ID, press: 5
    For update an Order Item, press: 6
    For delete an Order Item, press: 7");
                action = Convert.ToInt32(Console.ReadLine());
                ChoiceOrderItem(action);
            }
            break;
        default:
            Console.WriteLine("ERROR");
            break;
    }
} while (flag);

void ChoiceOrder(int action)
{
    DalOrder dalOrder = new DalOrder();
    switch (action)
    {
        case 1:
            {
                Order newOrder = new Order();

                Console.Write("Enter a Full name:");
                newOrder.CustomerName = Console.ReadLine();
                Console.Write("Enter an Email:");
                newOrder.CustomerEmail = Console.ReadLine();
                Console.Write("Enter an Adress:");
                newOrder.CustomerAdress = Console.ReadLine();
                newOrder.OrderDate = DateTime.Now;
                newOrder.ShipDate = newOrder.OrderDate.AddDays(2);
                newOrder.DeliveryDate = newOrder.ShipDate.AddDays(7);

                dalOrder.Create(newOrder);
                break;
            }
        case 2:
            {
                foreach(Order ord in dalOrder.RequestAll())
                {
                    Console.WriteLine(ord);
                }
                //Console.WriteLine(dalOrder.RequestAll());

                break;
            }
        case 3:
            {
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine(dalOrder.RequestById(id));
                break;
            }
        case 4:
            {
                Order newOrder = new Order();

                Console.Write("Enter a Full name:");
                newOrder.CustomerName = Console.ReadLine();
                Console.Write("Enter an Email:");
                newOrder.CustomerEmail = Console.ReadLine();
                Console.Write("Enter an Adress:");
                newOrder.CustomerAdress = Console.ReadLine();
                newOrder.OrderDate = DateTime.Now;
                newOrder.ShipDate = newOrder.OrderDate.AddDays(2);
                newOrder.DeliveryDate = newOrder.ShipDate.AddDays(7);

                dalOrder.Update(newOrder);
                break;
            }
        case 5:
            {
                Console.Write("Enter an ID:");
                int id = Convert.ToInt32(Console.ReadLine());

                dalOrder.Delete(id);
                break;
            }

        default:
            break;
    }
}

void ChoiceProduct(int action)
{
    DalProduct dalProduct = new DalProduct();

    switch (action)
    {
        case 1:
            {
                Product newProduct = new Product();

                Console.Write("Enter an ID:");
                newProduct.ID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter a Product name:");
                newProduct.Name = Console.ReadLine();
                Console.Write("Enter a Price:");
                newProduct.Price = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter a Category:");
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
                Console.Write("Enter an Amount:");
                newProduct.InStock = Convert.ToInt32(Console.ReadLine());

                dalProduct.Create(newProduct);
                break;
            }
        case 2:
            {
                Console.WriteLine(dalProduct.RequestAll()); //מדפיס את כל הרשימה
                break;
            }
        case 3:
            {
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(dalProduct.RequestById(id));//מדפיס את כל המוצר
                break;
            }
        case 4:
            {
                Product newProduct = new Product();

                Console.Write("Enter an ID:");
                newProduct.ID = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter a Product name:");
                newProduct.Name = Console.ReadLine();
                Console.Write("Enter a Price:");
                newProduct.Price = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter a Category:");
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
                Console.Write("Enter an Amount:");
                newProduct.InStock = Convert.ToInt32(Console.ReadLine());

                dalProduct.Update(newProduct);
                break;
            }
        case 5:
            {
                Console.Write("Enter an ID:");
                int id = Convert.ToInt32(Console.ReadLine());

                dalProduct.Delete(id);
                break;
            }
        default:
            break;
    }
}
void ChoiceOrderItem(int action)
{
    DalOrderItem dalOrderItem = new DalOrderItem();

    switch (action)
    {
        case 1:
            {
                OrderItem newOrderItem = new OrderItem();
                newOrderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                newOrderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                newOrderItem.Amount = Convert.ToInt32(Console.ReadLine());
                newOrderItem.Price = Convert.ToDouble(Console.ReadLine());

                dalOrderItem.Create(newOrderItem);

                break;
            }
        case 2:
            {
                Console.WriteLine(dalOrderItem.RequestAll());

                break;
            }
        case 3:
            {
                int num = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(dalOrderItem.RequestBySeqNum(num));
                break;
            }
        case 4:
            {
                int P_ID = Convert.ToInt32(Console.ReadLine());
                int O_ID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(dalOrderItem.RequestByOrderIDProductID(O_ID, P_ID));
                break;
            }
        case 5:
            {
                int O_ID = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(dalOrderItem.RequestByOrderId(O_ID));
                break;
            }
        case 6:
            {
                OrderItem newOrderItem = new OrderItem();

                newOrderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                newOrderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                newOrderItem.Amount = Convert.ToInt32(Console.ReadLine());
                newOrderItem.Price = Convert.ToDouble(Console.ReadLine());
                dalOrderItem.Update(newOrderItem);

                break;
            }
        case 7:
            {
                int id = Convert.ToInt32(Console.ReadLine());
                dalOrderItem.Delete(id);
                break;
            }
        default:
            break;
    }
}




