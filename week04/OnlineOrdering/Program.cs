class Program
{

    private static List<Customer> _customers = new List<Customer>();
    private static List<Order> _orders = new List<Order>();
    private static List<Address> _addresses = new List<Address>();
    private static int _customerId = 0;
    private static int _senderId = 0;
    private static int _productId = 0;
    private static int _orderId = 0;

    static void Main(string[] args)
    {
        Program program = new Program();
        program.Run();
    }

    private void Run()
    {
        Address sendAddress1 = new Address("1100 Lejune Dr", "Springfield", "IL", "62703", "USA", 0.0975m);
        Address sendAddress2 = new Address("5255 Elvis Presley Blvd", "Memphis", "TN", "38116", "USA", 0.07m);
        Address sendAddress3 = new Address("3705 E South St", "Long Beach", "CA", "90805", "USA", 0.1025m);
        Sender sender1 = new Sender("Walmart", _senderId++, sendAddress1);
        Sender sender2 = new Sender("Walmart", _senderId++, sendAddress2);
        Sender sender3 = new Sender("Walmart", _senderId++, sendAddress3);

        Product bananas = new Product("Bananas", _productId++, 0.59m, 100, 0.4m);
        Product shampoo = new Product("Shampoo", _productId++, 4.99m, 100, 1.05m);
        Product toothpaste = new Product("Toothpaste", _productId++, 2.99m, 100, 0.375m);
        Product television = new Product("Television", _productId++, 499.99m, 100, 50m);
        Product apples = new Product("Apples", _productId++, 1.99m, 100, 1m);
        Product milk = new Product("Milk", _productId++, 4.85m, 100, 8.6m);
        Product eggs = new Product("Eggs", _productId++, 8.12m, 100, 2m);
        Product bread = new Product("Bread", _productId++, 3.85m, 100, 1.25m);
        Product cheese = new Product("Cheese", _productId++, 3.99m, 100, 1m);
        Product crackers = new Product("Crackers", _productId++, 1.99m, 100, 3m);
        Product chips = new Product("Chips", _productId++, 1.99m, 100, 0.8m);
        Product soda = new Product("Soda", _productId++, 7.99m, 100, 10m);
        Product water = new Product("Water", _productId++, 5.86m, 100, 43m);
        Product juice = new Product("Juice", _productId++, 2.89m, 100, 1.5m);
        Product cereal = new Product("Cereal", _productId++, 3.51m, 100, 1.5m);

        Address address1 = new Address("123 Main St", "Springfield", "IL", "62701", "USA", 0.0975m);
        _addresses.Add(address1);
        Customer John = new Customer("John Doe", _customerId++, address1);
        _customers.Add(John);

        Order order1 = new Order(_orderId++, John, sender1, new List<Product>(), address1);
        order1.AddProduct(bananas, 5);
        order1.AddProduct(shampoo);
        order1.AddProduct(toothpaste);
        order1.AddProduct(television);
        order1.AddProduct(apples, 10);
        _orders.Add(order1);
        order1.PrintOrder();

        Address address2 = new Address("123 Beale St", "Memphis", "TN", "38103", "USA", 0.07m);
        _addresses.Add(address2);
        Customer Susan = new Customer("Susan McDonald", _customerId++, address2);
        _customers.Add(Susan);

        Order order2 = new Order(_orderId++, Susan, sender2, new List<Product>(), address2);
        bananas.SetQuantity(10);
        order2.AddProduct(bananas, 7);
        order2.AddProduct(milk, 2);
        order2.AddProduct(eggs, 4);
        order2.AddProduct(bread, 2);
        order2.AddProduct(cheese, 4);
        order2.AddProduct(soda);
        _orders.Add(order2);
        order2.PrintOrder();

        Address address3 = new Address("4557 Orange Ave", "Long Beach", "CA", "90807", "USA", 0.1025m);
        _addresses.Add(address3);
        Customer Rob = new Customer("Robert Smith", _customerId++, address2);
        _customers.Add(Rob);

        Order order3 = new Order(_orderId++, Rob, sender3, new List<Product>(), address3);
        order3.AddProduct(crackers, 3);
        order3.AddProduct(chips, 2);
        order3.AddProduct(water, 2);
        order3.AddProduct(juice, 4);
        order3.AddProduct(cereal, 2);
        _orders.Add(order3);
        order3.PrintOrder();
    }
}