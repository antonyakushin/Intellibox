using System;

using Mindscape.LightSpeed;
using Mindscape.LightSpeed.Validation;
using Mindscape.LightSpeed.Linq;

namespace IntelliBox.Examples
{
  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Categories", IdColumnName="Category ID")]
  [System.ComponentModel.DataObject]
  public partial class Category : Entity<int>
  {
    #region Fields
  
    [Column("Category Name")]
    [ValidatePresence]
    [ValidateLength(0, 15)]
    private string _categoryName;
    [ValidateLength(0, 536870911)]
    private string _description;
    private byte[] _picture;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the CategoryName entity attribute.</summary>
    public const string CategoryNameField = "CategoryName";
    /// <summary>Identifies the Description entity attribute.</summary>
    public const string DescriptionField = "Description";
    /// <summary>Identifies the Picture entity attribute.</summary>
    public const string PictureField = "Picture";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Category")]
    private readonly EntityCollection<Product> _products = new EntityCollection<Product>();


    #endregion
    
    #region Properties

    public EntityCollection<Product> Products
    {
      get { return Get(_products); }
    }


    public string CategoryName
    {
      get { return Get(ref _categoryName, "CategoryName"); }
      set { Set(ref _categoryName, value, "CategoryName"); }
    }

    public string Description
    {
      get { return Get(ref _description, "Description"); }
      set { Set(ref _description, value, "Description"); }
    }

    public byte[] Picture
    {
      get { return Get(ref _picture, "Picture"); }
      set { Set(ref _picture, value, "Picture"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Suppliers", IdColumnName="Supplier ID")]
  [System.ComponentModel.DataObject]
  public partial class Supplier : Entity<int>
  {
    #region Fields
  
    [Column("Company Name")]
    [ValidatePresence]
    [ValidateLength(0, 40)]
    private string _companyName;
    [Column("Contact Name")]
    [ValidateLength(0, 30)]
    private string _contactName;
    [Column("Contact Title")]
    [ValidateLength(0, 30)]
    private string _contactTitle;
    [ValidateLength(0, 60)]
    private string _address;
    [ValidateLength(0, 15)]
    private string _city;
    [ValidateLength(0, 15)]
    private string _region;
    [Column("Postal Code")]
    [ValidateLength(0, 10)]
    private string _postalCode;
    [ValidateLength(0, 15)]
    private string _country;
    [ValidateLength(0, 24)]
    private string _phone;
    [ValidateLength(0, 24)]
    private string _fax;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the CompanyName entity attribute.</summary>
    public const string CompanyNameField = "CompanyName";
    /// <summary>Identifies the ContactName entity attribute.</summary>
    public const string ContactNameField = "ContactName";
    /// <summary>Identifies the ContactTitle entity attribute.</summary>
    public const string ContactTitleField = "ContactTitle";
    /// <summary>Identifies the Address entity attribute.</summary>
    public const string AddressField = "Address";
    /// <summary>Identifies the City entity attribute.</summary>
    public const string CityField = "City";
    /// <summary>Identifies the Region entity attribute.</summary>
    public const string RegionField = "Region";
    /// <summary>Identifies the PostalCode entity attribute.</summary>
    public const string PostalCodeField = "PostalCode";
    /// <summary>Identifies the Country entity attribute.</summary>
    public const string CountryField = "Country";
    /// <summary>Identifies the Phone entity attribute.</summary>
    public const string PhoneField = "Phone";
    /// <summary>Identifies the Fax entity attribute.</summary>
    public const string FaxField = "Fax";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Supplier")]
    private readonly EntityCollection<Product> _products = new EntityCollection<Product>();


    #endregion
    
    #region Properties

    public EntityCollection<Product> Products
    {
      get { return Get(_products); }
    }


    public string CompanyName
    {
      get { return Get(ref _companyName, "CompanyName"); }
      set { Set(ref _companyName, value, "CompanyName"); }
    }

    public string ContactName
    {
      get { return Get(ref _contactName, "ContactName"); }
      set { Set(ref _contactName, value, "ContactName"); }
    }

    public string ContactTitle
    {
      get { return Get(ref _contactTitle, "ContactTitle"); }
      set { Set(ref _contactTitle, value, "ContactTitle"); }
    }

    public string Address
    {
      get { return Get(ref _address, "Address"); }
      set { Set(ref _address, value, "Address"); }
    }

    public string City
    {
      get { return Get(ref _city, "City"); }
      set { Set(ref _city, value, "City"); }
    }

    public string Region
    {
      get { return Get(ref _region, "Region"); }
      set { Set(ref _region, value, "Region"); }
    }

    public string PostalCode
    {
      get { return Get(ref _postalCode, "PostalCode"); }
      set { Set(ref _postalCode, value, "PostalCode"); }
    }

    public string Country
    {
      get { return Get(ref _country, "Country"); }
      set { Set(ref _country, value, "Country"); }
    }

    public string Phone
    {
      get { return Get(ref _phone, "Phone"); }
      set { Set(ref _phone, value, "Phone"); }
    }

    public string Fax
    {
      get { return Get(ref _fax, "Fax"); }
      set { Set(ref _fax, value, "Fax"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Customers", IdColumnName="Customer ID")]
  [System.ComponentModel.DataObject]
  public partial class Customer : Entity<string>
  {
    #region Fields
  
    [Column("Company Name")]
    [ValidatePresence]
    [ValidateLength(0, 40)]
    private string _companyName;
    [Column("Contact Name")]
    [ValidateLength(0, 30)]
    private string _contactName;
    [Column("Contact Title")]
    [ValidateLength(0, 30)]
    private string _contactTitle;
    [ValidateLength(0, 60)]
    private string _address;
    [ValidateLength(0, 15)]
    private string _city;
    [ValidateLength(0, 15)]
    private string _region;
    [Column("Postal Code")]
    [ValidateLength(0, 10)]
    private string _postalCode;
    [ValidateLength(0, 15)]
    private string _country;
    [ValidateLength(0, 24)]
    private string _phone;
    [ValidateLength(0, 24)]
    private string _fax;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the CompanyName entity attribute.</summary>
    public const string CompanyNameField = "CompanyName";
    /// <summary>Identifies the ContactName entity attribute.</summary>
    public const string ContactNameField = "ContactName";
    /// <summary>Identifies the ContactTitle entity attribute.</summary>
    public const string ContactTitleField = "ContactTitle";
    /// <summary>Identifies the Address entity attribute.</summary>
    public const string AddressField = "Address";
    /// <summary>Identifies the City entity attribute.</summary>
    public const string CityField = "City";
    /// <summary>Identifies the Region entity attribute.</summary>
    public const string RegionField = "Region";
    /// <summary>Identifies the PostalCode entity attribute.</summary>
    public const string PostalCodeField = "PostalCode";
    /// <summary>Identifies the Country entity attribute.</summary>
    public const string CountryField = "Country";
    /// <summary>Identifies the Phone entity attribute.</summary>
    public const string PhoneField = "Phone";
    /// <summary>Identifies the Fax entity attribute.</summary>
    public const string FaxField = "Fax";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Customer")]
    private readonly EntityCollection<Order> _orders = new EntityCollection<Order>();


    #endregion
    
    #region Properties

    public EntityCollection<Order> Orders
    {
      get { return Get(_orders); }
    }


    public string CompanyName
    {
      get { return Get(ref _companyName, "CompanyName"); }
      set { Set(ref _companyName, value, "CompanyName"); }
    }

    public string ContactName
    {
      get { return Get(ref _contactName, "ContactName"); }
      set { Set(ref _contactName, value, "ContactName"); }
    }

    public string ContactTitle
    {
      get { return Get(ref _contactTitle, "ContactTitle"); }
      set { Set(ref _contactTitle, value, "ContactTitle"); }
    }

    public string Address
    {
      get { return Get(ref _address, "Address"); }
      set { Set(ref _address, value, "Address"); }
    }

    public string City
    {
      get { return Get(ref _city, "City"); }
      set { Set(ref _city, value, "City"); }
    }

    public string Region
    {
      get { return Get(ref _region, "Region"); }
      set { Set(ref _region, value, "Region"); }
    }

    public string PostalCode
    {
      get { return Get(ref _postalCode, "PostalCode"); }
      set { Set(ref _postalCode, value, "PostalCode"); }
    }

    public string Country
    {
      get { return Get(ref _country, "Country"); }
      set { Set(ref _country, value, "Country"); }
    }

    public string Phone
    {
      get { return Get(ref _phone, "Phone"); }
      set { Set(ref _phone, value, "Phone"); }
    }

    public string Fax
    {
      get { return Get(ref _fax, "Fax"); }
      set { Set(ref _fax, value, "Fax"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Employees", IdColumnName="Employee ID")]
  [System.ComponentModel.DataObject]
  public partial class Employee : Entity<int>
  {
    #region Fields
  
    [Column("Last Name")]
    [ValidatePresence]
    [ValidateLength(0, 20)]
    private string _lastName;
    [Column("First Name")]
    [ValidatePresence]
    [ValidateLength(0, 10)]
    private string _firstName;
    [ValidateLength(0, 30)]
    private string _title;
    [Column("Birth Date")]
    private System.Nullable<System.DateTime> _birthDate;
    [Column("Hire Date")]
    private System.Nullable<System.DateTime> _hireDate;
    [ValidateLength(0, 60)]
    private string _address;
    [ValidateLength(0, 15)]
    private string _city;
    [ValidateLength(0, 15)]
    private string _region;
    [Column("Postal Code")]
    [ValidateLength(0, 10)]
    private string _postalCode;
    [ValidateLength(0, 15)]
    private string _country;
    [Column("Home Phone")]
    [ValidateLength(0, 24)]
    private string _homePhone;
    [ValidateLength(0, 4)]
    private string _extension;
    private byte[] _photo;
    [ValidateLength(0, 536870911)]
    private string _notes;
    [Column("Reports To")]
    private System.Nullable<int> _reportsTo;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the LastName entity attribute.</summary>
    public const string LastNameField = "LastName";
    /// <summary>Identifies the FirstName entity attribute.</summary>
    public const string FirstNameField = "FirstName";
    /// <summary>Identifies the Title entity attribute.</summary>
    public const string TitleField = "Title";
    /// <summary>Identifies the BirthDate entity attribute.</summary>
    public const string BirthDateField = "BirthDate";
    /// <summary>Identifies the HireDate entity attribute.</summary>
    public const string HireDateField = "HireDate";
    /// <summary>Identifies the Address entity attribute.</summary>
    public const string AddressField = "Address";
    /// <summary>Identifies the City entity attribute.</summary>
    public const string CityField = "City";
    /// <summary>Identifies the Region entity attribute.</summary>
    public const string RegionField = "Region";
    /// <summary>Identifies the PostalCode entity attribute.</summary>
    public const string PostalCodeField = "PostalCode";
    /// <summary>Identifies the Country entity attribute.</summary>
    public const string CountryField = "Country";
    /// <summary>Identifies the HomePhone entity attribute.</summary>
    public const string HomePhoneField = "HomePhone";
    /// <summary>Identifies the Extension entity attribute.</summary>
    public const string ExtensionField = "Extension";
    /// <summary>Identifies the Photo entity attribute.</summary>
    public const string PhotoField = "Photo";
    /// <summary>Identifies the Notes entity attribute.</summary>
    public const string NotesField = "Notes";
    /// <summary>Identifies the ReportsTo entity attribute.</summary>
    public const string ReportsToField = "ReportsTo";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Employee")]
    private readonly EntityCollection<Order> _orders = new EntityCollection<Order>();


    #endregion
    
    #region Properties

    public EntityCollection<Order> Orders
    {
      get { return Get(_orders); }
    }


    public string LastName
    {
      get { return Get(ref _lastName, "LastName"); }
      set { Set(ref _lastName, value, "LastName"); }
    }

    public string FirstName
    {
      get { return Get(ref _firstName, "FirstName"); }
      set { Set(ref _firstName, value, "FirstName"); }
    }

    public string Title
    {
      get { return Get(ref _title, "Title"); }
      set { Set(ref _title, value, "Title"); }
    }

    public System.Nullable<System.DateTime> BirthDate
    {
      get { return Get(ref _birthDate, "BirthDate"); }
      set { Set(ref _birthDate, value, "BirthDate"); }
    }

    public System.Nullable<System.DateTime> HireDate
    {
      get { return Get(ref _hireDate, "HireDate"); }
      set { Set(ref _hireDate, value, "HireDate"); }
    }

    public string Address
    {
      get { return Get(ref _address, "Address"); }
      set { Set(ref _address, value, "Address"); }
    }

    public string City
    {
      get { return Get(ref _city, "City"); }
      set { Set(ref _city, value, "City"); }
    }

    public string Region
    {
      get { return Get(ref _region, "Region"); }
      set { Set(ref _region, value, "Region"); }
    }

    public string PostalCode
    {
      get { return Get(ref _postalCode, "PostalCode"); }
      set { Set(ref _postalCode, value, "PostalCode"); }
    }

    public string Country
    {
      get { return Get(ref _country, "Country"); }
      set { Set(ref _country, value, "Country"); }
    }

    public string HomePhone
    {
      get { return Get(ref _homePhone, "HomePhone"); }
      set { Set(ref _homePhone, value, "HomePhone"); }
    }

    public string Extension
    {
      get { return Get(ref _extension, "Extension"); }
      set { Set(ref _extension, value, "Extension"); }
    }

    public byte[] Photo
    {
      get { return Get(ref _photo, "Photo"); }
      set { Set(ref _photo, value, "Photo"); }
    }

    public string Notes
    {
      get { return Get(ref _notes, "Notes"); }
      set { Set(ref _notes, value, "Notes"); }
    }

    public System.Nullable<int> ReportsTo
    {
      get { return Get(ref _reportsTo, "ReportsTo"); }
      set { Set(ref _reportsTo, value, "ReportsTo"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Order Details", IdColumnName="Product ID")]
  [System.ComponentModel.DataObject]
  public partial class OrderDetail : Entity<OrderDetailId>
  {
    #region Fields
  
    [Column("Unit Price")]
    private decimal _unitPrice;
    private short _quantity;
    private float _discount;
    private int _orderId;
    private int _productId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the UnitPrice entity attribute.</summary>
    public const string UnitPriceField = "UnitPrice";
    /// <summary>Identifies the Quantity entity attribute.</summary>
    public const string QuantityField = "Quantity";
    /// <summary>Identifies the Discount entity attribute.</summary>
    public const string DiscountField = "Discount";
    /// <summary>Identifies the OrderId entity attribute.</summary>
    public const string OrderIdField = "OrderId";
    /// <summary>Identifies the ProductId entity attribute.</summary>
    public const string ProductIdField = "ProductId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("OrderDetails")]
    private readonly EntityHolder<Order> _order = new EntityHolder<Order>();
    [ReverseAssociation("OrderDetails")]
    private readonly EntityHolder<Product> _product = new EntityHolder<Product>();


    #endregion
    
    #region Properties

    public Order Order
    {
      get { return Get(_order); }
      set { Set(_order, value); }
    }

    public Product Product
    {
      get { return Get(_product); }
      set { Set(_product, value); }
    }


    public decimal UnitPrice
    {
      get { return Get(ref _unitPrice, "UnitPrice"); }
      set { Set(ref _unitPrice, value, "UnitPrice"); }
    }

    public short Quantity
    {
      get { return Get(ref _quantity, "Quantity"); }
      set { Set(ref _quantity, value, "Quantity"); }
    }

    public float Discount
    {
      get { return Get(ref _discount, "Discount"); }
      set { Set(ref _discount, value, "Discount"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Order" /> property.</summary>
    public int OrderId
    {
      get { return Get(ref _orderId, "OrderId"); }
      set { Set(ref _orderId, value, "OrderId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Product" /> property.</summary>
    public int ProductId
    {
      get { return Get(ref _productId, "ProductId"); }
      set { Set(ref _productId, value, "ProductId"); }
    }

    #endregion
  }

  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  public partial struct OrderDetailId
  {
    
    public OrderDetailId(
      int orderId ,
      int productId     )
    {
      _orderId = orderId;
      _productId = productId;
    }
    
    #region Fields

    private readonly int _orderId;
    private readonly int _productId;

    #endregion
    
    #region Properties

    public int OrderId
    {
      get { return _orderId; }
    }
    public int ProductId
    {
      get { return _productId; }
    }

    #endregion
  }

  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Orders", IdColumnName="Order ID")]
  [System.ComponentModel.DataObject]
  public partial class Order : Entity<int>
  {
    #region Fields
  
    [Column("Ship Name")]
    [ValidateLength(0, 40)]
    private string _shipName;
    [Column("Ship Address")]
    [ValidateLength(0, 60)]
    private string _shipAddress;
    [Column("Ship City")]
    [ValidateLength(0, 15)]
    private string _shipCity;
    [Column("Ship Region")]
    [ValidateLength(0, 15)]
    private string _shipRegion;
    [Column("Ship Postal Code")]
    [ValidateLength(0, 10)]
    private string _shipPostalCode;
    [Column("Ship Country")]
    [ValidateLength(0, 15)]
    private string _shipCountry;
    [Column("Order Date")]
    private System.Nullable<System.DateTime> _orderDate;
    [Column("Required Date")]
    private System.Nullable<System.DateTime> _requiredDate;
    [Column("Shipped Date")]
    private System.Nullable<System.DateTime> _shippedDate;
    private System.Nullable<decimal> _freight;
    [Column("Customer ID")]
    private string _customerId;
    [Column("Employee ID")]
    private System.Nullable<int> _employeeId;
    [Column("Ship Via")]
    private System.Nullable<int> _shipViaId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the ShipName entity attribute.</summary>
    public const string ShipNameField = "ShipName";
    /// <summary>Identifies the ShipAddress entity attribute.</summary>
    public const string ShipAddressField = "ShipAddress";
    /// <summary>Identifies the ShipCity entity attribute.</summary>
    public const string ShipCityField = "ShipCity";
    /// <summary>Identifies the ShipRegion entity attribute.</summary>
    public const string ShipRegionField = "ShipRegion";
    /// <summary>Identifies the ShipPostalCode entity attribute.</summary>
    public const string ShipPostalCodeField = "ShipPostalCode";
    /// <summary>Identifies the ShipCountry entity attribute.</summary>
    public const string ShipCountryField = "ShipCountry";
    /// <summary>Identifies the OrderDate entity attribute.</summary>
    public const string OrderDateField = "OrderDate";
    /// <summary>Identifies the RequiredDate entity attribute.</summary>
    public const string RequiredDateField = "RequiredDate";
    /// <summary>Identifies the ShippedDate entity attribute.</summary>
    public const string ShippedDateField = "ShippedDate";
    /// <summary>Identifies the Freight entity attribute.</summary>
    public const string FreightField = "Freight";
    /// <summary>Identifies the CustomerId entity attribute.</summary>
    public const string CustomerIdField = "CustomerId";
    /// <summary>Identifies the EmployeeId entity attribute.</summary>
    public const string EmployeeIdField = "EmployeeId";
    /// <summary>Identifies the ShipViaId entity attribute.</summary>
    public const string ShipViaIdField = "ShipViaId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Order")]
    private readonly EntityCollection<OrderDetail> _orderDetails = new EntityCollection<OrderDetail>();
    [ReverseAssociation("Orders")]
    private readonly EntityHolder<Customer> _customer = new EntityHolder<Customer>();
    [ReverseAssociation("Orders")]
    private readonly EntityHolder<Employee> _employee = new EntityHolder<Employee>();
    [ReverseAssociation("Orders")]
    private readonly EntityHolder<Shipper> _shipVia = new EntityHolder<Shipper>();


    #endregion
    
    #region Properties

    public EntityCollection<OrderDetail> OrderDetails
    {
      get { return Get(_orderDetails); }
    }

    public Customer Customer
    {
      get { return Get(_customer); }
      set { Set(_customer, value); }
    }

    public Employee Employee
    {
      get { return Get(_employee); }
      set { Set(_employee, value); }
    }

    public Shipper ShipVia
    {
      get { return Get(_shipVia); }
      set { Set(_shipVia, value); }
    }


    public string ShipName
    {
      get { return Get(ref _shipName, "ShipName"); }
      set { Set(ref _shipName, value, "ShipName"); }
    }

    public string ShipAddress
    {
      get { return Get(ref _shipAddress, "ShipAddress"); }
      set { Set(ref _shipAddress, value, "ShipAddress"); }
    }

    public string ShipCity
    {
      get { return Get(ref _shipCity, "ShipCity"); }
      set { Set(ref _shipCity, value, "ShipCity"); }
    }

    public string ShipRegion
    {
      get { return Get(ref _shipRegion, "ShipRegion"); }
      set { Set(ref _shipRegion, value, "ShipRegion"); }
    }

    public string ShipPostalCode
    {
      get { return Get(ref _shipPostalCode, "ShipPostalCode"); }
      set { Set(ref _shipPostalCode, value, "ShipPostalCode"); }
    }

    public string ShipCountry
    {
      get { return Get(ref _shipCountry, "ShipCountry"); }
      set { Set(ref _shipCountry, value, "ShipCountry"); }
    }

    public System.Nullable<System.DateTime> OrderDate
    {
      get { return Get(ref _orderDate, "OrderDate"); }
      set { Set(ref _orderDate, value, "OrderDate"); }
    }

    public System.Nullable<System.DateTime> RequiredDate
    {
      get { return Get(ref _requiredDate, "RequiredDate"); }
      set { Set(ref _requiredDate, value, "RequiredDate"); }
    }

    public System.Nullable<System.DateTime> ShippedDate
    {
      get { return Get(ref _shippedDate, "ShippedDate"); }
      set { Set(ref _shippedDate, value, "ShippedDate"); }
    }

    public System.Nullable<decimal> Freight
    {
      get { return Get(ref _freight, "Freight"); }
      set { Set(ref _freight, value, "Freight"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Customer" /> property.</summary>
    public string CustomerId
    {
      get { return Get(ref _customerId, "CustomerId"); }
      set { Set(ref _customerId, value, "CustomerId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Employee" /> property.</summary>
    public System.Nullable<int> EmployeeId
    {
      get { return Get(ref _employeeId, "EmployeeId"); }
      set { Set(ref _employeeId, value, "EmployeeId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="ShipVia" /> property.</summary>
    public System.Nullable<int> ShipViaId
    {
      get { return Get(ref _shipViaId, "ShipViaId"); }
      set { Set(ref _shipViaId, value, "ShipViaId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Products", IdColumnName="Product ID")]
  [System.ComponentModel.DataObject]
  public partial class Product : Entity<int>
  {
    #region Fields
  
    [Column("Product Name")]
    [ValidatePresence]
    [ValidateLength(0, 40)]
    private string _productName;
    [Column("English Name")]
    [ValidateLength(0, 40)]
    private string _englishName;
    [Column("Quantity Per Unit")]
    [ValidateLength(0, 20)]
    private string _quantityPerUnit;
    [Column("Unit Price")]
    private System.Nullable<decimal> _unitPrice;
    [Column("Units In Stock")]
    private System.Nullable<short> _unitsInStock;
    [Column("Units On Order")]
    private System.Nullable<short> _unitsOnOrder;
    [Column("Reorder Level")]
    private System.Nullable<short> _reorderLevel;
    private bool _discontinued;
    [Column("Category ID")]
    private System.Nullable<int> _categoryId;
    [Column("Supplier ID")]
    private System.Nullable<int> _supplierId;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the ProductName entity attribute.</summary>
    public const string ProductNameField = "ProductName";
    /// <summary>Identifies the EnglishName entity attribute.</summary>
    public const string EnglishNameField = "EnglishName";
    /// <summary>Identifies the QuantityPerUnit entity attribute.</summary>
    public const string QuantityPerUnitField = "QuantityPerUnit";
    /// <summary>Identifies the UnitPrice entity attribute.</summary>
    public const string UnitPriceField = "UnitPrice";
    /// <summary>Identifies the UnitsInStock entity attribute.</summary>
    public const string UnitsInStockField = "UnitsInStock";
    /// <summary>Identifies the UnitsOnOrder entity attribute.</summary>
    public const string UnitsOnOrderField = "UnitsOnOrder";
    /// <summary>Identifies the ReorderLevel entity attribute.</summary>
    public const string ReorderLevelField = "ReorderLevel";
    /// <summary>Identifies the Discontinued entity attribute.</summary>
    public const string DiscontinuedField = "Discontinued";
    /// <summary>Identifies the CategoryId entity attribute.</summary>
    public const string CategoryIdField = "CategoryId";
    /// <summary>Identifies the SupplierId entity attribute.</summary>
    public const string SupplierIdField = "SupplierId";


    #endregion
    
    #region Relationships

    [ReverseAssociation("Product")]
    private readonly EntityCollection<OrderDetail> _orderDetails = new EntityCollection<OrderDetail>();
    [ReverseAssociation("Products")]
    private readonly EntityHolder<Category> _category = new EntityHolder<Category>();
    [ReverseAssociation("Products")]
    private readonly EntityHolder<Supplier> _supplier = new EntityHolder<Supplier>();


    #endregion
    
    #region Properties

    public EntityCollection<OrderDetail> OrderDetails
    {
      get { return Get(_orderDetails); }
    }

    public Category Category
    {
      get { return Get(_category); }
      set { Set(_category, value); }
    }

    public Supplier Supplier
    {
      get { return Get(_supplier); }
      set { Set(_supplier, value); }
    }


    public string ProductName
    {
      get { return Get(ref _productName, "ProductName"); }
      set { Set(ref _productName, value, "ProductName"); }
    }

    public string EnglishName
    {
      get { return Get(ref _englishName, "EnglishName"); }
      set { Set(ref _englishName, value, "EnglishName"); }
    }

    public string QuantityPerUnit
    {
      get { return Get(ref _quantityPerUnit, "QuantityPerUnit"); }
      set { Set(ref _quantityPerUnit, value, "QuantityPerUnit"); }
    }

    public System.Nullable<decimal> UnitPrice
    {
      get { return Get(ref _unitPrice, "UnitPrice"); }
      set { Set(ref _unitPrice, value, "UnitPrice"); }
    }

    public System.Nullable<short> UnitsInStock
    {
      get { return Get(ref _unitsInStock, "UnitsInStock"); }
      set { Set(ref _unitsInStock, value, "UnitsInStock"); }
    }

    public System.Nullable<short> UnitsOnOrder
    {
      get { return Get(ref _unitsOnOrder, "UnitsOnOrder"); }
      set { Set(ref _unitsOnOrder, value, "UnitsOnOrder"); }
    }

    public System.Nullable<short> ReorderLevel
    {
      get { return Get(ref _reorderLevel, "ReorderLevel"); }
      set { Set(ref _reorderLevel, value, "ReorderLevel"); }
    }

    public bool Discontinued
    {
      get { return Get(ref _discontinued, "Discontinued"); }
      set { Set(ref _discontinued, value, "Discontinued"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Category" /> property.</summary>
    public System.Nullable<int> CategoryId
    {
      get { return Get(ref _categoryId, "CategoryId"); }
      set { Set(ref _categoryId, value, "CategoryId"); }
    }

    /// <summary>Gets or sets the ID for the <see cref="Supplier" /> property.</summary>
    public System.Nullable<int> SupplierId
    {
      get { return Get(ref _supplierId, "SupplierId"); }
      set { Set(ref _supplierId, value, "SupplierId"); }
    }

    #endregion
  }


  [Serializable]
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  [Table("Shippers", IdColumnName="Shipper ID")]
  [System.ComponentModel.DataObject]
  public partial class Shipper : Entity<int>
  {
    #region Fields
  
    [Column("Company Name")]
    [ValidatePresence]
    [ValidateLength(0, 40)]
    private string _companyName;

    #endregion
    
    #region Field attribute and view names
    
    /// <summary>Identifies the CompanyName entity attribute.</summary>
    public const string CompanyNameField = "CompanyName";


    #endregion
    
    #region Relationships

    [ReverseAssociation("ShipVia")]
    private readonly EntityCollection<Order> _orders = new EntityCollection<Order>();


    #endregion
    
    #region Properties

    public EntityCollection<Order> Orders
    {
      get { return Get(_orders); }
    }


    public string CompanyName
    {
      get { return Get(ref _companyName, "CompanyName"); }
      set { Set(ref _companyName, value, "CompanyName"); }
    }

    #endregion
  }



  /// <summary>
  /// Provides a strong-typed unit of work for working with the NorthwindLightSpeedModel model.
  /// </summary>
  [System.CodeDom.Compiler.GeneratedCode("LightSpeedModelGenerator", "1.0.0.0")]
  public partial class NorthwindLightSpeedModelUnitOfWork : Mindscape.LightSpeed.UnitOfWork
  {
    /// <summary>
    /// Initializes a new instance of the NorthwindLightSpeedModelUnitOfWork class.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public NorthwindLightSpeedModelUnitOfWork()
    {
    }
    

    public System.Linq.IQueryable<Category> Categories
    {
      get { return this.Query<Category>(); }
    }
    
    public System.Linq.IQueryable<Supplier> Suppliers
    {
      get { return this.Query<Supplier>(); }
    }
    
    public System.Linq.IQueryable<Customer> Customers
    {
      get { return this.Query<Customer>(); }
    }
    
    public System.Linq.IQueryable<Employee> Employees
    {
      get { return this.Query<Employee>(); }
    }
    
    public System.Linq.IQueryable<OrderDetail> OrderDetails
    {
      get { return this.Query<OrderDetail>(); }
    }
    
    public System.Linq.IQueryable<Order> Orders
    {
      get { return this.Query<Order>(); }
    }
    
    public System.Linq.IQueryable<Product> Products
    {
      get { return this.Query<Product>(); }
    }
    
    public System.Linq.IQueryable<Shipper> Shippers
    {
      get { return this.Query<Shipper>(); }
    }
    
  }

}
