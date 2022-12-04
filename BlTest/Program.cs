using BO;
using BlApi;
using BlImplementation;
using System;

namespace BlTest
{
    public class Program
    {
        private static IBl _ibl = new Bl();
        private static Cart _cart = new Cart() { CustomerName = null, CustomerEmail = null, CustomerAddress = null, Items = new List<BO.OrderItem>(), TotalPrice = 0 };

        static void Main()
        {
            Console.WriteLine("Hello , we are happy to have you in our store :) ");

            getCartDetails(ref _cart);


            //for the switch loop
            int Choice;

            do
            {
                Console.WriteLine(@"
Please choose the topic:
1: ORDER
2: PRODUCT
3: CART
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
                            ChoiceCart();
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
        static void ChoiceOrder()
        {
            int action;
            string tmpOrderId;
            int orderId;
            Order order = new Order();
            do
            {
                Console.WriteLine(
    @"  
  Please choose the option:
  1: Get Order List For Manager
  2: Get Order Details
  3: Update Order Ship
  4: Update Order Delivery
  5: Tracking Order
  6: Back");

                int.TryParse(Console.ReadLine(), out action);
                try
                {
                    switch (action)
                    {
                        case 1://Get Order List For Manager
                            {
                                IEnumerable<OrderForList> orderList = _ibl.Order.GetOrderListForManager();
                                foreach (OrderForList orderInList in orderList)
                                {
                                    Console.WriteLine(orderInList);
                                }
                                break;
                            }
                        case 2://Get Order Details
                            {
                                Console.WriteLine("Enter order's id");
                                tmpOrderId = Console.ReadLine()!;
                                int.TryParse(tmpOrderId, out orderId);

                                order = _ibl.Order.GetOrderDetails(orderId);
                                Console.WriteLine(order);
                                break;
                            }
                        case 3://Update Order Ship
                            {
                                Console.WriteLine("Enter order's id");
                                tmpOrderId = Console.ReadLine()!;
                                int.TryParse(tmpOrderId, out orderId);

                                order = _ibl.Order.UpdateOrderShip(orderId);
                                Console.WriteLine(order);

                                break;
                            }
                        case 4://Update Order Delivery
                            {
                                Console.WriteLine("Enter order's id");
                                tmpOrderId = Console.ReadLine()!;
                                int.TryParse(tmpOrderId, out orderId);

                                order = _ibl.Order.UpdateOrderDelivery(orderId);
                                Console.WriteLine(order);
                                break;
                            }
                        case 5://Traking Order
                            {
                                Console.WriteLine("Enter order's id");
                                tmpOrderId = Console.ReadLine()!;
                                int.TryParse(tmpOrderId, out orderId);

                                OrderTracking orderTrack = _ibl.Order.TrackingOrder(orderId);
                                Console.WriteLine(orderTrack);
                                break;
                            }
                        case 6:
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }

                catch (BoDoesNoExistException ex)
                {
                    Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                }
                catch (NegativeNumberException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (action != 6);
        }

        //in case the user chose product
        static void ChoiceProduct()
        {
            int action;
            string tmpProductId;
            int productId;
            Product product = new Product();
            ProductItem productItem = new ProductItem();
            do
            {
                Console.WriteLine(
    @"  
  Please choose the option:
  1: Get List Product For Manager And Catalog
  2: Get Product Details For Manager
  3: Get Product Details For Customer
  4: Add Product
  5: Update Product
  6: Delete Product
  7: Back");

                int.TryParse(Console.ReadLine(), out action);
                try
                {
                    switch (action)
                    {

                        case 1://Get List Product For Manager And Catalog
                            {
                                IEnumerable<ProductForList> productList = _ibl.Product.GetListProductForManagerAndCatalog();
                                foreach (ProductForList productInList in productList)
                                {
                                    Console.WriteLine(productInList);
                                }
                                break;
                            }
                        case 2://Get Product Details For Manager
                            {
                                Console.WriteLine("Enter product's id");
                                tmpProductId = Console.ReadLine()!;
                                int.TryParse(tmpProductId, out productId);

                                product = _ibl.Product.GetProductDetailsForManager(productId);
                                Console.WriteLine(product);
                                break;
                            }
                        case 3://Get Product Details For Customer
                            {
                                Console.WriteLine("Enter product's id");
                                tmpProductId = Console.ReadLine()!;
                                int.TryParse(tmpProductId, out productId);

                                productItem = _ibl.Product.GetProductDetailsForCustomer(productId, _cart);
                                Console.WriteLine(productItem);
                                break;
                            }
                        case 4://Add Product
                            {
                                GetProductDetails(ref product);
                                _ibl.Product.AddProduct(product);
                                break;
                            }
                        case 5://Update Product
                            {
                                GetProductDetails(ref product);
                                _ibl.Product.UpdateProduct(product);
                                break;
                            }
                        case 6://Delete Product
                            {
                                Console.WriteLine("Enter product's id");
                                tmpProductId = Console.ReadLine()!;
                                int.TryParse(tmpProductId, out productId);
                                _ibl.Product.DeleteProduct(productId);
                                Console.WriteLine($"Product number {productId} deleted");
                                break;
                            }
                        case 7:
                            break;

                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                catch (BoDoesNoExistException ex)
                {
                    Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                }
                catch (NegativeNumberException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (WrongLengthException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotValidFormatNameException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NegativeDoubleNumberException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotValidDeleteException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (action != 7);
        }

        //in case the user chose orderItem
        static void ChoiceCart()
        {
            int action;
            string tempProductsId;
            int productId;
            Product product = new Product();
            ProductItem productItem = new ProductItem();
            do
            {
                Console.WriteLine(
    @"  
  Please choose the option:
  1: Add Product To Cart
  2: Update Amount Of Product
  3: Delete a product
  4: Commit Order
  5: Clear the cart
  6: Back");
                int.TryParse(Console.ReadLine(), out action);
                try
                {
                    switch (action)
                    {
                        case 1://Add Product To Cart
                            {
                                Console.WriteLine("Enter product's id");
                                tempProductsId = Console.ReadLine()!;
                                int.TryParse(tempProductsId, out productId);
                                Console.WriteLine(_ibl.Cart.AddProductToCart(_cart, productId));
                                break;
                            }
                        case 2://Update Amount Of Product
                            {
                                Console.WriteLine("Enter product's id");
                                tempProductsId = Console.ReadLine()!;
                                int.TryParse(tempProductsId, out productId);
                                Console.WriteLine("Enter product's amount");
                                string tempAmount = Console.ReadLine()!;
                                int amount; int.TryParse(tempAmount, out amount);
                                Console.WriteLine(_ibl.Cart.UpdateAmountOfProduct(_cart, productId, amount));

                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Enter product's id");
                                tempProductsId = Console.ReadLine()!;
                                int.TryParse(tempProductsId, out productId);

                                Console.WriteLine(_ibl.Cart.UpdateAmountOfProduct(_cart, productId, 0));
                                break;
                            }
                        case 4://Commit Order
                            {
                                Console.WriteLine(_ibl.Cart.CommitOrder(_cart));
                                _cart.Items.Clear(); //Emptying the _cart
                                _cart.TotalPrice = 0;

                                break;
                            }
                        case 5:
                            {
                                _cart.Items.Clear(); //Emptying the _cart
                                _cart.TotalPrice = 0;
                            }
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                }
                catch (BoDoesNoExistException ex)
                {
                    Console.WriteLine(ex.Message + " " + ex.InnerException.Message);
                }
                catch (NegativeNumberException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotValidFormatNameException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotValidEmailException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotExistInCartException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotInStockException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            } while (action != 6);
        }
        static Cart getCartDetails(ref Cart cart)
        {
            try
            {
                Console.WriteLine("Enter customer's name:");
                cart.CustomerName = Console.ReadLine()!;

                Console.WriteLine("Enter customer's email:");
                cart.CustomerEmail = Console.ReadLine()!;

                Console.WriteLine("Enter customer's address:");
                cart.CustomerAddress = Console.ReadLine()!;
                _ibl.Cart.CheckFormat(cart);
            }
            catch (NotValidFormatNameException ex)
            {
                Console.WriteLine(ex.Message);
                getCartDetails(ref _cart);
            }
            catch (NotValidEmailException ex)
            {
                Console.WriteLine(ex.Message);
                getCartDetails(ref _cart);
            }
            return cart;
        }

        static Product GetProductDetails(ref Product product)
        {

            Console.WriteLine("Enter product's barcode:");
            int barcode; int.TryParse(Console.ReadLine(), out barcode);
            product.ID = barcode;

            Console.WriteLine("Enter product's name:");
            product.Name = Console.ReadLine()!;

            Console.Write("Enter product's category: 0- Percussions , 1- StringInstrument , 2- WindInstrument , 3- KeyBoard , 4- BowInstrument: ");
            string sinput = Console.ReadLine()!;
            int.TryParse(sinput, out int input);
            if (input <= 4)
                product.Category = (Category)input;
            else throw new Exception("Not valid category exception");

            Console.Write("Enter product's color: 0- Black , 1- Red , 2- White , 3- Brown: ");
            sinput = Console.ReadLine()!;
            int.TryParse(sinput, out input);
            if (input <= 3)
                product.Color = (Color)input;
            else throw new Exception("Not valid color exception");

            Console.WriteLine("Enter product's price:");
            double price; double.TryParse(Console.ReadLine(), out price);
            product.Price = price;

            Console.WriteLine("Enter product's amount in stock:");
            string tempInStock = Console.ReadLine()!;
            int inStock; int.TryParse(tempInStock, out inStock);
            product.InStock = inStock;

            return product;

        }

    }
}

