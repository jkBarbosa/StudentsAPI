using Core.CQRS;
using Domain.Data.Addresses.Commands;
using Domain.Services;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Domain.Data.Students.Commands
{
    public class UpdateStudent
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public UpdateAddress Address { get; set; }

    }

    public class UpdateStudentExecutor : ICommandExecutor<UpdateStudent>
    {
        private readonly IStudentService _service;
        public UpdateStudentExecutor(IStudentService service)
        {
            _service = service;
        }
        public async Task ExecuteAsync(UpdateStudent command)
        {
            await _service.Update(command);
        }

    }
}
