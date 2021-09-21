using Core.CQRS;
using System;
using System.Threading.Tasks;
using Domain.Services;

namespace Domain.Data.Students.Commands
{
    public class DeleteStudent
    {
        public long Id { get; set; }
    }

    public class DeleteStudentExecutor : ICommandExecutor<DeleteStudent>
    {
        private readonly IStudentService _service;

        public DeleteStudentExecutor(IStudentService service)
        {
            _service = service;
        }
        public async Task ExecuteAsync(DeleteStudent command)
        {
            await _service.Delete(command.Id);
        }
    }
}
