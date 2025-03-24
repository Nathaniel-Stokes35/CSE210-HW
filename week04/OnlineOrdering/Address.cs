public class Address 
{
    private string _street;
    private string _city;
    private string _state;
    private string _zip;
    private string _country;
    private decimal _tax;
    public Address(string street, string city, string state, string zip, string country, decimal tax) 
    {
        this._street = street;
        this._city = city;
        this._state = state;
        this._zip = zip;
        this._country = country;
        this._tax = tax;
    }
    public decimal GetTax()
    {
        return this._tax;
    }
    public void SetTax(decimal tax)
    {
        this._tax = tax;
    }
    public string GetCountry() 
    {
        return this._country;
    }
    public string GetStreet() 
    {
        return this._street;
    }
    public string GetCity() 
    {
        return this._city;
    }
    public string GetState() 
    {
        return this._state;
    }
    public string GetZip() 
    {
        return this._zip;
    }
    public string GetAddress() 
    {
        return this._street + ",\n" + this._city + ", " + this._state + " " + this._zip + ",\n" + this._country;
    }
    public void SetAddress(string street, string city, string state, string zip, string country) 
    {
        this._street = street;
        this._city = city;
        this._state = state;
        this._zip = zip;
        this._country = country;
    }
    public Boolean IsInUSA() 
    {
        if (this._country == "USA") 
        {
            return true;
        } 
        else 
        {
            return false;
        }
    }    
}