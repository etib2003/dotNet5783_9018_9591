
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
                dalOrder.RequestAll();


                break;
            }
        case "request By Id":
            {
                int id = Convert.ToInt32(Console.ReadLine());
                break;
            }
        case "update":
            {
                break;
            }
        case "delete":
            {
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
                break;
            }
        case "request All":
            {
                break;
            }
        case "request By Id":
            {
                break;
            }
        case "update":
            {
                break;
            }
        case "delete":
            {
                break;
            }

        default:
            break;
    }
}




 