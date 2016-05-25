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
    public class WebUser : IWebUserDAL
    {
        private string connectionString;
        public WebUser()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        }

        public bool Create(WebUserDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var add_web_user = connection.CreateCommand();
                add_web_user.CommandText = $"INSERT INTO dbo.WebUsers (Name, PasswordHashCode) VALUES (@Name, @PasswordHashCode)";
                add_web_user.Parameters.AddWithValue("@Name", note.Name);
                add_web_user.Parameters.AddWithValue("@PasswordHashCode", note.PasswordHashCode);
                connection.Open();
                var result = add_web_user.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var delete_web_user = connection.CreateCommand();
                delete_web_user.CommandText = $"DELETE FROM dbo.WebUsers WHERE Id = @Id";
                delete_web_user.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = delete_web_user.ExecuteNonQuery();
                return result > 0;
            }
        }

        public WebUserDTO Get(int id)
        {
            string name;
            int password_hash_code;
            using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user = connection.CreateCommand();
                get_web_user.CommandText = @"SELECT Name, PasswordHashCode FROM dbo.WebUsers WHERE Id = @Id";
                get_web_user.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = get_web_user.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        name = (string)reader["Name"];
                        password_hash_code = (int)reader["PasswordHashCode"];
                        return new WebUserDTO(id, name, password_hash_code);
                    }

                }

                return null;
            }
        }

        public IEnumerable<WebUserDTO> GetAll()
        {
            int id;
            string name;
            int password_hash_code;
            List<WebUserDTO> web_users = new List<WebUserDTO>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Name, PasswordHashCode FROM dbo.WebUsers";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = (int)reader["Id"];
                        name = (string)reader["Name"];
                        password_hash_code = (int)reader["PasswordHashCode"];
                        web_users.Add(new WebUserDTO(id, name, password_hash_code));
                    }
                }
            }
            return web_users;
        }

        public bool Update(WebUserDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var update_web_user = connection.CreateCommand();
                update_web_user.CommandText = $"UPDATE dbo.WebUsers SET Id = @Id , Name = @Name, PasswordHashCode = @PasswordHashCode WHERE Id = @Id";
                update_web_user.Parameters.AddWithValue("@Id", note.Id);
                update_web_user.Parameters.AddWithValue("@Name", note.Name);
                update_web_user.Parameters.AddWithValue("@Password", note.PasswordHashCode);
                connection.Open();
                var result = update_web_user.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool IsWebUser(string name, string password)
        {
            int password_hash_code = password.GetHashCode();
            using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user = connection.CreateCommand();
                get_web_user.CommandText = @"SELECT Name, PasswordHashCode FROM dbo.WebUsers WHERE Name = @Name AND PasswordHashCode = @PasswordHashCode";
                get_web_user.Parameters.AddWithValue("@Name", name);
                get_web_user.Parameters.AddWithValue("@PasswordHashCode", password_hash_code);
                connection.Open();
                using (var reader = get_web_user.ExecuteReader())
                {
                        return reader.HasRows;
                }
            }
        }
        public int GetIdAnWebUserName(string web_user_name)
        {
            int web_user_id = -1;
             using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user_id = connection.CreateCommand();
                get_web_user_id.CommandText = @"SELECT Id FROM dbo.WebUsers WHERE Name = @Name";
                get_web_user_id.Parameters.AddWithValue("@Name", web_user_name);
                connection.Open();
                using (var reader = get_web_user_id.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        web_user_id = (int)reader["Id"];
                    }
                }
            }
            return web_user_id;
        }
    }
}
 