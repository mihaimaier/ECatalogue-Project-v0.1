using Microsoft.AspNetCore.Mvc;
using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogue;
using ECatalogueApi.DTO;
using ECatalogueApi.Extensions;
using System.ComponentModel.DataAnnotations;
using ProjectOnlineCatalogueData.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Abp.Domain.Entities;

namespace ECatalogueApi.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    #region Constructor
    public class StudentController : Controller
    {

        private readonly DataAccessLayer dataLayer;
        private readonly OnlineCatalogueDbContext context;

        public StudentController(DataAccessLayer datalayer, OnlineCatalogueDbContext context)
        {
            this.dataLayer = datalayer;
            this.context = context;
        }
        #endregion
    #region Get Methods
        /// <summary>
        /// Gets all students with their addresses.
        /// </summary>
        /// <returns>Returns a student list.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGet>))]
        public IActionResult GetAllStudents() =>
        Ok(dataLayer.GetAllStudents().Select(s => s.ToDto()).ToList());

        /// <summary>
        /// Gets student by Id.
        /// </summary>
        /// <param name="studentId">Insert A Student Id</param>
        /// <returns>Student specified by Id given.</returns>
        [HttpGet("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentToGet>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetById([FromRoute][Range(1, int.MaxValue)] int studentId)
        {
            var student = dataLayer.GetStudentById(studentId);
            if (student == null)
            {
                return NotFound($"Student With ID: {studentId} not found.");
            }
            return Ok(student.ToDto());
        }
        /// <summary>
        /// Gets all marks for a student for a specified subject.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="subjectId">Subject Id</param>
        /// <returns>List of Marks</returns>
        [HttpGet("{studentId}/marks")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MarkToGet>))]
        public IActionResult GetAllMarks([FromRoute][Range(1, int.MaxValue)] int studentId, [FromQuery][Optional][Range(1, int.MaxValue)] int subjectId)
        {
            var student = context.Students.Include(s => s.Marks).FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return NotFound($"Student with ID: {studentId} not found");
            }
            if (subjectId != null)
            {
                return Ok(student.Marks.Select(m => m.ToDto()).ToList());
            }
            else
            {
                return Ok(student.Marks.Where(m => m.SubjectId == subjectId).Select(m => m.ToDto()).ToList());
            }
        }
        /// <summary>
        /// Gets average per subject for a student.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <returns>List of Averages per Subject</returns>
        [HttpGet("{studentId}/averages/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AverageForSubject>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetAveragePerSubject([FromRoute][Range(1, int.MaxValue)] int studentId)
        {
            var student = context.Students.Include(s => s.Marks).FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(student.Marks.GroupBy(m => m.SubjectId).Select(
                g => new AverageForSubject { SubjectId = g.Key, Average = g.Average(m => m.Value) }).ToList());
        }

        /// <summary>
        /// Gets the students marks ordered by the average.
        /// </summary>
        /// <param name="order">Descending Order(Select True) / Ascending Order (Select False)</param>
        /// <returns></returns>
        [HttpGet("all/orderByAverage")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<StudentWithAverageToGet>))]
        public IActionResult GetAllStudentOrderedByAverage([Optional][FromQuery] bool order)
        {
            var allStudentsWithMarks = context.Students.Include(s => s.Marks).ToList();
            List<StudentWithAverageToGet> result;
            if (order)
            {
                result = allStudentsWithMarks.OrderByDescending(s => s.Marks.Average(m => m.Value)).Select(s => new StudentWithAverageToGet(
                s.Id,
                s.FirstName + s.LastName,
                s.Age,
                s.Marks.Average(m => m.Value)
                )).ToList();
            }
            else
            {
                result = allStudentsWithMarks.OrderBy(s => s.Marks.Average(m => m.Value)).Select(s => new StudentWithAverageToGet(
                s.Id,
                s.FirstName + s.LastName,
                s.Age,
                s.Marks.Average(m => m.Value)
                )).ToList();
            }
            return Ok(result);
        }
        #endregion
    #region Update Methods

        /// <summary>
        /// Update student data.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="newStudentData">Student Data</param>
        /// <returns>Updated student data.</returns>
        [HttpPut("{studentId}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<StudentToGet>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult UpadateStudent([FromRoute][Range(1,int.MaxValue)] int studentId, [FromBody] StudentToUpdate newStudentData)
        {
            try
            {
                dataLayer.ChangeStudentData(studentId, newStudentData.ToEntity());
            }
            catch (StudentDoesNotExistsException e)
            {
                return NotFound(e.Message);
            }
            return Created("Successfully updated",newStudentData);
        }

        /// <summary>
        /// Updates or creates a student's address information.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="newAddress">New Address</param>
        /// <returns>Modified student data.</returns>
        [HttpPut("{studentId}/address")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<AddressToGet>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult ChangeStudentAddress([FromRoute][Range(1,int.MaxValue)] int studentId, [Required][FromBody] AddressToUpdate newAddress)
        {
            try
            {
                dataLayer.ChangeStudentAddress(studentId, newAddress.ToEntity());
            }
            catch (StudentDoesNotExistsException e)
            {
                return NotFound(e.Message);
            }
            return Created("Successfully updated",newAddress);
        }
        #endregion
    #region Create/Addition Methods
        
        /// <summary>
        /// Creates a student based on data provided.
        /// </summary>
        /// <param name="studentToCreate">Student Data</param>
        /// <returns>New Created Student.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<StudentToGet>))]
        public IActionResult CreateStudent([FromBody] StudentToCreate studentToCreate)
        {
            return Created("Successfully Created",dataLayer.CreateStudent(studentToCreate.ToEntity()).ToDto());
        }
        /// <summary>
        /// Add a mark to a student.
        /// </summary>
        /// <param name="newMark">Mark Data</param>
        /// <returns>Result</returns>
        [HttpPost("marks/create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MarkToGet))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult AddMarkToStudent([FromBody] MarksToCreate newMark)
        {
            MarkToGet mark;
            try
            {
                mark = dataLayer.AddMarkToStudent(newMark.ToEntity()).ToDto();
            }
            catch (StudentDoesNotExistsException e)
            {
                return NotFound(e.message);
            }
            catch (SubjectDoesNotExistException e)
            {
                return NotFound(e.message);
            }
            return Created("Mark Sucessfully Added", mark);
        }
        #endregion
    #region Delete Method
        /// <summary>
        /// Deletes a student based on Id.
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <param name="deleteAddress">Do you want to delete the address?</param>
        /// <returns>Removed Student</returns>
        [HttpDelete("{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteStudent([FromRoute][Range(1,int.MaxValue)] int studentId, [FromQuery] bool deleteAddress)
        {
            try
            {
                dataLayer.DeleteStudent(studentId, deleteAddress);
            }
            catch (StudentDoesNotExistsException e)
            {
                return NotFound(e.message);
            }
            return Ok("Student Successfully Removed.");
        }
    }
}
        #endregion