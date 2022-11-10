using Dal;
using DO;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static DO.Enums;

string Choice;
int Action;
bool Flag = true;
////////////להפוך שמות משתנים לאותיות גדולות!!!
/*חובה לפרמט את הקוד בהתאם - הזחות, שורות רווח, ריווח בתוך השורות
כל השמות (של המחלקות, השדות, התכונות, המתודות) חייבות להיות באנגלית ובעלי משמעות בהתאם לתפקיד המחלקה, המבנה, התכונה, המתודה
שמות כל הסוגים, כמו כן השדות, התכונות, והמתודות עם הרשאה public יש להגדיר בפורמט PascalCase
שמות כל השדות והתכונות בהרשאה private או  internal יש להגדיר בפורמט camelCase_
שמות כל המתודות בהרשאה private או  internal, הפרמטרים, והמשתנים המקומיים יש להגדיר בפורמט camelCase 
חובה לתעד את כל הסוגים, המתודות, והתכונות בעזרת תיעוד מפורמט (///)
חובה לתעד את הקוד אם איננו ברור מאליו ממבנה הקוד (למשל במקרה של חישובים מתמטיים שאינם בסיסיים, אלגוריתם שאיננו ברמה של "פשיטא ליה

Kטפל בעדכוניייים

 * 
 * */
do
{
    Console.WriteLine("Enter the Topic - order / product / orderItem / finish: ");
    Choice = Console.ReadLine();
    switch (Choice)
    {
        case "order":
                ChoiceOrder();
            break;

        case "product":
                ChoiceProduct();
        
            break;

        case "orderItem":  
                ChoiceOrderItem();
       
            break;
        case "finish":
            Console.Write("Thank you and have a nice day :) ");

            Flag = false;
            break;
        default:
            Console.WriteLine("ERROR");
            break;
    }
} while (Flag);

void ChoiceOrder()
{
    bool FlagOrder = true;
    DalOrder DalOrder_ = new DalOrder();
    do
    {
        Console.WriteLine(
@"    For add a new order, press: 1 
    For request all the orders, press: 2
    For request an order by ID, press: 3
    For update an order, press: 4
    For delete an order, press: 5
    For exit from Order topic, press: 6");
        //Action = Convert.ToInt32(Console.ReadLine());
        int.TryParse(Console.ReadLine(), out Action);
        switch (Action)
        {
            case 1:
                {
                    Order NewOrder = new Order();

                    Console.Write("Enter a Full name: ");
                    NewOrder.CustomerName = Console.ReadLine();
                    Console.Write("Enter an Email: ");
                    NewOrder.CustomerEmail = Console.ReadLine();
                    Console.Write("Enter an Adress: ");
                    NewOrder.CustomerAdress = Console.ReadLine();
                    NewOrder.OrderDate = DateTime.Now;
                    NewOrder.ShipDate = NewOrder.OrderDate.AddDays(2);
                    NewOrder.DeliveryDate = NewOrder.ShipDate.AddDays(7);

                    DalOrder_.Create(NewOrder);
                    break;
                }
            case 2:
                {
                    foreach (Order ord in DalOrder_.RequestAll())
                    {
                        Console.Write(ord);
                    }

                    break;
                }
            case 3:
                {
                    Console.Write("Enter the order's seqNum: ");
                    int id = Convert.ToInt32(Console.ReadLine());


                    Console.WriteLine(DalOrder_.RequestById(id));
                    break;
                }
            case 4:
                {
                    Console.Write("Enter a seqNum: ");
                    int seqNum_; int.TryParse(Console.ReadLine(), out seqNum_);

                    Console.WriteLine(DalOrder_.RequestById(seqNum_));

                    Order updatedOrder = new Order();

                    Console.Write("Enter a seqNum: ");
                    updatedOrder.seqNum= Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter a Full name: ");
                    updatedOrder.CustomerName = Console.ReadLine();
                    Console.Write("Enter an Email: ");
                    updatedOrder.CustomerEmail = Console.ReadLine();
                    Console.Write("Enter an Adress: ");
                    updatedOrder.CustomerAdress = Console.ReadLine();
                    //DateTime.TryParse(Console.ReadLine(), out updatedOrder.OrderDate);
                    updatedOrder.OrderDate = DateTime.Now;
                    updatedOrder.ShipDate = updatedOrder.OrderDate.AddDays(2);
                    updatedOrder.DeliveryDate = updatedOrder.ShipDate.AddDays(7);

                    DalOrder_.Update(updatedOrder);
                    break;
                }
            case 5:
                {
                    Console.Write("Enter the order's seqNum: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    DalOrder_.Delete(id);
                    break;
                }
            case 6:
                FlagOrder = false;
                break;
            default:
                break;
        }
    } while (FlagOrder);
}

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
        switch (Action)
        {

            case 1:
                {
                    Product newProduct = new Product();

                    Console.Write("Enter a barcode: ");
                    newProduct.ID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter a Product name: ");
                    newProduct.Name = Console.ReadLine();
                    Console.Write("Enter a Price: ");
                    newProduct.Price = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter a Category: 1- Percussions , 2- StringInstrument , 3- WindInstrument , 4- KeyBoard , 5- BowInstrument: ");
                    int ctgr;
                    ctgr = Convert.ToInt32(Console.ReadLine());
                    switch (ctgr)
                    {
                        case 1:
                            {
                                newProduct.Category = category.Percussions;
                                break;
                            }
                        case 2:
                            {
                                newProduct.Category = category.StringInstrument;
                                break;
                            }
                        case 3:
                            {
                                newProduct.Category = category.WindInstrument;
                                break;
                            }
                        case 4:
                            {
                                newProduct.Category = category.KeyBoard;

                                break;
                            }
                        case 5:
                            {
                                newProduct.Category = category.BowInstrument;
                                break;
                            }
                        default:

                            break;
                    }
                    Console.Write("Enter an Amount: ");
                    newProduct.InStock = Convert.ToInt32(Console.ReadLine());

                    dalProduct.Create(newProduct);
                    break;
                }
            case 2:
                {
                    foreach (Product pdct in dalProduct.RequestAll())
                    {
                        Console.Write(pdct);
                    }
                    break;
                }
            case 3:
                {
                    Console.Write("Enter the product's barcode: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(dalProduct.RequestById(id));
                    break;
                }
            case 4:
                {
                    Product newProduct = new Product();

                    Console.Write("Enter a barcode: ");
                    newProduct.ID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter a Product name: ");
                    newProduct.Name = Console.ReadLine();
                    Console.Write("Enter a Price: ");
                    newProduct.Price = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter a Category: 1- Percussions , 2- StringInstrument , 3- WindInstrument , 4- KeyBoard , 5- BowInstrument: ");
                    int ctgr;
                    ctgr = Convert.ToInt32(Console.ReadLine());
                    switch (ctgr)
                    {
                        case 1:
                            {
                                newProduct.Category = category.Percussions;
                                break;
                            }
                        case 2:
                            {
                                newProduct.Category = category.StringInstrument;
                                break;
                            }
                        case 3:
                            {
                                newProduct.Category = category.WindInstrument;
                                break;
                            }
                        case 4:
                            {
                                newProduct.Category = category.KeyBoard;
                                break;
                            }
                        case 5:
                            {
                                newProduct.Category = category.BowInstrument;
                                break;
                            }
                        default:

                            break;
                    }
                    Console.Write("Enter an Amount: ");
                    newProduct.InStock = Convert.ToInt32(Console.ReadLine());

                    dalProduct.Update(newProduct);
                    break;
                }
            case 5:
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
    } while (flagProduct);
}

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
        switch (Action)
        {
            case 1:
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

                    DalOrderItem.Create(newOrderItem);

                    break;
                }
            case 2:
                {
                    foreach (OrderItem OI in DalOrderItem.RequestAll())
                    {
                        Console.Write(OI);
                    }

                    break;
                }
            case 3:
                {
                    Console.Write("Enter the orderItem's seqNum: ");
                    int seqNum = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(DalOrderItem.RequestBySeqNum(seqNum));
                    break;
                }
            case 4:
                {
                    Console.Write("Enter the order's seqNum: ");
                    int O_ID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter the product's barcode: ");
                    int P_ID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(DalOrderItem.RequestByOrderIDProductID(O_ID, P_ID));
                    break;
                }
            case 5:
                {
                    Console.Write("Enter the order's seqNum: ");
                    int O_ID = Convert.ToInt32(Console.ReadLine());
                    foreach (OrderItem OI in DalOrderItem.RequestByOrderId(O_ID))
                    {
                        Console.Write(OI);
                    }
                    break;
                }
            case 6:
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
            case 7:
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
    } while (flagOrderItem);
}




