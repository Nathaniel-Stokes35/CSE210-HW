public class Product(string name, int productId, decimal price, int stock, decimal weight = 0.5m, int quantity = 1)
{
    private string _name = name;
    private int _productId = productId;
    private decimal _price = price;
    private decimal _weight = weight; 
    private int _quantity = quantity; // Amount being sold on the order
    private int _stock = stock; // OH of the product for the store
//----------------------------------------------
    public string GetName() {
        return this._name;
    }
    public void SetName(string name) {
        this._name = name;
    }
    public decimal GetWeight() {
        return this._weight;
    }
    public void SetWeight(decimal weight) {
        this._weight = weight;
    }
    public int GetProductId() {
        return this._productId;
    }
    public void SetProductId(int productId) {
        this._productId = productId;
    }
    public decimal GetPrice() {
        return this._price;
    }
    public void SetPrice(decimal price) {
        this._price = price;
    }
    public int GetQuantity() {
        return this._quantity;
    }
    public void SetQuantity(int quantity) {
        this._quantity = quantity;
    }
    public int GetStock() {
        return this._stock;
    }
//----------------------------------------------
    public decimal Cost (int quantity, decimal price) {
        if (quantity < 0 || price < 0) {
            Console.WriteLine("Quantity and Price must be greater than 0.");
            return 0;
        } else {
            return quantity * price;
        }
    }
    public void Restock (int restock) {
        if (restock < 0) {
            Console.WriteLine("Restock must be greater than 0.");
        } else {
            this._stock += restock;
        }
    }
    public void Sell (int sell) {
        if (sell < 0) {
            Console.WriteLine("Sell must be greater than 0.");
        } else if (sell > this._stock) {
            Console.WriteLine("Not enough stock to sell.");
        } else {
            this._stock -= sell;
        }
    }
}