using studentExercises.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace studentExercises.Data
{
    public class StudentRepository
    {
        ExerciseRepository exerciseRepo = new ExerciseRepository();
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
        //Get all students
        public List<Student> GetAllStudents()
        {
            
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.Id, s.FirstName, s.LastName, s.SlackHandle, c.Name FROM Student s 
                                        INNER JOIN Cohort c ON s.CohortId = c.Id";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Student> students = new List<Student>();
                    while(reader.Read())
                    {
                        
                        Student student = new Student
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            Cohort = new Cohort
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }
                        };
                        student.Exercise = exerciseRepo.GetAllExerciseByStudentId(reader.GetInt32(reader.GetOrdinal("Id")));
                        students.Add(student);

                    }
                    reader.Close();
                    return students;
                }
            }
        }

        //Get Students by Id
        public Student GetStudentById(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT s.FirstName, s.LastName, c.Name FROM Student s
                                        INNER JOIN Cohort c ON s.CohortId = c.Id
                                        WHERE s.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    Student student = null;

                    if(reader.Read())
                    {
                        student = new Student
                        {
                            Id = id,
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            Cohort = new Cohort
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                            }

                        };
                        student.Exercise = exerciseRepo.GetAllExerciseByStudentId(id);
                    }
                    reader.Close();
                    return student;
                }
            }
        }

        public void AddExercise(int studentId, int exerciseId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO StudentExercise (StudentId, ExerciseId) Values (@studentId, @exerciseId)";
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@exerciseId", exerciseId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

       
    }
}
