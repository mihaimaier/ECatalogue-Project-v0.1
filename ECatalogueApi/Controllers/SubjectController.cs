using ECatalogueApi.DTO;
using ECatalogueApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using ProjectOnlineCatalogue;
using ProjectOnlineCatalogue.Models;

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
    #region Add Method
        /// <summary>
        /// Adds a new subject to the system.
        /// </summary>
        /// <param name="subject">Subject Data</param>
        /// <returns>Created subject data.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubjectToGet>))]
        public IActionResult AddCourse([FromBody] SubjectToCreate subject)
        {
            return Ok(dataLayer.AddSubject(subject.Name).ToDto());
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