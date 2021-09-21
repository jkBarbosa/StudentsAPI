using Core.CQRS;
using Domain.Models;
using Domain.Services;
using System.Threading.Tasks;

namespace Domain.Data.Students.Queries
{
    public class GetStudent : IQuery<Student>
    {
        public long Id { get; set; }
    }

    public class GetStudentExecutor : IQueryHandler<GetStudent, Student>
    {
        private readonly IStudentService _service;

        public GetStudentExecutor(IStudentService service)
        {
            _service = service;
        }
        public async Task<Student> ExecuteAsync(GetStudent query)
        {
            return await _service.GetById(query.Id);
        }
    }
}
