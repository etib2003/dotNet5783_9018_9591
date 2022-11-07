
// See https://aka.ms/new-console-template for more information
using Dal;
using DO;
using System;
using System.Runtime.InteropServices;

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




 