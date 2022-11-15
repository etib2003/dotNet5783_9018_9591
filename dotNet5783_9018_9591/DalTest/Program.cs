using Dal;
using DalApi;
using DO;
using DalApi;

using static DO.Enums;

namespace DalTest
{
    public class Program
    {
        private static IOrder dalOrderObj = new IOrder();
        private static IProduct dalProductObj = new IProduct();
        private static IOrderItem dalOrderItemObj = new IOrderItem();

        static void Main()
        {
            //for the switch loop
            int Choice;


            Console.WriteLine("Hello , we are happy to have you in our store :) ");

            do
            {
                Console.WriteLine(@"
Please choose the topic:
1: ORDER
2: PRODUCT
3: ORDER ITEM
4: EXIT");
                int.TryParse(Console.ReadLine(), out Choice);
                try
                {
                    switch (Choice)
                    {
                        case 1:
                            ChoiceOrder();
                            break;
                        case 2:
                            ChoiceProduct();
                            break;
                        case 3:
                            ChoiceOrderItem();

                            break;
                        case 4:
                            Console.WriteLine("Thank you and have a nice day :) ");
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

            } while (Choice != 4);
        }

        //in case the user chose order
        public void ChoiceOrder()
        {
            int action;
            do
            {
                Console.WriteLine(
@"  
  Please choose the option:
  1: Add Order
  2: Get list of Order
  3: Search Order
  4: Update Order
  5: Delete Order
  6: Back");

                int.TryParse(Console.ReadLine(), out action);
                try
                {
                    switch (action)
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

                                DateTime tmpDateTime;

                                Console.WriteLine("enter order's date in a dd.mm.yy format:");
                                DateTime.TryParse(Console.ReadLine(), out tmpDateTime);
                                NewOrder.OrderDate = tmpDateTime;

                                Console.WriteLine("enter order's ship date in a dd.mm.yy format:");
                                DateTime.TryParse(Console.ReadLine(), out tmpDateTime);
                                NewOrder.ShipDate = tmpDateTime;

                                Console.WriteLine("enter order's delivary date in a dd.mm.yy format:");
                                DateTime.TryParse(Console.ReadLine(), out tmpDateTime);
                                NewOrder.DeliveryDate = tmpDateTime;

                                Console.Write("The order's seqNum is: ");
                                Console.WriteLine(dalList.Order.Create(NewOrder));

                                break;
                            }
                        case 2://return the orders
                            {
                                Console.WriteLine("The orders list:");
                                foreach (Order ord in dalList.Order.RequestAll())
                                {
                                    Console.Write(ord);
                                }

                                break;
                            }
                        case 3://return the order that matches the id
                            {
                                Console.Write("Enter the order's seqNum: ");
                                int id; int.TryParse(Console.ReadLine(), out id);
                                Console.WriteLine(dalList.RequestById(id));
                                break;
                            }
                        case 4://update an order
                            {
                                Console.Write("Enter a seqNum: ");
                                int seqNum_; int.TryParse(Console.ReadLine(), out seqNum_);

                                Console.WriteLine(dalList.Order.RequestById(seqNum_));

                                //Console.Write("If you still want to update, press 1, else press 0 ");
                                //int.TryParse(Console.ReadLine(), out int yes);

                                //if (yes == 1)
                                //    break;

                                Order updatedOrder = new Order();

                                updatedOrder.seqNum = seqNum_;
                                Console.Write("Enter a Full name: ");
                                updatedOrder.CustomerName = Console.ReadLine();
                                Console.Write("Enter an Email: ");
                                updatedOrder.CustomerEmail = Console.ReadLine();
                                Console.Write("Enter an Adress: ");
                                updatedOrder.CustomerAdress = Console.ReadLine();

                                DateTime tmpDateTime;

                                Console.WriteLine("enter order's date in a dd.mm.yy format:");
                                DateTime.TryParse(Console.ReadLine(), out tmpDateTime);
                                updatedOrder.OrderDate = tmpDateTime;

                                Console.WriteLine("enter order's ship date in a dd.mm.yy format:");
                                DateTime.TryParse(Console.ReadLine(), out tmpDateTime);
                                updatedOrder.ShipDate = tmpDateTime;

                                Console.WriteLine("enter order's delivary date in a dd.mm.yy format:");
                                DateTime.TryParse(Console.ReadLine(), out tmpDateTime);
                                updatedOrder.DeliveryDate = tmpDateTime;

                                dalList.Order.Update(updatedOrder);
                                break;
                            }
                        case 5://delete an order
                            {
                                Console.Write("Enter the order's seqNum: ");
                                int id; int.TryParse(Console.ReadLine(), out id);

                                dalList.Order.Delete(id);
                                break;
                            }
                        case 6:
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

            } while (action != 6);
        }

        //in case the user chose product
        public void ChoiceProduct()
        {
            int action;
            do
            {
                Console.WriteLine(
@"  
  Please choose the option:
  1: Add Product
  2: Get list of Product
  3: Search Product
  4: Update Product
  5: Delete Product
  6: Back");
                int.TryParse(Console.ReadLine(), out action);
                try
                {
                    switch (action)
                    {

                        case 1://adds a product
                            {
                                Product newProduct = new Product();
                                Console.Write("Enter a barcode: ");
                                int ID; int.TryParse(Console.ReadLine(), out ID);
                                newProduct.ID = ID;

                                Console.Write("Enter a Category: 0- Percussions , 1- StringInstrument , 2- WindInstrument , 3- KeyBoard , 4- BowInstrument: ");
                                string input = Console.ReadLine();
                                newProduct.Category = (category)int.Parse(input);

                                Console.Write("Enter a Product name: ");
                                newProduct.Name = Console.ReadLine();

                                Console.Write("Enter a Color: 0- Black , 1- Red , 2- White , 3- Brown: ");
                                input = Console.ReadLine();
                                newProduct.Color = (color)int.Parse(input);

                                Console.Write("Enter a Price: ");
                                int price; int.TryParse(Console.ReadLine(), out price);
                                newProduct.Price = price;

                                Console.Write("Enter an Amount: ");
                                int InStock; int.TryParse(Console.ReadLine(), out InStock);
                                newProduct.InStock = InStock;

                                Console.Write("The product's barcode is: ");
                                Console.WriteLine(dalList.Product.Create(newProduct));

                                break;
                            }
                        case 2://return the products
                            {
                                Console.WriteLine("The products list:");

                                foreach (Product pdct in dalList.Product.RequestAll())
                                {
                                    Console.WriteLine(pdct);
                                }
                                break;
                            }
                        case 3://return the product that matches the given barcode
                            {
                                Console.Write("Enter the product's barcode: ");
                                int id; int.TryParse(Console.ReadLine(), out id);
                                Console.Write(dalList.Product.RequestById(id));
                                break;
                            }
                        case 4://update a product
                            {
                                Console.Write("Enter a barcode: ");
                                int barcode; int.TryParse(Console.ReadLine(), out barcode);
                                Console.WriteLine(dalList.Product.RequestById(barcode));

                                Product newProduct = new Product();
                                newProduct.ID = barcode;

                                Console.Write("Enter a Category: 0- Percussions , 1- StringInstrument , 2- WindInstrument , 3- KeyBoard , 4- BowInstrument: ");
                                string input = Console.ReadLine();
                                newProduct.Category = (category)int.Parse(input);

                                Console.Write("Enter a Product name: ");
                                newProduct.Name = Console.ReadLine();

                                Console.Write("Enter a Color: 0- Black , 1- Red , 2- White , 3- Brown: ");
                                input = Console.ReadLine();
                                newProduct.Color = (color)int.Parse(input);

                                Console.Write("Enter a Price: ");
                                int Price; int.TryParse(Console.ReadLine(), out Price);
                                newProduct.Price = Price;

                                Console.Write("Enter an Amount: ");
                                int InStock; int.TryParse(Console.ReadLine(), out InStock);
                                newProduct.InStock = InStock;

                                dalList.Product.Update(newProduct);
                                break;
                            }
                        case 5://delete a product
                            {
                                Console.Write("Enter the product's barcode: ");
                                int id; int.TryParse(Console.ReadLine(), out id);

                                dalList.Product.Delete(id);
                                break;
                            }
                        case 6:
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

            } while (action != 6);
        }

        //in case the user chose orderItem
        void ChoiceOrderItem()
        {
            int action;
            do
            {
                Console.WriteLine(
@"  
  Please choose the option:
  1: Add Order item
  2: Get list of Order item
  3: Search Order item 
  4: Get Order item by order seq number & product barcode
  5: Get list of Order item by order seq number
  6: Update Order item
  7: Delete Order item
  8: Back");
                int.TryParse(Console.ReadLine(), out action);
                try
                {
                    switch (action)
                    {
                        case 1://adds an orderItem
                            {
                                OrderItem newOrderItem = new OrderItem();
                                Console.Write("Enter the order's ID: ");
                                int OrderID; int.TryParse(Console.ReadLine(), out OrderID);
                                newOrderItem.OrderID = OrderID;

                                Console.Write("Enter the product's barcode: ");
                                int ProductID; int.TryParse(Console.ReadLine(), out ProductID);
                                newOrderItem.ProductID = ProductID;

                                Console.Write("Enter a Price: ");
                                Double Price; double.TryParse(Console.ReadLine(), out Price);
                                newOrderItem.Price = Price;

                                Console.Write("Enter the amount: ");
                                int Amount; int.TryParse(Console.ReadLine(), out Amount);
                                newOrderItem.Amount = Amount;

                                Console.Write("The orderItem's seqNum is: ");
                                Console.WriteLine(dalList.OrderItem.Create(newOrderItem));

                                break;
                            }
                        case 2://returns all the orderItems
                            {
                                Console.WriteLine("The order items list:");

                                foreach (OrderItem OI in dalList.OrderItem.RequestAll())
                                {
                                    Console.Write(OI);
                                }

                                break;
                            }
                        case 3://returns the orderItem that matches the given seqNum
                            {
                                Console.Write("Enter the orderItem's seqNum: ");
                                int seqNum; int.TryParse(Console.ReadLine(), out seqNum);
                                Console.Write(dalList.OrderItem.RequestById(seqNum));
                                break;
                            }
                        case 4://returns the orderItem that matches the given order's seqNum and  the product's barcode
                            {
                                Console.Write("Enter the order's seqNum: ");
                                int O_ID; int.TryParse(Console.ReadLine(), out O_ID);
                                Console.Write("Enter the product's barcode: ");
                                int P_ID; int.TryParse(Console.ReadLine(), out P_ID); Console.WriteLine(dalList.OrderItem.RequestByOrderIDProductID(O_ID, P_ID));
                                break;
                            }
                        case 5://returns the orderItem that matches the given order's seqNum
                            {
                                Console.Write("Enter the order's seqNum: ");
                                int O_ID; int.TryParse(Console.ReadLine(), out O_ID);
                                foreach (OrderItem OI in dalList.OrderItem.RequestByOrderId(O_ID))
                                {
                                    Console.Write(OI);
                                }
                                break;
                            }
                        case 6://update an orderItem
                            {
                                Console.Write("Enter a seqNum: ");
                                int seqNum_; int.TryParse(Console.ReadLine(), out seqNum_);
                                Console.WriteLine(dalList.OrderItem.RequestById(seqNum_));

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

                                dalList.OrderItem.Update(updatedOrderItem);

                                break;
                            }
                        case 7://delete an orderItem
                            {
                                Console.Write("Enter the orderItem's seqNum: ");

                                int seqNum; int.TryParse(Console.ReadLine(), out seqNum);
                                dalList.OrderItem.Delete(seqNum);
                                break;
                            }
                        case 8:
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
            } while (action != 8);
        }

    }
}

