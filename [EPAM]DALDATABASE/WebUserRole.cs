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
    public class WebUserRole:IWebUserRoleDAL
    {
        private string connectionString;
        public WebUserRole()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        }

        public bool Create(WebUserRoleDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var add_web_user_role = connection.CreateCommand();
                add_web_user_role.CommandText = $"INSERT INTO dbo.WebUserRoles (WebUserId, RoleId) VALUES (@WebUserId, @RoleId)";
                add_web_user_role.Parameters.AddWithValue("@WebUserId", note.WebUserId);
                add_web_user_role.Parameters.AddWithValue("@RoleId", note.RoleId);
                connection.Open();
                var result = add_web_user_role.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Delete(WebUserRoleDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var delete_web_user = connection.CreateCommand();
                delete_web_user.CommandText = $"DELETE FROM dbo.WebUserRoles WHERE WebUserId = @WebUserId AND RoleId = @RoleId";
                delete_web_user.Parameters.AddWithValue("@WebUserId", note.WebUserId);
                delete_web_user.Parameters.AddWithValue("@RoleId", note.RoleId);
                connection.Open();
                var result = delete_web_user.ExecuteNonQuery();
                return result > 0;
            }
        }

        public WebUserRoleDTO Get(int id)
        {
            int web_user_id;
            int role;
            using (var connection = new SqlConnection(connectionString))
            {
                var get_award = connection.CreateCommand();
                get_award.CommandText = @"SELECT WebUserId, RoleId FROM dbo.WebUserRoles WHERE Id = @Id";
                get_award.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = get_award.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        web_user_id = (int)reader["WebUserId"];
                        role = (int)reader["RoleId"];
                        return new WebUserRoleDTO(id, web_user_id, role);
                    }

                }

                return null;
            }
        }

        public IEnumerable<WebUserRoleDTO> GetAll()
        {
            int id;
            int web_user_id;
            int role;
            List<WebUserRoleDTO> web_users_roles = new List<WebUserRoleDTO>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT WebUserId, RoleId FROM dbo.WebUserRoles";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = (int)reader["Id"];
                        web_user_id = (int)reader["WebUserId"];
                        role = (int)reader["RoleId"];
                        web_users_roles.Add(new WebUserRoleDTO(id, web_user_id, role));
                    }
                }
            }
            return web_users_roles;
        }

        public bool Update(WebUserRoleDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var update_web_user = connection.CreateCommand();
                update_web_user.CommandText = $"UPDATE dbo.WebUsers SET Id = @Id , WebUserId = @WebUserId, RoleId = @RoleId WHERE Id = @Id";
                update_web_user.Parameters.AddWithValue("@Id", note.Id);
                update_web_user.Parameters.AddWithValue("@WebUserId", note.WebUserId);
                update_web_user.Parameters.AddWithValue("@RoleId", note.RoleId);
                connection.Open();
                var result = update_web_user.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool IsWebUserInRole(string name, string role)
        {
            int web_user_id = -1;
            int role_id = -1;
            using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user_role = connection.CreateCommand();
                get_web_user_role.CommandText = @"SELECT Id FROM dbo.WebUsers WHERE Name = @Name";
                get_web_user_role.Parameters.AddWithValue("@Name", name);
                connection.Open();
                using (var reader = get_web_user_role.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        web_user_id = (int)reader["Id"];
                    }
                }
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user_role = connection.CreateCommand();
                get_web_user_role.CommandText = @"SELECT Id FROM dbo.Roles WHERE Name = @Name";
                get_web_user_role.Parameters.AddWithValue("@Name", role);
                connection.Open();
                using (var reader = get_web_user_role.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        role_id = (int)reader["Id"];
                    }
                }
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user_roles = connection.CreateCommand();
                get_web_user_roles.CommandText = @"SELECT RoleId FROM dbo.WebUserRoles WHERE WebUserId = @WebUserId AND RoleId = @RoleId";
                get_web_user_roles.Parameters.AddWithValue("@WebUserId", web_user_id);
                get_web_user_roles.Parameters.AddWithValue("@RoleId", role_id);
                connection.Open();
                using (var reader = get_web_user_roles.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }

        }

        public string[] GetRolesForUser(string web_user_name)
        {
            int web_user_id = -1;
            List<int> roles_id = new List<int>();
            string[] roles_name = null;
            using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user_role = connection.CreateCommand();
                get_web_user_role.CommandText = @"SELECT Id FROM dbo.WebUsers WHERE Name = @Name";
                get_web_user_role.Parameters.AddWithValue("@Name", web_user_name);
                connection.Open();
                using (var reader = get_web_user_role.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        web_user_id = (int)reader["Id"];
                    }
                }
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var get_web_user_roles = connection.CreateCommand();
                get_web_user_roles.CommandText = @"SELECT RoleId FROM dbo.WebUserRoles WHERE WebUserId = @WebUserId";
                get_web_user_roles.Parameters.AddWithValue("@WebUserId", web_user_id);
                connection.Open();
                using (var reader = get_web_user_roles.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles_id.Add((int)reader["RoleId"]);
                    }
                }
            }

            int i = 0;
            roles_name = new string[roles_id.Count];
            foreach (var role_id in roles_id)
            {
                using (var connection = new SqlConnection(connectionString))
                {                       
                    var get_web_user_role = connection.CreateCommand();
                    get_web_user_role.CommandText = @"SELECT Name FROM dbo.Roles WHERE Id = @Id";
                    get_web_user_role.Parameters.AddWithValue("@Id", role_id);
                    connection.Open();
                    using (var reader = get_web_user_role.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            roles_name[i] = (string)reader["Name"];
                            i++;
                        }
                    }
                }
            }

            return roles_name;
        }
    }
}

