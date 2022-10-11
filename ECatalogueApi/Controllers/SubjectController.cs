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
    public class SubjectController : Controller
    {
        private readonly DataAccessLayer dataLayer;
        private readonly OnlineCatalogueDbContext context;

        public SubjectController(DataAccessLayer dataLayer, OnlineCatalogueDbContext context)
        {
            this.dataLayer = dataLayer;
            this.context = context;
        }
        #endregion
    #region Get Method
        /// <summary>
        /// Returns a subject by a specified Id.
        /// </summary>
        /// <param name="id">Subject's ID</param>
        /// <returns>Result</returns>
        [HttpGet("subjects/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubjectToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetSubjectById([FromRoute][Range(1, 200)] int id)
        {
            SubjectToGet subject;

            try
            {
                subject = dataLayer.GetSubjectById(id).ToDto();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return Ok(subject);
        }
        #endregion
    #region Create Method
        /// <summary>
        /// Creates or updates a subject.
        /// </summary>
        /// <param name="subject">Subject Data</param>
        /// <returns>Created subject data.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<SubjectToGet>))]
        public IActionResult AddCourse([FromBody] SubjectToCreate subject)
        {
            return Created("Successfully Created", dataLayer.AddSubject(subject.ToEntity()).ToDto());
        }
        #endregion
    #region Delete Method
        /// <summary>
        /// Deletes a subject based on Id.
        /// </summary>
        /// <param name="subjectId">Subject Id</param>
        /// <returns>Removed Subject</returns>
        [HttpDelete("{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteSubject([FromRoute] int subjectId)
        {
            dataLayer.DeleteSubject(subjectId);
            return Ok();
        }
    }
}
        #endregion