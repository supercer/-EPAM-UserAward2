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
    public class Award : IAwardDAL
    {
       private string connectionString;
       public Award()
        {
           this.connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
           
        }

        public bool Create(AwardDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var add_award = connection.CreateCommand();
                if (note.ImageId == null)
                {
                    add_award.CommandText = $"INSERT INTO dbo.Award (AwardId, Title) VALUES (@AwardId, @Title)";
                    add_award.Parameters.AddWithValue("@AwardId", note.Id);
                    add_award.Parameters.AddWithValue("@Title", note.Title);
                }

                else
                {
                    add_award.CommandText = $"INSERT INTO dbo.Award (AwardId, Title, ImageId) VALUES (@AwardId, @Title, @ImageId)";
                    add_award.Parameters.AddWithValue("@AwardId", note.Id);
                    add_award.Parameters.AddWithValue("@Title", note.Title);
                    add_award.Parameters.AddWithValue("@ImageId", note.ImageId);
                }
                connection.Open();
                var result = add_award.ExecuteNonQuery();
                return result > 0;
            }
        }
        public bool Delete(Guid id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var delete_award = connection.CreateCommand();
                delete_award.CommandText = $"DELETE FROM dbo.Award WHERE AwardId = @Id";
                delete_award.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var result = delete_award.ExecuteNonQuery();
                if (result == 0)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }
        }

        public AwardDTO Get(Guid id)
        {
            string title;
            int? image_id;
            using (var connection = new SqlConnection(connectionString))
            {
                var get_award = connection.CreateCommand();
                get_award.CommandText = @"SELECT Title, ImageId FROM dbo.Award WHERE AwardId = @Id";
                get_award.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = get_award.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        title = (string)reader["Title"];
                        image_id = reader["ImageId"] as int?;
                        if (image_id == null)
                        {
                            return new AwardDTO(id, title);
                        }

                        else
                        {
                            return new AwardDTO(id, title, image_id);
                        }
                    }        
                    
                }

                return null;
            }
        }

        public IEnumerable<AwardDTO> GetAll()
        {
            Guid id;
            string title;
            int? image_id;
            List<AwardDTO> awards = new List<AwardDTO>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT AwardId, Title, ImageId FROM dbo.Award";
                connection.Open();
                using (var reader = command.ExecuteReader())
                { 
                    while (reader.Read())
                    {
                        id = (Guid)reader["AwardId"];
                        title = (string)reader["Title"];
                        image_id = reader["ImageId"] as int?;
                        if (image_id == null)
                        {
                            awards.Add(new AwardDTO(id, title));
                        }

                        else
                        {
                            awards.Add(new AwardDTO(id, title, image_id));
                        }
                    }
            }
            }
            return awards;
        }

        public bool Update(AwardDTO note)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var add_award = connection.CreateCommand();
                if (note.ImageId == null)
                {
                    add_award.CommandText = $"UPDATE dbo.Award SET AwardId = @AwardId , Title = @Title WHERE AwardId = @AwardId";
                    add_award.Parameters.AddWithValue("@AwardId", note.Id);
                    add_award.Parameters.AddWithValue("@Title", note.Title);
                }

                else
                {
                    add_award.CommandText = $"UPDATE dbo.Award SET AwardId = @AwardId , Title = @Title, ImageId = @ImageId WHERE AwardId = @AwardId";
                    add_award.Parameters.AddWithValue("@AwardId", note.Id);
                    add_award.Parameters.AddWithValue("@Title", note.Title);
                    add_award.Parameters.AddWithValue("@ImageId", note.ImageId);
                }

                connection.Open();
                var result = add_award.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
