using System.Reflection.Metadata.Ecma335;

public class Order
{
    private int _orderId;
    private Sender _sender;
    private Customer _customer;
    private List<Product> _products;
    private string _shippingMethod;
    private string _trackingNumber;
    private decimal _total;
    private decimal _tax;
    private decimal _weight = 0;
    private static Random _random = new Random();

    public Order(int orderId, Customer customer, Sender sender, List<Product> products, Address address, string shippingMethod = "Ground")
    {
        this._orderId = orderId;
        this._customer = customer;
        this._products = products;
        this._tax = address.GetTax();
        foreach (Product product in products)
        {
            Math.Round(this._total += product.Cost(product.GetQuantity(), product.GetPrice()), 2);
        }
        this._trackingNumber = GenerateTrackingNumber();
        this._shippingMethod = shippingMethod;
        this._sender = sender;
    }
    public static string GenerateTrackingNumber()
    {
        string prefix = "1Z";
        string randomPart = GenerateRandomString(10);
        string trackingNumber = $"{prefix}{randomPart}{DateTime.UtcNow.Ticks}";
        return trackingNumber;
    }
    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEF0123456789";
        char[] stringChars = new char[length];
        
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[_random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
    public decimal GetTax()
    {
        return this._tax;
    }
    public void SetTax(decimal tax)
    {
        this._tax = tax;
    }
    public int GetOrderId()
    {
        return this._orderId;
    }
    public void SetOrderId(int orderId)
    {
        this._orderId = orderId;
    }
    public Customer GetCustomer()
    {
        return this._customer;
    }
    public void SetCustomer(Customer Customer)
    {
        this._customer = Customer;
    }
    public List<Product> GetProducts()
    {
        return this._products;
    }
    public void SetProducts(List<Product> Products)
    {
        this._products = Products;
    }
    public void ListProducts()
    {
        foreach (Product product in _products)
        {
            Console.WriteLine("\t" + product.GetName() + " - Product ID: (" + product.GetProductId() + ") - " + product.GetQuantity() + " @ $" + product.GetPrice());
            _weight += product.GetWeight();
        }
    }
    public decimal CalculateTotal()
    {
        this._total = 0;
        foreach (Product product in _products)
        {
            Math.Round(this._total += product.Cost(product.GetQuantity(), product.GetPrice()), 2);
        }
        return this._total;
    }
    public decimal GetTotal()
    {   
        return this._total;
    }
    public void AddProduct(Product product, int quantity = 1)
    {
        product.Sell(quantity);
        product.SetQuantity(quantity);
        if (product == null)
        {
            Console.WriteLine("Product must not be null.");
            return;
        }

        this._products.Add(product); 
        this._total += product.Cost(product.GetQuantity(), product.GetPrice());
    }
    public void RemoveProduct(Product product)
    {
        if (product == null)
        {
            Console.WriteLine("Product must not be null.");
            return;
        }

        if (!this._products.Contains(product))
        {
            Console.WriteLine("Product not found.");
            return;
        }
        int index = this._products.FindIndex(p => p.Equals(product));
        this._products[index].Restock(product.GetQuantity());

        this._products.Remove(product);
        this._total -= product.Cost(product.GetQuantity(), product.GetPrice());
    }
    public void UpdateCustomer(Customer customer)
    {
        if (customer == null)
        {
            Console.WriteLine("Customer must not be null.");
        }
        else
        {
            this._customer = customer;
        }
    }
    public void UpdateTotal(decimal total)
    {
        if (total < 0)
        {
            Console.WriteLine("Total must be greater than 0.");
        }
        else
        {
            this._total = total;
        }
    }
    public void PrintShippingLabel()
    {
        Console.WriteLine("++++++ Shipping Label: ++++++");
        Console.WriteLine(this._sender.GetName());
        this._sender.PrintAddress();
        Console.WriteLine();
        Console.WriteLine(this._customer.GetName());
        this._customer.PrintAddress();
        Console.WriteLine();
        Console.WriteLine("Order ID: " + this._orderId);
        Console.WriteLine("Tracking Number: " + this._trackingNumber);
        Console.WriteLine("Date Shipped: " + DateTime.Now);
        Console.WriteLine("Shipping Method: " + this._shippingMethod);
        Console.WriteLine("--------------------------------");
    }
    public void PrintPackingLabel()
    {
        Console.WriteLine("++++++ Packing Label: ++++++");
        Console.WriteLine(this._customer.GetName());
        this._customer.PrintAddress();
        ListProducts();
        Console.WriteLine("Total Weight: " + this._weight + " lbs");
        Console.WriteLine("--------------------------------");
    }
    public void PrintOrder()
    {
        PrintShippingLabel();
        PrintPackingLabel();
        Console.WriteLine("Order ID: " + this._orderId);
        Console.WriteLine("Customer: " + this._customer
            .GetName() + " - CustomerID:(" + this._customer.GetCustomerId() + ")");
        Console.WriteLine("Products:");
        ListProducts();
        Console.WriteLine("Tax: $" + Math.Round((this._total * this._tax), 2));
        Console.WriteLine("Total: $" + Math.Round((this._total + (this._total * _tax)), 2));
        Console.WriteLine("--------------------------------");
    }
}