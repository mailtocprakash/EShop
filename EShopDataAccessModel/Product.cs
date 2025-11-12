namespace EShopDataAccessModel
{
    public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class ProductService
{
    public static List<Product> GetMultipleProducts(string DBEnvironment)
    {
        // Create a new list of Product
        List<Product> products = new List<Product>();

        // Add multiple Product items to the list
        products.Add(new Product { Id = 1, Name = "Laptop", Price = 1200.00m });
        products.Add(new Product { Id = 2, Name = "Mouse", Price = 25.50m });
        products.Add(new Product { Id = 3, Name = "Keyboard", Price = 75.00m });

        if(DBEnvironment == "DBLocal")
            {
                products.Add(new Product { Id = 4, Name = "ProductLocal", Price = 35.00m });
            }
        else if(DBEnvironment == "DBStaging")
            {
                products.Add(new Product { Id = 4, Name = "ProductStaging", Price = 35.00m });
            }
        else if(DBEnvironment == "DBProd")
            {
                products.Add(new Product { Id = 4, Name = "ProductProd", Price = 35.00m });
            }

        // Return the populated list
        return products;
    }
}
}
