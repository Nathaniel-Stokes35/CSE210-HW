public class Customer
{
    private string _name;
    private int _customerId;
    private Address _address;
    private string _email;
    private string _phone;

    public Customer(string name, int customerId, Address address, string email = null, string phone = null)
    {
        this._name = name;
        this._customerId = customerId;
        this._address = address;
        this._email = email;
        this._phone = phone;
    }
    public string GetName() {
        return this._name;
    }
    public void SetName(string name) {
        this._name = name;
    }
    public int GetCustomerId() {
        return this._customerId;
    }
    public void SetCustomerId(int customerId) {
        this._customerId = customerId;
    }
    public void PrintAddress() {
        Console.WriteLine(this._address.GetAddress());
    }
    public Address GetAddress() {
        return this._address;
    }
    public void SetAddress(Address address) {
        this._address = address;
    }
    public string GetEmail() {
        if (_email == null) {
            Console.WriteLine("Customer Doesn't have a recorded Email. Add Email? (y/n)");
            string response = Console.ReadLine();
            if (response == "y") {
                Console.WriteLine("Enter Full Email (i.e. newEmail@e-mail.com):");
                string email = Console.ReadLine();
                this._email = email;
            } else {
                Console.WriteLine("Email not updated.");
            }
        }
        return this._email;
    }
    public void SetEmail(string email) {
        this._email = email;
    }
    public string GetPhone() {
        return this._phone;
    }
    public void SetPhone(string phone) {
        this._phone = phone;
    }
    public void UpdateAddress (Address address) {
        if (_address.GetAddress == null) {
            Console.WriteLine("Customer has No Listed Address. Add Address? (y/n)");
            string response = Console.ReadLine();
            if (response == "y") {
                this._address = address;
            } else {
                Console.WriteLine("Address not updated.");
            }
        } else {
            this._address = address;
        }
    }
    public void UpdatePhone (string phone) {
        if (_phone == null) {
            Console.WriteLine("Customer has no Listed Phone Number. Add Number? (y/n)");
            string response = Console.ReadLine();
            if (response == "y") {
                this._phone = phone;
            } else {
                Console.WriteLine("Phone number not updated.");
            }
        } else {
            this._phone = phone;
        }
    }
    public static Boolean UsaCustomer (Address address) {
        if (address.IsInUSA()) {
            return true;
        } else {
            return false;
        }
    }
}