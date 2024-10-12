using DENMAP_SERVER.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Repository
{
    internal class GenreRepository
    {
        public int addGenre(MySqlConnection connection, string name)
        {
            string query = $"INSERT INTO genres (name) VALUES ( @name); SELECT LAST_INSERT_ID();";
            int id = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return id;
        }

        public bool updateGenre(MySqlConnection connection, int id, string name)
        {
            string query = $"UPDATE genres SET name = @name WHERE id = @id";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@id", id);
                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }

        public List<Genre> getAllGenres(MySqlConnection connection)
        {
            string query = "SELECT * FROM genres";
            List<Genre> genres = new List<Genre>();

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToString(reader["name"])
                            );
                        genres.Add(genre);
                    }
                }
            }

            return genres;
        }

        public Genre getGenreById(MySqlConnection connection, int id)
        {
            string query = "SELECT * FROM genres WHERE id = @id";
            Genre genre = null;

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        genre = new Genre(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToString(reader["name"])
                            );
                    }
                }
            }

            return genre;
        }

        public List<Genre> GetGenresByIds(MySqlConnection connection, List<int> ids)
        {
            List<Genre> genres = new List<Genre>();

            if (ids.Count == 0) 
                return genres;

            string idsString = string.Join(",", ids);

            string query = $"SELECT * FROM genres WHERE id IN ({idsString})";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Genre genre = new Genre(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToString(reader["name"])
                            );
                        genres.Add(genre);
                    }
                }
            }

            return genres;
        }
    }
}
