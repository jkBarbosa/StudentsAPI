using System.Runtime.InteropServices.WindowsRuntime;
using Domain.Models;
using System.Threading.Tasks;
using Domain.Data.Addresses.Commands;
using Domain.Repositories;

namespace Domain.Services
{
    public interface IAddressService
    {
        Task<Address> GetById(long Id);

        Task<long> CreateAsync(CreateAddress command);

        Task Delete(long id);

        Task Update(UpdateAddress command);
    }

    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> GetById(long Id)
        {
            return await _addressRepository.GetById(Id);
        }

        public async Task<long> CreateAsync(CreateAddress command)
        {
            return await _addressRepository.Create(Address.Create(command.Country, command.State, command.PostalCode,
                command.City, command.Line1, command.Line2));
        }

        public async Task Delete(long Id)
        {
            await _addressRepository.Delete(Id);
        }

        public async Task Update(UpdateAddress command)
        {
             await _addressRepository.Update(Address.Create(command.Country, command.State, command.PostalCode,
                command.City, command.Line1, command.Line2, command.Id));
        }
    }
}
