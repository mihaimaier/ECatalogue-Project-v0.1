using Microsoft.EntityFrameworkCore;
using ProjectOnlineCatalogue.Models;
using ProjectOnlineCatalogueData.Exceptions;
using ProjectOnlineCatalogueData.Models;
using System.Dynamic;

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

            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new StudentDoesNotExistsException(studentId);
            }

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
            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new StudentDoesNotExistsException(studentId);
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
        public Student ChangeStudentData(int studentId, Student student)
        {

            if (!ctx.Students.Any(s => s.Id == studentId))
            {
                throw new StudentDoesNotExistsException (studentId);
            }
            Student studentToModify = ctx.Students.First(s => s.Id == studentId);

            studentToModify.FirstName = student.FirstName;
            studentToModify.LastName = student.LastName;
            studentToModify.Age = student.Age;

            ctx.SaveChanges();
            return studentToModify;
        }
        // Add Marks to Student
        public Mark AddMarkToStudent(Mark newMark)
        {
            if (!ctx.Students.Any(s => s.Id == newMark.StudentId))
            {
                throw new StudentDoesNotExistsException(newMark.StudentId);
            }

            if (!ctx.Subjects.Any(s => s.Id == newMark.SubjectId))
            {
                throw new SubjectDoesNotExistException(newMark.SubjectId);
            }

            ctx.Marks.Add(newMark);
            ctx.SaveChanges();
            return newMark;
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
        public void RemoveTeacher(int teacherId)
        {
            if (!ctx.Teachers.Any(t => t.Id == teacherId))
            {
                throw new TeacherDoesNotExistException(teacherId);
            }
            Teacher existingTeacher = ctx.Teachers.Include(t => t.Address).Include(t => t.Subject).First(t => t.Id == teacherId);
            RemoveSubject(existingTeacher.Subject);

            ctx.Teachers.Remove(existingTeacher);
            ctx.SaveChanges();
        }
        // Change Teacher Address
        public void ChangeTeacherAddress(int teacherId, Address newAddress)
        {
            var teacher = ctx.Teachers.Include(s => s.Address).FirstOrDefault(s => s.Id == teacherId);
            if (!ctx.Teachers.Any(t => t.Id == teacherId))
            {
                throw new TeacherDoesNotExistException(teacherId);
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
        public void PromoteTeacher(int teacherId)
        {
            var teacher = ctx.Teachers.FirstOrDefault(s => s.Id == teacherId);
            if (!ctx.Teachers.Any(t => t.Id == teacherId))
            {
                throw new TeacherDoesNotExistException(teacherId);
            }
            if (teacher != null)
            {
                teacher.Rank++;
            }
            if(teacher.Rank == Rank.Professor)
            {
                _ = teacher.Rank;
            }
            ctx.SaveChanges();
        }
        //Get Marks By Teacher
        public List<Mark> GetMarksByTeacher(int teacherId)
        {
            return ctx.Marks.Where(m => m.Id == teacherId).ToList();
        }
        //Gets all teachers
        public List<Teacher> GetAllTeachers()
        {
            return ctx.Teachers.Include(t => t.Address).Include(t => t.Subject).ToList();
        }
        //Get Teacher By Id
        public Teacher GetTeacherById(int teacherId)
        {
            if (!ctx.Teachers.Any(t => t.Id == teacherId))
            {
                throw new EntityNotFoundException(teacherId);
            }
            return ctx.Teachers.Include(t => t.Address).Include(t => t.Subject).First(t => t.Id == teacherId);
        }

        #endregion
    #region Subject Methods
        // Get All Subjects
        public List<Subject> GetAllSubjects()
        {
            return ctx.Subjects.ToList();
        }
        // Get Subject By Id
        public Subject GetSubjectById(int subjectId)
        {
            if (!ctx.Subjects.Any(s => s.Id == subjectId))
            {
                throw new SubjectDoesNotExistException(subjectId);
            }
            return ctx.Subjects.First(s => s.Id == subjectId);
        }
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
        // Assign or Update a Subject
        public Subject AssignTeacherSubject(int teacherId, Subject newSubject)
        {
            if (!ctx.Teachers.Any(t => t.Id == teacherId))
            {
                throw new TeacherDoesNotExistException(teacherId);
            }

            Teacher existingTeacher = ctx.Teachers.Include(t => t.Subject).First(t => t.Id == teacherId);
            RemoveSubject(existingTeacher.Subject);

            existingTeacher.Subject = newSubject;
            ctx.SaveChanges();
            return newSubject;
        }
        //Remove Subject
        private void RemoveSubject(Subject subject)
        {
            if (subject != null)
            {
                ctx.Marks.RemoveRange(ctx.Marks.Where(m => m.SubjectId == subject.Id));
                ctx.Subjects.Remove(subject);
            }

        }
    }
}
#endregion
