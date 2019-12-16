using studentExercises.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace studentExercises.Data
{
    public class InstructorRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }
        //Get All Instructors with cohort name
        public List<Instructor> GetAllIntructors()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT i.Id, i.FirstName, i.LastName, i.SlackHandle, c.Name FROM Instructor i
                                        INNER JOIN Cohort c ON i.CohortId = c.Id";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Instructor> instructors = new List<Instructor>();

                    while (reader.Read())
                    {
                        
                        Instructor instructor = new Instructor
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            LastName = reader.GetString(reader.GetOrdinal("LastName")),
                            SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                            Cohort = new Cohort()
                            {
                                Name = reader.GetString(reader.GetOrdinal("Name"))
                            }

                        };


                        instructors.Add(instructor);
                    }

                    reader.Close();
                    return instructors;
                }


            }

        }
        public void AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Instructor(FirstName, LastName, SlackHandle, CohortId) OUTPUT INSERTED.Id Values(@FirstName, @LastName, @SlackHandle, @CohortId)";
                    cmd.Parameters.Add(new SqlParameter("@FirstName", instructor.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LastName", instructor.LastName));
                    cmd.Parameters.Add(new SqlParameter("@SlackHandle", instructor.SlackHandle));
                    cmd.Parameters.Add(new SqlParameter("@CohortId", instructor.Cohort.Id));

                    int id = (int)cmd.ExecuteScalar();
                    instructor.Id = id;

                }


            }
        }
    }
}

