using DomainLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IService;

namespace schedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesEvent : ControllerBase
    {
        private readonly IEventserv<Event> _customService;   //Service for managing employee entities
        private readonly appDbContext _applicationDbContext;   //Database context for additional operations
        public ValuesEvent(IEventserv<Event> customService, appDbContext applicationDbContext)
        {
            _customService = customService;
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        [Route("AddEvent")]
        public ActionResult<IEnumerable<Event>> AddEvent([FromBody] List<Event> newEvents)
        {
            return Ok(_customService.AddEvents(newEvents));
        }
    

        [HttpDelete(nameof(DeleteEvent))]
        public IActionResult DeleteEvent([FromBody] Event eventToDelete)
        {
            try
            {
                _customService.DeleteEvent(eventToDelete);
                return Ok("Event deleted successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPut(nameof(UpdateEvent))]
        public IActionResult UpdateEvent([FromBody] Event updatedEvent)
        {
            try
            {
                _customService.UpdateEvent(updatedEvent);
                return Ok(new { message = "Event updated successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = $"An error occurred: {ex.Message}" });
            }
        }
    }
}
