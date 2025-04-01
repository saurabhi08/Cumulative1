using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cumulative01.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace Cumulative01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TeacherAPIController : ControllerBase
    {

        // Private field to store database context
        private readonly SchoolDbContext _context;


        // Constructor to initialize database context
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        // GET API to fetch all teachers
        [HttpGet(template:"Teacher")]

        public List<Teacher> RetrieveTeacherList()
        {
            List<Teacher> teachers = new List<Teacher>();
            MySqlConnection Connection = _context.GetConnection();
            Connection.Open();
            Debug.WriteLine("DbConnected");
            string SQLQuery = "SELECT * FROM teachers";
 
            MySqlCommand Command = Connection.CreateCommand();
        
            Command.CommandText = SQLQuery;
            
            MySqlDataReader DataReader = Command.ExecuteReader();

            while (DataReader.Read())
            {
             
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFirstName = TeacherFName;
                newTeacher.TeacherLastName = TeacherLName;
                newTeacher.EmployeeID = EmployeeID;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;
                teachers.Add(newTeacher);
            }
            Connection.Close();
            return teachers;
        }


        // GET API to fetch a teacher by ID
        [HttpGet]
        [Route(template: "SearchTeacher/{id}")]

        public Teacher SearchTeacher(int id)
        {
            Teacher teacher = new Teacher();
            MySqlConnection Connection = _context.GetConnection();
            Connection.Open();

            string SQL = "Select * FROM teachers Where Teacherid = "+id.ToString();
            MySqlCommand Command = Connection.CreateCommand();
            Command.CommandText = SQL;
            MySqlDataReader DataReader = Command.ExecuteReader();
            while (DataReader.Read())
            {
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                teacher.TeacherId = TeacherId;
                teacher.TeacherFirstName = TeacherFName;
                teacher.TeacherLastName = TeacherLName;
                teacher.EmployeeID = EmployeeID;
                teacher.HireDate = HireDate;
                teacher.Salary = Salary;
            }

            Connection.Close(); 
            return teacher;
        }

        // GET API to fetch teachers hired within a specific date range
        [HttpGet]
        [Route(template: "searchbydate")]
        public List<Teacher> searchbydate([FromQuery] string Start, [FromQuery] string End)
        {

            DateTime startDate = DateTime.Parse(Start);
            DateTime endDate = DateTime.Parse(End);

            List<Teacher> teachers = new List<Teacher>();

            MySqlConnection Connection = _context.GetConnection();

            Connection.Open();

            string SQL = "SELECT * FROM `teachers` WHERE teachers.hiredate BETWEEN '"
                  + startDate.ToString("yyyy-MM-dd") + "' AND '"
                  + endDate.ToString("yyyy-MM-dd") + "'";

            MySqlCommand Command = Connection.CreateCommand();

            Command.CommandText = SQL;

            MySqlDataReader DataReader = Command.ExecuteReader();

            while (DataReader.Read())
            {
                int TeacherId = Convert.ToInt32(DataReader["teacherid"]);
                string TeacherFName = DataReader["teacherfname"].ToString();
                string TeacherLName = DataReader["teacherlname"].ToString();
                string EmployeeID = DataReader["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(DataReader["hiredate"]);
                double Salary = Convert.ToDouble(DataReader["salary"]);

                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFirstName = TeacherFName;
                newTeacher.TeacherLastName = TeacherLName;
                newTeacher.EmployeeID = EmployeeID;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;
                teachers.Add(newTeacher);
            }

            return teachers;

        }





    }
}
