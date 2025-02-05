using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class ProductService
{
    private readonly EcommerceDbContext _dbContext;

    public ProductService(EcommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool AddProduct(Product product, out string errorMessage)
    {
        errorMessage = string.Empty;
        try
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            return false;
        }
    }
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _dbContext.Products.FindAsync(id);
    }

    public List<Product> getProducts()
    {
        try
        {
            return _dbContext.Products.ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    
}