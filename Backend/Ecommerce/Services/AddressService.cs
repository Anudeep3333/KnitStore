using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services;

public class AddressService
{
    private readonly EcommerceDbContext _dbContext;

    public AddressService(EcommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Address>> GetAddresses(int userId)
    {
        return await _dbContext.Address
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Address> GetAddress(int addressId)
    {
        return await _dbContext.Address.FindAsync(addressId);
    }
    public async Task<Address> AddAddress(AddressRequest addressRequest)
    {
        Address address = new Address
        {
            UserId = addressRequest.UserId,
            City = addressRequest.City,
            State = addressRequest.State,
            Street = addressRequest.Street,
            Pincode = addressRequest.Pincode,
        };
            
        _dbContext.Address.Add(address);
        await _dbContext.SaveChangesAsync();
        return address;
    }
}