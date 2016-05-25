using _EPAM_Intefases.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using _EPAM_Entites;

namespace _EPAM_DALDATABASE
{
    public class Image : IimageDAL
    {
        private string connectionString;
        public Image()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;

        }

        public int Create(ImageDTO note)
        {
            int current_id;
            using (var connection = new SqlConnection(connectionString))
            {
                var add_image = connection.CreateCommand();
                add_image.CommandText = "Img_Add";
                add_image.CommandType = System.Data.CommandType.StoredProcedure;
                add_image.Parameters.AddWithValue("@Content", note.Content);
                add_image.Parameters.AddWithValue("@ContentType", note.ContentType);
                connection.Open();      
                current_id = (int)(decimal)add_image.ExecuteScalar();
                return current_id;
            }
        }

        public ImageDTO Get(int id)
        {
                byte[] content;
                string contentType;
                using (var connection = new SqlConnection(connectionString))
                {
                    var add_image = connection.CreateCommand();
                    add_image.CommandText = @"SELECT [Content], ContentType FROM dbo.Img WHERE Id = @Id";
                    add_image.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (var reader = add_image.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            content = (byte[])reader["Content"];
                            contentType = (string)reader["ContentType"];
                            return new ImageDTO(id, contentType, content);
                        }

                    }

                    return null;
                }
            }
        }
    }

