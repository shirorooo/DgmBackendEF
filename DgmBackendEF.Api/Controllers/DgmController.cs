using DgmBackendEF.Data.Models;
using DgmBackendEF.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DgmBackendEF.Api.Controllers
{
    [Route("api/attendee")]
    [ApiController]
    public class DgmController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<DgmController> _logger;

        public DgmController(IPersonRepository personRepository, ILogger<DgmController> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        /*
         * ADD NEW ATTENDEE PROFILE TO THE DATABASE
         */
        [HttpPost]
        public async Task<IActionResult> addAttendeeProfile(Person person)
        {
            try
            {
                var profileRecord = await _personRepository.getAttendeeById(person.Id);
                /*
                 * CHECK FIRST IF THE ATTENDEE DOES NOT EXIST.
                 * IF RECORD EXIST, THROW AN ERROR INFORMING THE FE SIDE
                 * THAT THE PROFILE IS ALREADY RECORDED.
                 */
                if (profileRecord != null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Profile does not exist in the database yet"
                    });
                }

                /*
                 * IF THE DATA IS NOT YET RECORDED, ADDED IT
                 * TO THE DATABASE.
                 */
                var addedPerson = await _personRepository.createAttendeeProfile(person);
                return CreatedAtAction(nameof(addAttendeeProfile), addedPerson);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /*
         * UPDATE ATTENDEE PROFILE TO THE DATABASE
         */
        [HttpPut]
        public async Task<IActionResult> updateAttendeeProfile(Person person)
        {
            try
            {
                var profileRecord = await _personRepository.getAttendeeById(person.Id);
                /*
                 * CHECK FIRST IF THE ATTENDEE ALREADY EXIST.
                 * IF NOT, THROW AN ERROR INFORMING THE FE SIDE
                 * THAT THE PROFILE IS NOT YET RECORDED.
                 */
                if (profileRecord == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Profile does not exist in the database yet"
                    });
                }
                /*
                 * IF THE PROFILE EXIST IN THE DATABASE, PROCEED
                 * TO UPDATE.
                 */
                await _personRepository.updateAttendeeProfile(person);
                return Ok("Attendee profile updated successfully");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /*
         * DELET A RECORD OF AN ATTENDEE
         */
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteAttendeeProfile(int id)
        {
            try
            {
                var profileRecord = await _personRepository.getAttendeeById(id);
                /*
                 * CHECK FIRST IF THE ATTENDEE ALREADY EXIST.
                 * IF NOT, THROW AN ERROR INFORMING THE FE SIDE
                 * THAT THE PROFILE IS NOT YET RECORDED.
                 */
                if (profileRecord == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Profile does not exist in the database yet"
                    });
                }
                /*
                 * IF THE PROFILE EXIST IN THE DATABASE, PROCEED
                 * TO DELETE.
                 */
                await _personRepository.deleteAttendeeProfile(profileRecord);
                return Ok("Data removed successfully!");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /*
         * FETCH ALL THE RECORDED DATA OF THE ATTENDEES
         * FROM THE DATABASE.
         */
        [HttpGet]
        public async Task<IActionResult> getAttendeeList()
        {
            try
            {
                var attendees = await _personRepository.getAttendeeList();
                return Ok(attendees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /*
         * FIND ATTENDEE PROFILE USING THE USER ID.
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> getAttendeeById(int id)
        {
            try
            {
                var attendee = await _personRepository.getAttendeeById(id);
                /*
                 * CHECK IF THE ATTENDEE EXIST. IF NOT
                 * INFORM THE FE SIDE THAT THE PROFILE IS
                 * NOT YET CREATED.
                 */
                if (attendee == null)
                {
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = "Profile does not exist in the database yet"
                    });
                }

                /*
                 * IF THE ATTENDEE PROFILE ALREADY EXIST,
                 * RETURN THE DATA FETCHED FROM BE TO FE
                 */
                return Ok(attendee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
