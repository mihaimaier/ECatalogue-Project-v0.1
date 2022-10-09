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
    #region Add Methods
        /// <summary>
        /// Adds a new teacher in the system.
        /// </summary>
        /// <param name="teacherToCreate">Teacher Data</param>
        /// <returns>Created Teacher Data</returns>

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeacherToGet>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult CreateTeacher([FromBody] TeacherToCreate teacherToCreate)
        {
            return Ok(dataLayer.CreateTeacher(teacherToCreate.ToEntity()).ToDto());
        }
        /// <summary>
        /// Add a subject to a teacher.
        /// </summary>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="subjectToCreate">Subject Data</param>
        [HttpPost("{teacherId}/addsubject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult AddSubjectToTeacher([FromRoute] int teacherId, [FromBody] SubjectToCreate subjectToCreate)
        {
            try
            {
                dataLayer.AddSubjectToTeacher(teacherId, subjectToCreate.ToEntity()).ToDto();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
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
        public IActionResult DeleteTeacher([FromRoute] int teacherId, [FromQuery] bool deleteAddress, [FromQuery] bool deleteSubject)
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TeacherToUpdate>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult ChangeTeacherAddress([FromRoute] int teacherId, [Required][FromBody] AddressToUpdate newAddress)
        {
            try
            {
                dataLayer.ChangeTeacherAddress(teacherId, newAddress.ToEntity());
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
        #endregion
    #region Promotion Method
        /// <summary>
        /// Give a teacher a promotion.
        /// </summary>
        /// <param name="teacherId">Teacher Id</param>
        /// <param name="promotion">Do you wish to promote the teacher? (True = Yes / False = No)</param>
        /// <returns>Modifed rank for the teacher.</returns>
        [HttpPut("{teacherId}/promotion")]
        public IActionResult PromoteTeacher([FromRoute] int teacherId, [FromQuery] bool promotion)
        {
            try
            {
                dataLayer.PromoteTeacher(teacherId, promotion);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok();
        }
    }
}
        #endregion

