using System;
using Domain.Data.Students.Commands;
using Domain.Models;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IStudentService
    {
        Task<long> CreateAsync(CreateStudent command);
        Task<IEnumerable<Student>> GetAll();
        Task Update(UpdateStudent command);
        Task<Student> GetById(long id);

        Task Delete(long id);
    }
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAddressService _addressService;

        public StudentService(IStudentRepository studentRepository,
            IAddressService addressService)
        {
            _studentRepository = studentRepository;
            _addressService = addressService;
        }

        public async Task<long> CreateAsync(CreateStudent command)
        {
            var addressId = await _addressService.CreateAsync(command.Address);

            return await _studentRepository.Create(Student.Create(command.Name, command.BirthDate ?? DateTime.Now, addressId, 0));
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _studentRepository.GetAll();
        }

        public async Task Update(UpdateStudent command)
        {
            var existingModel = await GetById(command.Id);
            command.Address.Id = existingModel.AddressId;
            await _addressService.Update(command.Address);
            await _studentRepository.Update(Student.Create(command.Name, command.BirthDate, command.Address.Id, command.Id));
        }

        public async Task<Student> GetById(long id)
        {
            return await _studentRepository.GetById(id); 
        }

        public async Task Delete(long id)
        {
            var entity = await _studentRepository.GetById(id);
            await _addressService.Delete(entity.AddressId);
        }
    }
}
