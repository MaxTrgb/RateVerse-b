using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Service
{
    internal class UserService
    {
        string connectionString = "Server=34.116.253.154;Port=3306;Database=chat_database;Uid=app_user;Pwd=&X9fT#7vYqZ$4LpR;";

        private UserRepository userRepository = new UserRepository();
        private PostRepository postRepository = new PostRepository();

        public int RegisterUser(string username, string password, string image, string description)
        {
            int id = 0;
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    user = userRepository.getUserByName(connection, username);

                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }

                if (user != null)
                {
                    throw new Exception("User with name: " + username + " already exists");
                }

                try
                {
                    id = userRepository.addUser(connection, username, password, image, description);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }
            return id;
        }

        public User getUserByName(string name)
        {
            User user = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    user = userRepository.getUserByName(connection, name);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }

            if (user == null)
            {
                throw new Exception("User with name: " + name + " not found");
            }


            return user;
        }

        public int loginUser(string name, string password)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                System.Console.WriteLine("User service, starting login");
                try
                {
                    user = userRepository.getUserByName(connection, name);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine("Error: " + ex.Message);
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }

            if (user == null)
            {
                System.Console.WriteLine("User with name: " + name + " not found");
                throw new Exception("User with name: " + name + " not found");
            }

            if (!user.Password.Equals(password))
            {
                System.Console.WriteLine("Incorrect password");
                throw new Exception("Incorrect password");
            }

            return user.Id;
        }

        public User GetUserById(int id)
        {
            User user = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    user = userRepository.getUserById(connection, id);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }

            if (user == null)
            {
                throw new Exception("User with id: " + id + " not found");
            }

            return user;
        }

        public List<User> GetUsersByIds(List<int> ids)
        {
            List<User> users = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    users = userRepository.GetUsersByIds(connection, ids);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }

            if (users == null)
            {
                throw new Exception("Users not found. Ids: " + ids);
            }

            return users;
        }


        public int updateUser(int id, string name, string password, string image, double rating, string description)
        {
            User user = GetUserById(id);


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    return userRepository.updateUser(connection, id, name, password, image, rating, description);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }
        }

        public int UpdateUserRating(int id, double rating)
        {
            int result = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    result = userRepository.updateUserRating(connection, id, rating);
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }

            return result;
        }
        public void ReCalculateUserRating(int id)
        {
            double rating = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    List<Post> posts = postRepository.getPostsByUserID(connection, id);
                    foreach (Post comment in posts)
                    {
                        rating += comment.Rating;
                    }
                    rating /= posts.Count;


                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.\nError:" + ex.Message);
                }
            }

            UpdateUserRating(id, rating); ;
        }
    }
}
