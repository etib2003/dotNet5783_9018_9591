
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
        private static DalOrder dalOrderObj = new DalOrder();
        private static DalProduct dalProductObj = new DalProduct();
        private static DalOrderItem dalOrderItemObj = new DalOrderItem();

       
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
                                    int id; int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(dalOrderObj.RequestById(id));
                                    break;
                                }
                            case 4://update an order
                                {
                                    Console.Write("Enter a seqNum: ");
                                    int seqNum_; int.TryParse(Console.ReadLine(), out seqNum_);

                                    Console.WriteLine(dalOrderObj.RequestById(seqNum_));

                                    Order updatedOrder = new Order();

                                    updatedOrder.seqNum= seqNum_;
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
                                    int id; int.TryParse(Console.ReadLine(), out id);

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

                do
                {
                    Console.WriteLine(
@"    For add a new Product, press: 1 
    For request all the Products, press: 2
    For request a Product by ID, press: 3
    For update a Product, press: 4
    For delete a Product, press: 5
    For exit from Product topic, press: 6");
                    int Action; int.TryParse(Console.ReadLine(), out Action);
                    try
                    {
                        switch (Action)
                        {

                            case 1://adds a product
                                {
                                    Product newProduct = new Product();
                                    Console.Write("Enter a barcode: ");
                                    int ID; int.TryParse(Console.ReadLine(), out ID);
                                    newProduct.ID=ID;
                                    Console.Write("Enter a Category: 1- Percussions , 2- StringInstrument , 3- WindInstrument , 4- KeyBoard , 5- BowInstrument: ");
                                    category ctgr = new category();
                                    string input = Console.ReadLine();
                                    ctgr = (category)Enum.Parse(typeof(category), input);
                                    newProduct.Category = ctgr;
                                    Console.Write("Enter a Product name: ");
                                    newProduct.Name = Console.ReadLine();
                                    Console.Write("Enter a Color: 1- Black , 2- Red , 3- White , 4- Brown : ");
                                    color c = new color();
                                    string str = Console.ReadLine();
                                    c  = (color)Enum.Parse(typeof(color), str);
                                    newProduct.Color = c;
                                    Console.Write("Enter a Price: ");
                                    int price; int.TryParse(Console.ReadLine(), out price);
                                    newProduct.Price= price;
                                    Console.Write("Enter an Amount: ");
                                    int InStock; int.TryParse(Console.ReadLine(), out InStock);
                                    newProduct.InStock= InStock;
                                    Console.Write(" The prodact's barcode is: ");
                                    Console.WriteLine(dalProductObj.Create(newProduct));

                                    break;
                                }
                            case 2://return the products
                                {
                                    foreach (Product pdct in dalProductObj.RequestAll())
                                    {
                                        Console.Write(pdct);
                                    }
                                    break;
                                }
                            case 3://return the product that matches the given barcode
                                {
                                    Console.Write("Enter the product's barcode: ");
                                    int id; int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(dalProductObj.RequestById(id));
                                    break;
                                }
                            case 4://update a product
                                {


                                    Console.Write("Enter a barcode: ");
                                    int barcode; int.TryParse(Console.ReadLine(), out barcode);
                                    Console.WriteLine(dalProductObj.RequestById(barcode));

                                    Product newProduct = new Product();
                                    newProduct.ID = barcode;
                                    Console.Write("Enter a Category: 1- Percussions , 2- StringInstrument , 3- WindInstrument , 4- KeyBoard , 5- BowInstrument: ");
                                    category ctgr = new category();
                                    string input = Console.ReadLine();
                                    ctgr = (category)Enum.Parse(typeof(category), input);
                                    newProduct.Category = ctgr;
                                    Console.Write("Enter a Product name: ");
                                    newProduct.Name = Console.ReadLine();
                                    Console.Write("Enter a Color: 1- Black , 2- Red , 3- White , 4- Brown : ");
                                    color c = new color();
                                    string str = Console.ReadLine();
                                    c = (color)Enum.Parse(typeof(color), str);
                                    newProduct.Color = c;
                                    Console.Write("Enter a Price: ");
                                    int Price; int.TryParse(Console.ReadLine(), out Price);
                                    newProduct.Price= Price;
                                    Console.Write("Enter an Amount: ");
                                    int InStock; int.TryParse(Console.ReadLine(), out InStock);
                                    newProduct.InStock= InStock;
                                    dalProductObj.Update(newProduct);
                                    break;
                                }
                            case 5://delete a product
                                {
                                    Console.Write("Enter the product's barcode: ");
                                    int id; int.TryParse(Console.ReadLine(), out id);

                                    dalProductObj.Delete(id);
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
                    int Action; int.TryParse(Console.ReadLine(), out Action);
                    try
                    {
                        switch (Action)
                        {
                            case 1://adds an orderItem
                                {
                                    OrderItem newOrderItem = new OrderItem();
                                    Console.Write("Enter the order's seqNum: ");
                                    int OrderID; int.TryParse(Console.ReadLine(), out OrderID);
                                    newOrderItem.OrderID= OrderID;
                                    Console.Write("Enter the product's barcode: ");
                                    int ProductID; int.TryParse(Console.ReadLine(), out ProductID);
                                    newOrderItem.ProductID= ProductID;
                                    Console.Write("Enter a Price: ");
                                    Double Price; double.TryParse(Console.ReadLine(), out Price);
                                    newOrderItem.Price= Price;
                                     Console.Write("Enter the amount: ");
                                    int Amount; int.TryParse(Console.ReadLine(), out Amount);
                                    newOrderItem.Amount= Amount;
                                    
                                    Console.Write(" The orderItem's seqNum is: ");
                                    Console.WriteLine(dalOrderItemObj.Create(newOrderItem));

                                    break;
                                }
                            case 2://returns all the orderItems
                                {
                                    foreach (OrderItem OI in dalOrderItemObj.RequestAll())
                                    {
                                        Console.Write(OI);
                                    }

                                    break;
                                }
                            case 3://returns the orderItem that matches the given seqNum
                                {
                                    Console.Write("Enter the orderItem's seqNum: ");
                                    int seqNum; int.TryParse(Console.ReadLine(), out seqNum);
                                    Console.WriteLine(dalOrderItemObj.RequestBySeqNum(seqNum));
                                    break;
                                }
                            case 4://returns the orderItem that matches the given order's seqNum and  the product's barcode
                                {
                                    Console.Write("Enter the order's seqNum: ");
                                    int O_ID; int.TryParse(Console.ReadLine(), out O_ID);
                                    Console.Write("Enter the product's barcode: ");
                                    int P_ID; int.TryParse(Console.ReadLine(), out P_ID); Console.WriteLine(dalOrderItemObj.RequestByOrderIDProductID(O_ID, P_ID));
                                    break;
                                }
                            case 5://returns the orderItem that matches the given order's seqNum
                                {
                                    Console.Write("Enter the order's seqNum: ");
                                    int O_ID; int.TryParse(Console.ReadLine(), out O_ID);
                                    foreach (OrderItem OI in dalOrderItemObj.RequestByOrderId(O_ID))
                                    {
                                        Console.Write(OI);
                                    }
                                    break;
                                }
                            case 6://update an orderItem
                                {


                                    Console.Write("Enter a seqNum: ");
                                    int seqNum_; int.TryParse(Console.ReadLine(), out seqNum_);
                                    Console.WriteLine(dalOrderItemObj.RequestBySeqNum(seqNum_));

                                    OrderItem updatedOrderItem = new OrderItem();                                  
                                    Console.Write("Enter the order's seqNum: ");
                                    int OrderID; int.TryParse(Console.ReadLine(), out OrderID);
                                    updatedOrderItem.OrderID = OrderID;
                                    Console.Write("Enter the product's barcode: ");
                                    int ProductID; int.TryParse(Console.ReadLine(), out ProductID);
                                    updatedOrderItem.ProductID = ProductID;
                                    Console.Write("Enter the amount: ");
                                    int Amount; int.TryParse(Console.ReadLine(), out Amount);
                                    updatedOrderItem.Amount = Amount;
                                    Console.Write("Enter a Price: ");
                                    updatedOrderItem.Price = Convert.ToDouble(Console.ReadLine());

                                    dalOrderItemObj.Update(updatedOrderItem);

                                    break;
                                }
                            case 7://delete an orderItem
                                {
                                    Console.Write("Enter the orderItem's seqNum: ");

                                    int seqNum; int.TryParse(Console.ReadLine(), out seqNum);
                                    dalOrderItemObj.Delete(seqNum);
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





 


