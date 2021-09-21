using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace EntityFramework.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentsAppDbContext _context;

        public StudentRepository(StudentsAppDbContext context)
        {
            _context = context;
        }

        public async Task<long> Create(Student model)
        {
            await _context.Students.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;

        }

        public async Task<Student> GetById(long id)
        {
            return await _context.Students.Include(p => p.Address).FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return (await _context.Students.Include(p => p.Address).ToArrayAsync()).ToList();
        }

        public async Task Update(Student model)
        {
            var existingEntity = await GetById(model.Id);
            existingEntity.BirthDate = model.BirthDate;
            existingEntity.Name = model.Name;
            _context.Students.Update(existingEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var model = _context.Students.FirstOrDefault(p => p.Id.Equals(id));
            _context.Students.Remove(model);
            await _context.SaveChangesAsync();
        }
    }
}
