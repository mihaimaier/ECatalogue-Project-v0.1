using Microsoft.EntityFrameworkCore;
using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Exceptions;
using ProjectOnlineCatalogueData.Models;

namespace ProjectOnlineCatalogue
{
    #region Constructor
    public class DataAccessLayer
    {
        private readonly OnlineCatalogueDbContext ctx;

        public DataAccessLayer(OnlineCatalogueDbContext context)
        {
            this.ctx = context;
        }
        #endregion
    #region Student Methods
        //Get All Students
        public List<Student> GetAllStudents()
        {
            return ctx.Students.Include(s => s.Address).ToList();
        }

        //Get Student By Id
        public Student GetStudentById(int studentId)
        {
            return ctx.Students.Where(s => s.Id == studentId).FirstOrDefault();
        }
        // Add Student
        public Student CreateStudent(Student studentToCreate)
        {
            var student = new Student { FirstName = studentToCreate.FirstName, LastName = studentToCreate.LastName, Age = studentToCreate.Age };
            ctx.Add(student);
            ctx.SaveChanges();
            return student;
        }
        // Delete Student
        public void DeleteStudent(int studentId, bool deleteAddress)
        {
            var student = ctx.Students.Include(student => student.Address).Where(s => s.Id == studentId).FirstOrDefault();

            if (student == null)
                return;

            if (!deleteAddress)
            {
                if (student.Address != null)
                {
                    student.Address.StudentId = null;
                    student.Address = null;
                }
            }
            else
            {
                if (student.Address != null && student.Address.TeacherId != null)
                {
                    ctx.Remove(student.Address);
                }
            }
            ctx.Remove(student);
            ctx.SaveChanges();
        }
        //Change Student Address
        public void ChangeStudentAddress(int studentId, Address newAddress)
        {
            var student = ctx.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                throw new EntityNotFoundException($"A student with a student id {studentId} was not found");
            }
            if (student.Address == null)
            {
                student.Address = new Address();
            }
            student.Address.City = newAddress.City;
            student.Address.Street = newAddress.Street;
            student.Address.Number = newAddress.Number;

            ctx.SaveChanges();
        }
        //Change Student Data
        public void ChangeStudentData(int studentId, Student newStudentData)
        {
            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);

            if (student == null)
            {
                throw new EntityNotFoundException($"A student with an id {studentId} was not found.");
            }
            student.FirstName = newStudentData.FirstName;
            student.LastName = newStudentData.LastName;
            student.Age = newStudentData.Age;

            ctx.SaveChanges();
        }
        // Add Marks to Student
        public void AddMarkToStudent(int studentId, int markValue)
        {
            var student = ctx.Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                throw new EntityNotFoundException($"Student {studentId} is null");
            }
            student.Marks.Add(new Mark { Value = markValue, CreationDate = DateTime.Now });
            ctx.SaveChanges();
        }
        #endregion
    #region Teacher Methods
        // Add Teacher
        public Teacher CreateTeacher(Teacher teacherToCreate)
        {
            var teacher = new Teacher { Name = teacherToCreate.Name, Rank = teacherToCreate.Rank, Address = teacherToCreate.Address, Subject = teacherToCreate.Subject };
            ctx.Add(teacher);
            ctx.SaveChanges();
            return teacher;
        }
        //Delete Teacher
        public void DeleteTeacher(int teacherId, bool deleteAddress, bool deleteSubject)
        {
            var teacher = ctx.Teachers.Include(teacher => teacher.Address).Where(s => s.Id == teacherId).FirstOrDefault();
            if (teacher == null)
                return;
            if (!deleteAddress)
            {
                if (teacher.Address != null)
                {
                    teacher.Address.StudentId = null;
                    teacher.Address = null;
                }
            }
            if (!deleteSubject)
            {
                if (teacher.Subject != null)
                {
                    teacher.Subject = null;
                }
            }
            else
            {
                if (teacher.Address != null && teacher.Subject != null)
                {
                    ctx.Remove(teacher.Address);
                    ctx.Remove(teacher.Subject);
                }
            }
            ctx.Remove(teacher);
            ctx.SaveChanges();
        }
        // Change Teacher Address
        public void ChangeTeacherAddress(int teacherId, Address newAddress)
        {
            var teacher = ctx.Teachers.Include(s => s.Address).FirstOrDefault(s => s.Id == teacherId);
            if (teacher == null)
            {
                throw new EntityNotFoundException($"A teacher with a teacher id {teacherId} was not found");
            }
            if (teacher.Address == null)
            {
                teacher.Address = new Address();
            }
            teacher.Address.City = newAddress.City;
            teacher.Address.Street = newAddress.Street;
            teacher.Address.Number = newAddress.Number;

            ctx.SaveChanges();
        }
        //Promotion of Teacher
        public void PromoteTeacher(int teacherId, bool promotion)
        {
            var teacher = ctx.Teachers.FirstOrDefault(s => s.Id == teacherId);
            if (teacher == null)
            {
                throw new EntityNotFoundException($"Teacher {teacherId} is null");
            }
            if (promotion)
            {
                teacher.Rank++;
            }
            else
            {
                teacher.Rank--;
            }
            ctx.SaveChanges();
        }
        #endregion
    #region Subject Methods
        //Delete Subject
        public void DeleteSubject(int subjectId)
        {
            var subject = ctx.Subjects.Where(s => s.Id == subjectId).FirstOrDefault();
            if (subject == null)
                return;

            if(subject.Name != null)
                {
                    ctx.Remove(subject.TeacherId);
                }
            ctx.Remove(subject);
            ctx.SaveChanges();
        }
        // Add Subject
        public Subject AddSubject(string subjectName)
        {
            var subject = new Subject {Name = subjectName};
            ctx.Subjects.Add(subject);
            ctx.SaveChanges();
            return subject;
        }
        //Add Subject to Teacher
        public Subject AddSubjectToTeacher(int teacherId, Subject subjectToCreate)
        {
            var teacher = ctx.Teachers.FirstOrDefault(s => s.Id == teacherId);
            if(teacher == null)
            {
                throw new EntityNotFoundException($"Teacher {teacherId} is null");
            }
            var subject = new Subject { Name = subjectToCreate.Name };
            ctx.Add(subject);
            ctx.SaveChanges();
            return subject;
        }
    }
}
#endregion
