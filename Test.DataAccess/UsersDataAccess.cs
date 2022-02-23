using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Test.Models;

namespace Test.DataAccess
{
    public class UsersDataAccess
    {
        public static object GetAllUsers()
        {
            string dbConnection = Environment.GetEnvironmentVariable("DBCONN") ?? throw new Exception("Database connection string not found!");
            object response = null;

            using (var dbContext = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new();
                List<User> listUsers = new List<User>();

                try
                {
                    cmd.Connection = dbContext;
                    cmd.Connection.Open();
                    cmd.Transaction = dbContext.BeginTransaction();
                    cmd.CommandText = "dbo.GetAllUsers";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var user = new User()
                            {
                                Id = int.Parse(rdr[nameof(User.Id)].ToString()),
                                Email = rdr[nameof(User.Email)].ToString(),
                                Username = rdr[nameof(User.Username)].ToString(),
                                Password = rdr[nameof(User.Password)].ToString(),
                                Genre = rdr[nameof(User.Genre)].ToString(),
                                CreationDate = (DateTime)rdr[nameof(User.CreationDate)],
                                Status = (bool)rdr[nameof(User.Status)]
                            };

                            listUsers.Add(user);
                        }
                    }
                    response = listUsers;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        public static object GetUser(int Id)
        {
            string dbConnection = Environment.GetEnvironmentVariable("DBCONN") ?? throw new Exception("Database connection string not found!");
            object response = null;

            using (var dbContext = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new();

                try
                {
                    cmd.Connection = dbContext;
                    cmd.Connection.Open();
                    cmd.Transaction = dbContext.BeginTransaction();
                    cmd.CommandText = "dbo.GetUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", Id));

                    User user = null;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            user = new User()
                            {
                                Id = int.Parse(rdr[nameof(User.Id)].ToString()),
                                Email = rdr[nameof(User.Email)].ToString(),
                                Username = rdr[nameof(User.Username)].ToString(),
                                Password = rdr[nameof(User.Password)].ToString(),
                                Genre = rdr[nameof(User.Genre)].ToString(),
                                CreationDate = (DateTime)rdr[nameof(User.CreationDate)],
                                Status = (bool)rdr[nameof(User.Status)]
                            };
                        }
                    }

                    response = user;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        public static User GetUserByUsername(string Username)
        {
            string dbConnection = Environment.GetEnvironmentVariable("DBCONN") ?? throw new Exception("Database connection string not found!");
            User response = null;

            using (var dbContext = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new();

                try
                {
                    cmd.Connection = dbContext;
                    cmd.Connection.Open();
                    cmd.Transaction = dbContext.BeginTransaction();
                    cmd.CommandText = "dbo.GetUserByUsername";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Username", Username));

                    User user = null;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            user = new User()
                            {
                                Id = int.Parse(rdr[nameof(User.Id)].ToString()),
                                Email = rdr[nameof(User.Email)].ToString(),
                                Username = rdr[nameof(User.Username)].ToString(),
                                Password = rdr[nameof(User.Password)].ToString(),
                                Genre = rdr[nameof(User.Genre)].ToString(),
                                CreationDate = (DateTime)rdr[nameof(User.CreationDate)],
                                Status = (bool)rdr[nameof(User.Status)]
                            };
                        }
                    }

                    response = user;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        public static User ValidateUserExists(User user)
        {
            string dbConnection = Environment.GetEnvironmentVariable("DBCONN") ?? throw new Exception("Database connection string not found!");
            User response = null;

            using (var dbContext = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new();

                try
                {
                    cmd.Connection = dbContext;
                    cmd.Connection.Open();
                    cmd.Transaction = dbContext.BeginTransaction();
                    cmd.CommandText = "dbo.GetExistingUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
                    cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    cmd.Parameters.Add(new SqlParameter("@Genre", user.Genre));

                    User userFound = null;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            userFound = new User()
                            {
                                Id = int.Parse(rdr[nameof(User.Id)].ToString()),
                                Email = rdr[nameof(User.Email)].ToString(),
                                Username = rdr[nameof(User.Username)].ToString(),
                                Password = rdr[nameof(User.Password)].ToString(),
                                Genre = rdr[nameof(User.Genre)].ToString(),
                                CreationDate = (DateTime)rdr[nameof(User.CreationDate)],
                                Status = (bool)rdr[nameof(User.Status)]
                            };
                        }
                    }

                    response = userFound;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        public static object InsertUser(User user)
        {
            string dbConnection = Environment.GetEnvironmentVariable("DBCONN") ?? throw new Exception("Database connection string not found!");
            object response = false;

            using (var dbContext = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new();

                try
                {
                    cmd.Connection = dbContext;
                    cmd.Connection.Open();
                    cmd.Transaction = dbContext.BeginTransaction();
                    cmd.CommandText = "dbo.InsertUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
                    cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
                    cmd.Parameters.Add(new SqlParameter("@Genre", user.Genre));

                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();

                    response = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        public static object UpdateUser(User user)
        {
            string dbConnection = Environment.GetEnvironmentVariable("DBCONN") ?? throw new Exception("Database connection string not found!");
            object response = false;

            using (var dbContext = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new();

                try
                {
                    cmd.Connection = dbContext;
                    cmd.Connection.Open();
                    cmd.Transaction = dbContext.BeginTransaction();
                    cmd.CommandText = "dbo.UpdateUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", user.Id));
                    cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
                    cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
                    cmd.Parameters.Add(new SqlParameter("@Genre", user.Genre));

                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();

                    response = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
        public static object DeleteUser(int Id)
        {
            string dbConnection = Environment.GetEnvironmentVariable("DBCONN") ?? throw new Exception("Database connection string not found!");
            object response = false;

            using (var dbContext = new SqlConnection(dbConnection))
            {
                SqlCommand cmd = new();

                try
                {
                    cmd.Connection = dbContext;
                    cmd.Connection.Open();
                    cmd.Transaction = dbContext.BeginTransaction();
                    cmd.CommandText = "dbo.DeleteUser";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Id", Id));

                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();

                    response = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return response;
        }
    }
}
