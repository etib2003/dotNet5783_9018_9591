
using Dal;
using DO;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static DO.Enums;

namespace DalTest
{
    public class Program
    {
        static void Main()
        {
            //for the switch loop
            string Choice;
            int Action;
            bool Flag = true;

            Console.WriteLine("Hello , we are happy to have you in our store :) ");

            do
            {
                Console.WriteLine("Choose the Topic - Order / Product / OrderItem / Finish:");
                Choice = Console.ReadLine();
                try
                {
                    switch (Choice)
                    {
                        case "Order":
                            ChoiceOrder();
                            break;
                        case "Product":
                            ChoiceProduct();
                            break;
                        case "OrderItem":
                            ChoiceOrderItem();
                            break;
                        case "Finish":
                            Console.Write("Thank you and have a nice day :) ");
                            Flag = false;
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (Flag);

            //in case the user chose order
            void ChoiceOrder()
            {
                bool FlagOrder = true;
                DalOrder dalOrderObj = new DalOrder();
                do
                {
                    Console.WriteLine(
@"    For add a new order, press: 1 
    For request all the orders, press: 2
    For request an order by ID, press: 3
    For update an order, press: 4
    For delete an order, press: 5
    For exit from Order topic, press: 6");

                    int.TryParse(Console.ReadLine(), out Action);
                    try
                    {
                        switch (Action)
                        {
                            case 1://add a new order
                                {
                                    Order NewOrder = new Order();

                                    Console.Write("Enter a Full name: ");
                                    NewOrder.CustomerName = Console.ReadLine();
                                    Console.Write("Enter an Email: ");
                                    NewOrder.CustomerEmail = Console.ReadLine();
                                    Console.Write("Enter an Adress: ");
                                    NewOrder.CustomerAdress = Console.ReadLine();


                                    Console.WriteLine("enter order's date in a dd.mm.yy format:");
                                    string strDateTime;
                                    DateTime tmpDateTime;

                                    strDateTime = Console.ReadLine();
                                    if (strDateTime.Equals(""))
                                    {
                                        DateTime.TryParse(strDateTime, out tmpDateTime);
                                        NewOrder.OrderDate = tmpDateTime;
                                    }

                                    Console.WriteLine("enter order's ship date in a dd.mm.yy format:");
                                    strDateTime = Console.ReadLine();
                                    if (strDateTime.Equals(""))
                                    {
                                        DateTime.TryParse(strDateTime, out tmpDateTime);
                                        NewOrder.ShipDate = tmpDateTime;
                                    }

                                    Console.WriteLine("enter order's delivary date in a dd.mm.yy format:");
                                    strDateTime = Console.ReadLine();
                                    if (strDateTime.Equals(""))
                                    {
                                        DateTime.TryParse(strDateTime, out tmpDateTime);
                                        NewOrder.DeliveryDate = tmpDateTime;
                                    }

                                    NewOrder.OrderDate = DateTime.Now;
                                    NewOrder.ShipDate = NewOrder.OrderDate.AddDays(2);
                                    NewOrder.DeliveryDate = NewOrder.ShipDate.AddDays(7);

                                    Console.Write(" The order's seqNum is: ");
                                    Console.WriteLine(dalOrderObj.Create(NewOrder));

                                    break;
                                }
                            case 2://return the orders
                                {
                                    foreach (Order ord in dalOrderObj.RequestAll())
                                    {
                                        Console.Write(ord);
                                    }

                                    break;
                                }
                            case 3://return the order that matches the id
                                {
                                    Console.Write("Enter the order's seqNum: ");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine(dalOrderObj.RequestById(id));
                                    break;
                                }
                            case 4://update an order
                                {
                                    Console.Write("Enter a seqNum: ");
                                    int seqNum_; int.TryParse(Console.ReadLine(), out seqNum_);

                                    Console.WriteLine(dalOrderObj.RequestById(seqNum_));

                                    Order updatedOrder = new Order();

                                    Console.Write("Enter a seqNum: ");
                                    updatedOrder.seqNum = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter a Full name: ");
                                    updatedOrder.CustomerName = Console.ReadLine();
                                    Console.Write("Enter an Email: ");
                                    updatedOrder.CustomerEmail = Console.ReadLine();
                                    Console.Write("Enter an Adress: ");
                                    updatedOrder.CustomerAdress = Console.ReadLine();                                  
                                    updatedOrder.OrderDate = DateTime.Now;
                                    updatedOrder.ShipDate = updatedOrder.OrderDate.AddDays(2);
                                    updatedOrder.DeliveryDate = updatedOrder.ShipDate.AddDays(7);

                                    dalOrderObj.Update(updatedOrder);
                                    break;
                                }
                            case 5://delete an order
                                {
                                    Console.Write("Enter the order's seqNum: ");
                                    int id = Convert.ToInt32(Console.ReadLine());

                                    dalOrderObj.Delete(id);
                                    break;
                                }
                            case 6:
                                FlagOrder = false;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                    }

                } while (FlagOrder);
            }

            //in case the user chose product
            void ChoiceProduct()
            {
                bool flagProduct = true;

                DalProduct dalProduct = new DalProduct();
                do
                {
                    Console.WriteLine(
@"    For add a new Product, press: 1 
    For request all the Products, press: 2
    For request a Product by ID, press: 3
    For update a Product, press: 4
    For delete a Product, press: 5
    For exit from Product topic, press: 6");
                    Action = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        switch (Action)
                        {

                            case 1://adds a product
                                {
                                    Product newProduct = new Product();
                                    Console.Write("Enter a barcode: ");
                                    newProduct.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter a Category: 1- Percussions , 2- StringInstrument , 3- WindInstrument , 4- KeyBoard , 5- BowInstrument: ");
                                    category ctgr = new category();
                                    string input = Console.ReadLine();
                                    ctgr = (category)Enum.Parse(typeof(category), input);
                                    newProduct.Category = ctgr;
                                    Console.Write("Enter a Product name: ");
                                    newProduct.Name = Console.ReadLine();
                                    Console.Write("Enter a Price: ");
                                    newProduct.Price = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter an Amount: ");
                                    newProduct.InStock = Convert.ToInt32(Console.ReadLine());
                                    Console.Write(" The prodact's barcode is: ");
                                    Console.WriteLine(dalProduct.Create(newProduct));

                                    break;
                                }
                            case 2://return the products
                                {
                                    foreach (Product pdct in dalProduct.RequestAll())
                                    {
                                        Console.Write(pdct);
                                    }
                                    break;
                                }
                            case 3://return the product that matches the given barcode
                                {
                                    Console.Write("Enter the product's barcode: ");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine(dalProduct.RequestById(id));
                                    break;
                                }
                            case 4://update a product
                                {
                                    Product newProduct = new Product();

                                    Console.Write("Enter a barcode: ");
                                    newProduct.ID = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter a Category: 1- Percussions , 2- StringInstrument , 3- WindInstrument , 4- KeyBoard , 5- BowInstrument: ");
                                    category ctgr = new category();
                                    string input = Console.ReadLine();
                                    ctgr = (category)Enum.Parse(typeof(category), input);
                                    newProduct.Category = ctgr;
                                    Console.Write("Enter a Product name: ");
                                    newProduct.Name = Console.ReadLine();
                                    Console.Write("Enter a Price: ");
                                    newProduct.Price = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter an Amount: ");
                                    newProduct.InStock = Convert.ToInt32(Console.ReadLine());
                                    dalProduct.Update(newProduct);
                                    break;
                                }
                            case 5://delete a product
                                {
                                    Console.Write("Enter the product's barcode: ");
                                    int id = Convert.ToInt32(Console.ReadLine());

                                    dalProduct.Delete(id);
                                    break;
                                }
                            case 6:
                                flagProduct = false;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                } while (flagProduct);
            }

            //in case the user chose orderItem
            void ChoiceOrderItem() 
            {
                bool flagOrderItem = true;
                DalOrderItem DalOrderItem = new DalOrderItem();
                do
                {
                    Console.WriteLine(
@"    For add a new Order Item, press: 1 
    For request all the Order Items, press: 2
    For request an Order Item by seqNum, press: 3
    For request an Order Item by order ID and product ID, press: 4
    For request an Order Item by order ID, press: 5
    For update an Order Item, press: 6
    For delete an Order Item, press: 7
    For exit from Order Item topic, press: 8");
                    Action = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        switch (Action)
                        {
                            case 1://adds an orderItem
                                {
                                    OrderItem newOrderItem = new OrderItem();
                                    Console.Write("Enter the order's seqNum: ");
                                    newOrderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the product's barcode: ");
                                    newOrderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the amount: ");
                                    newOrderItem.Amount = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter a Price: ");
                                    newOrderItem.Price = Convert.ToDouble(Console.ReadLine());
                                    Console.Write(" The orderItem's seqNum is: ");
                                    Console.WriteLine(DalOrderItem.Create(newOrderItem));

                                    break;
                                }
                            case 2://returns all the orderItems
                                {
                                    foreach (OrderItem OI in DalOrderItem.RequestAll())
                                    {
                                        Console.Write(OI);
                                    }

                                    break;
                                }
                            case 3://returns the orderItem that matches the given seqNum
                                {
                                    Console.Write("Enter the orderItem's seqNum: ");
                                    int seqNum = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine(DalOrderItem.RequestBySeqNum(seqNum));
                                    break;
                                }
                            case 4://returns the orderItem that matches the given order's seqNum and  the product's barcode
                                {
                                    Console.Write("Enter the order's seqNum: ");
                                    int O_ID = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the product's barcode: ");
                                    int P_ID = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine(DalOrderItem.RequestByOrderIDProductID(O_ID, P_ID));
                                    break;
                                }
                            case 5://returns the orderItem that matches the given order's seqNum
                                {
                                    Console.Write("Enter the order's seqNum: ");
                                    int O_ID = Convert.ToInt32(Console.ReadLine());
                                    foreach (OrderItem OI in DalOrderItem.RequestByOrderId(O_ID))
                                    {
                                        Console.Write(OI);
                                    }
                                    break;
                                }
                            case 6://update an orderItem
                                {

                                    OrderItem updatedOrderItem = new OrderItem();

                                    Console.Write("Enter a seqNum: ");
                                    updatedOrderItem.seqNum = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the order's seqNum: ");
                                    updatedOrderItem.OrderID = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the product's barcode: ");
                                    updatedOrderItem.ProductID = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter the amount: ");
                                    updatedOrderItem.Amount = Convert.ToInt32(Console.ReadLine());
                                    Console.Write("Enter a Price: ");
                                    updatedOrderItem.Price = Convert.ToDouble(Console.ReadLine());

                                    DalOrderItem.Update(updatedOrderItem);

                                    break;
                                }
                            case 7://delete an orderItem
                                {
                                    Console.Write("Enter the orderItem's seqNum: ");

                                    int seqNum = Convert.ToInt32(Console.ReadLine());
                                    DalOrderItem.Delete(seqNum);
                                    break;
                                }
                            case 8:
                                flagOrderItem = false;
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                } while (flagOrderItem);
            }
        }
    }

}





 


