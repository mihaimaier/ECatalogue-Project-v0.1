using Abp.Domain.Entities;
using ECatalogueApi.DTO;
using ECatalogueApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProjectOnlineCatalogue;
using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ECatalogueApi.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    #region Constructor
    public class TeacherController : Controller
    {
        private readonly DataAccessLayer dataLayer;
        private readonly OnlineCatalogueDbContext context;


        public TeacherController(DataAccessLayer dataLayer, OnlineCatalogueDbContext context)
        {
            this.dataLayer = dataLayer;
            this.context = context;
        }
        #endregion
    #region Get Methods
        /// <summary>
        /// Gets all teachers in the system.
        /// </summary>
        /// <returns>List of Teachers.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeacherToGet>))]
        public IActionResult GetAllTeachers()
        {
            return Ok(dataLayer.GetAllTeachers().Select(t => t.ToDto()).ToList());
        }
        /// <summary>
        /// Gets teacher by Id.
        /// </summary>
        /// <param name="id">Please input an Id.</param>
        /// <returns>Returns the specified teacher.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeacherToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetTeacherById([FromRoute][Range(1,int.MaxValue)] int id)
        {
            TeacherToGet teacher;

            try
            {
                teacher = dataLayer.GetTeacherById(id).ToDto();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok(teacher);
        }
        /// <summary>
        /// Returns all marks by a teacher.
        /// </summary>
        /// <param name="id">Teacher's ID</param>
        /// <returns>Result</returns>
        [HttpGet("teachers/{id}/marks/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarksByTeacherToGet>))]
        public IActionResult GetAllMarksByTeacher([FromRoute][Range(1, int.MaxValue)] int id)
        {
            return Ok(dataLayer.GetMarksByTeacher(id).Select(m => m.ToDtoMarksByTeacher()).ToList());
        }
        #endregion
    #region Add Methods
        /// <summary>
        /// Adds a new teacher in the system.
        /// </summary>
        /// <param name="teacherToCreate">Teacher Data</param>
        /// <returns>Created Teacher Data</returns>

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<TeacherToGet>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult CreateTeacher([FromBody] TeacherToCreate teacherToCreate)
        {
            return Created("Successfully created",dataLayer.CreateTeacher(teacherToCreate.ToEntity()).ToDto());
        }
        /// <summary>
        /// Creates or updates a teacher's subject
        /// </summary>
        /// <param name="subjectId">Subject Id</param>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="newSubjectName">Subject's name</param>
        /// <returns>Result</returns>
        [HttpPut("{id}/update/subject")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubjectToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult ModifyTeacherSubject([FromRoute][Range(1, int.MaxValue)] int subjectId, [FromRoute][Range(1,int.MaxValue)] int teacherId, [FromBody] string newSubjectName)
        {
            SubjectToGet subject;
            try
            {
                subject = dataLayer.ModifyTeacherSubject(subjectId,teacherId, newSubjectName).ToDto();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Created("Successfully updated", subject);
        }
        #endregion
    #region Delete Method
        /// <summary>
        /// Delete a teacher from the system.
        /// </summary>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="deleteAddress">Would you like to delete the teacher's address?</param>
        /// <param name="deleteSubject">Would you like to delete the teacher's subject?</param>
        [HttpDelete("{teacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteTeacher([FromRoute][Range(1, int.MaxValue)] int teacherId, [FromQuery] bool deleteAddress, [FromQuery] bool deleteSubject)
        {
            dataLayer.DeleteTeacher(teacherId, deleteAddress, deleteSubject);
            return Ok();
        }
        #endregion
    #region Change Address For Teacher Method
        /// <summary>
        /// Change address for a specified teacher.
        /// </summary>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="newAddress">Address Data</param>
        /// <returns>Modified teacher data.</returns>
        [HttpPut("{teacherId}/addressChange")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<TeacherToGet>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult ChangeTeacherAddress([FromRoute][Range(1,int.MaxValue)] int teacherId, [Required][FromBody] AddressToUpdate newAddress)
        {
            try
            {
                dataLayer.ChangeTeacherAddress(teacherId, newAddress.ToEntity());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Created("Successfully updated",newAddress);
        }
        #endregion
    #region Promotion Method
        /// <summary>
        /// Give a teacher a promotion.
        /// </summary>
        /// <param name="teacherId">Teacher Id</param>
        /// <returns>Modifed rank for the teacher.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpPut("{teacherId}/promotion")]
        public IActionResult PromoteTeacher([FromRoute][Range(1,int.MaxValue)] int teacherId)
        {
            try
            {
                dataLayer.PromoteTeacher(teacherId);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            {
                return Ok("Teacher promoted sucessfully.");
            }
        }
    }
}
        #endregion

