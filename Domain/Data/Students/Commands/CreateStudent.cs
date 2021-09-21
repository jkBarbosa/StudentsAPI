using System;
using Core.CQRS;
using Domain.Data.Addresses.Commands;
using Domain.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Domain.Data.Students.Commands
{
    public class CreateStudent
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public CreateAddress Address { get; set; }

    }

    public class CreateStudentExecutor : ICommandExecutor<CreateStudent>
    {
        private readonly IStudentService _service;
        public CreateStudentExecutor(IStudentService service)
        {
            _service = service;
        }
        public async Task ExecuteAsync(CreateStudent command)
        {
            command.Id = await _service.CreateAsync(command);
        }

    }
}
