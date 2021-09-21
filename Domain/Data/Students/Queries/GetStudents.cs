using Core.CQRS;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Services;

namespace Domain.Data.Students.Queries
{
    public class GetStudents : IQuery<IEnumerable<Student>>
    {
    }

    public class GetStudentsExecutor : IQueryHandler<GetStudents, IEnumerable<Student>>
    {
        private readonly IStudentService _service;

        public GetStudentsExecutor(IStudentService service)
        {
            _service = service;
        }
        public async Task<IEnumerable<Student>> ExecuteAsync(GetStudents query)
        {
            return await _service.GetAll();
        }
    }
}
