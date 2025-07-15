using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventBookingDataAccess;
using EBS_Business;

namespace EBS_API.Controllers
{
    [Route("api/Event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        [HttpGet("GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DTOEvent>> GetAllEvents()
        {
            List<DTOEvent> events = clsEvent.GetAllEvents();
            if(events.Count>0)
                return Ok(events);
            return NotFound("No events found.");
        }

        [HttpDelete("DeleteEvent{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool>DeleteEvent(int ID)
        {
            if (clsEvent.Delete(ID))
                return Ok(true);
            return BadRequest(false);
        }

    }
}
