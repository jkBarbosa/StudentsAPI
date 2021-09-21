using System.Collections.Generic;
using Core.CQRS;
using Domain.Data.Students.Commands;
using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Logger;
using Domain.Data.Students.Queries;
using Microsoft.AspNetCore.Http;

namespace Api.Rest.Controllers
{
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repository;
        private readonly IQueryExecutor _queryExecutor;
        private readonly ICommandSender _commandSender;
        private readonly ILoggerManager _logger;
        public StudentController(IStudentRepository repository, IQueryExecutor queryExecutor, ICommandSender commandSender,  ILoggerManager logger)

        {
            _repository = repository;
            _queryExecutor = queryExecutor;
            _commandSender = commandSender;
            _logger = logger;
        }

        [HttpGet("{id:long}", Name = "GetStudent")]
        [Produces(typeof(Student))]
        public async Task<Student> GetById([FromRoute] long id)
        {
            return await _repository.GetById(id);
        }

        [HttpPost("", Name = "CreateStudent")]
        [ProducesResponseType(typeof(Student), 201)]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudent command)
        {
            await _commandSender.SendAsync(command);
            var existingEntity = await _repository.GetById(command.Id);
            return base.CreatedAtRoute("GetStudent",new { id = command.Id },existingEntity);
        }

        [HttpGet("", Name = "GetStudents")]
        [Produces(typeof(IEnumerable<Student>))]
        public async Task<IActionResult> GetStudents()
        {
            _logger.LogInfo("Here is info message from the controller.");
            return Ok((await _queryExecutor.ExecuteAsync(new GetStudents())));
        }

        [HttpPost("{id:long}", Name = "UpdateStudent")]
        [Produces(typeof(Student))]
        public async Task<IActionResult> UpdateStudent([FromRoute] long id, [FromBody] UpdateStudent command)
        {
            command.Id = id;
            await _commandSender.SendAsync(command);
            var existingEntity = await _repository.GetById(command.Id);
            return Ok(existingEntity);
        }

        [HttpDelete("{id:long}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteBatch([FromRoute] long id)
        {
            await _commandSender.SendAsync(new DeleteStudent() { Id = id });
            return NoContent();
        }

    }
}
