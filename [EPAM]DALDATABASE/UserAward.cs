using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _EPAM_Entites;
using _EPAM_Intefases.DAL;
using System.Configuration;
using System.Data.SqlClient;

namespace _EPAM_DALDATABASE
{
    public class UserAward : IUserAwardDAL
    {
        private string connectionString;
        public UserAward()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        }

        public bool Create(UserAwardDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var add_award = connection.CreateCommand();
                add_award.CommandText = $"INSERT INTO dbo.UserAward (UserId, AwardId) VALUES (@UserId, @AwardId)";
                add_award.Parameters.AddWithValue("@UserId", note.UserId);
                add_award.Parameters.AddWithValue("@AwardId", note.AwardId);
                connection.Open();
                var result = add_award.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Delete(UserAwardDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var delete_user_award = connection.CreateCommand();
                delete_user_award.CommandText = $"DELETE FROM dbo.UserAward WHERE UserId = @UserId AND AwardId = @AwardId";
                delete_user_award.Parameters.AddWithValue("@UserId", note.UserId);
                delete_user_award.Parameters.AddWithValue("@AwardId", note.AwardId);
                connection.Open();
                var result = delete_user_award.ExecuteNonQuery();
                return result > 0;
            }
        }

        public IEnumerable<UserAwardDTO> GetAll()
        {
            try
            {
                Guid user_id;
                Guid award_id; ;
                List<UserAwardDTO> user_awards = new List<UserAwardDTO>();
                using (var connection = new SqlConnection(connectionString))
                {
                    var get_all_user_award = connection.CreateCommand();
                    get_all_user_award.CommandText = @"SELECT UserId, AwardId FROM dbo.UserAward";
                    connection.Open();
                    using (var reader = get_all_user_award.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user_id = (Guid)reader["UserId"];
                            award_id = (Guid)reader["AwardId"];
                            user_awards.Add(new UserAwardDTO(user_id, award_id));
                        }
                    }
                }
                return user_awards;
            }

            catch (Exception e)
            {
               
                throw e;
            }
        }

    }
}
