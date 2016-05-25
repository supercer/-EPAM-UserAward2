using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_Entites;
using System.Configuration;
using System.Data.SqlClient;
using _EPAM_Intefases.DAL;

namespace _EPAM_DALDATABASE
{
    public class User : IUserDAL
    {
        private string connectionString;
        public User()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        }

        public bool Create(UserDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var add_user = connection.CreateCommand();
                if (note.ImageId == null)
                {            
                    add_user.CommandText = $"INSERT INTO dbo.[User] (UserId, Name, DateOfBirth ) VALUES (@UserId, @Name, @DateOfBirth)";
                    add_user.Parameters.AddWithValue("@UserId", note.Id);
                    add_user.Parameters.AddWithValue("@Name", note.Name);
                    add_user.Parameters.AddWithValue("@DateOfBirth", note.DateOfBith);
                }

                else
                {
                    add_user.CommandText = $"INSERT INTO dbo.[User] (UserId, Name, DateOfBirth, ImageId) VALUES (@UserId, @Name, @DateOfBirth, @ImageId)";
                    add_user.Parameters.AddWithValue("@UserId", note.Id);
                    add_user.Parameters.AddWithValue("@Name", note.Name);
                    add_user.Parameters.AddWithValue("@DateOfBirth", note.DateOfBith);
                    add_user.Parameters.AddWithValue("@ImageId", note.ImageId);
                }
       
                connection.Open();
                var result = add_user.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Delete(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var delete_award = connection.CreateCommand();
                delete_award.CommandText = $"DELETE FROM dbo.[User] WHERE UserId = @Id";
                delete_award.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = delete_award.ExecuteNonQuery();
                return result > 0;
            }
        }

        public UserDTO Get(Guid id)
        {
            int? image_id;
            string name;
            DateTime date_of_birth;
            using (var connection = new SqlConnection(connectionString))
            {
                var get_award = connection.CreateCommand();
                get_award.CommandText = $@"SELECT Name, DateOfBirth, ImageId FROM dbo.[User] WHERE UserId = @Id";
                get_award.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = get_award.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = (string)reader["Name"];
                        date_of_birth = (DateTime)reader["DateOfBirth"];
                        image_id = reader["ImageId"] as int?;
                        if (image_id == null)
                        {
                            return new UserDTO(id, name, date_of_birth);
                        }

                        else
                        {
                            return new UserDTO(id, name, date_of_birth, image_id);
                        }
                    }
                }

                return null;
            }
        }

        public IEnumerable<UserDTO> GetAll()
        {
            Guid id;
            string name;
            DateTime date_of_birth;
            int? image_id;
            List<UserDTO> users = new List<UserDTO>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT UserId, Name, DateOfBirth, ImageId FROM dbo.[User]";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = (Guid)reader["UserId"];
                        name = (string)reader["Name"];
                        date_of_birth = (DateTime)reader["DateOfBirth"];
                        image_id = reader["ImageId"] as int?;
                        if (image_id == null)
                        {
                            users.Add(new UserDTO(id, name, date_of_birth));
                        }

                        else
                        {
                            users.Add(new UserDTO(id, name, date_of_birth, image_id));
                        }
                       
                    }
                }
            }
            return users;
        }

        public bool Update(UserDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var add_user = connection.CreateCommand();
                if (note.ImageId == null)
                {
                    add_user.CommandText = $"UPDATE dbo.[User] SET UserId = @UserId , Name = @Name, DateOfBirth = @DateOfBirth WHERE UserId = @UserId";
                    add_user.Parameters.AddWithValue("@UserId", note.Id);
                    add_user.Parameters.AddWithValue("@Name", note.Name);
                    add_user.Parameters.AddWithValue("@DateOfBirth", note.DateOfBith);
                }

                else
                {
                    add_user.CommandText = $"UPDATE dbo.[User] SET UserId = @UserId , Name = @Name, ImageId = @ImageId, DateOfBirth = @DateOfBirth WHERE UserId = @UserId";
                    add_user.Parameters.AddWithValue("@UserId", note.Id);
                    add_user.Parameters.AddWithValue("@Name", note.Name);
                    add_user.Parameters.AddWithValue("@DateOfBirth", note.DateOfBith);
                    add_user.Parameters.AddWithValue("@ImageId", note.ImageId);
                }

                connection.Open();
                var result = add_user.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
