using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Repositories
{
    public class AddressRepository: IAddressRepository
    {

        private readonly StudentsAppDbContext _context;

        public AddressRepository(StudentsAppDbContext context)
        {
            _context = context;
        }

        public async Task<long> Create(Address model)
        {
            await _context.Addresses.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;

        }

        public async Task<Address> GetById(long id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Address>> GetAll()
        {
            return (await _context.Addresses.ToArrayAsync()).ToList();
        }

        public async Task Update(Address model)
        {
            var existingModel = await GetById(model.Id);
            existingModel.City = model.City;
            existingModel.Country = model.Country;
            _context.Addresses.Update(existingModel);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var model = _context.Addresses.FirstOrDefault(p => p.Id.Equals(id));
            _context.Addresses.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
