using System;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Models;
using Test.Models.Communication;

namespace Test.Business
{
    public static class UsersService
    {
        public static Response GetAllUsers()
        {
            Response response;
            try
            {
                var data = UsersDataAccess.GetAllUsers();
                response = new Response()
                {
                    IsSuccess = true,
                    Status = StatusCode.OK,
                    Content = data,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = StatusCode.ERROR,
                    Content = ex
                };
            }

            return response;
        }
        public static Response GetUser(int Id)
        {
            Response response;
            try
            {
                var data = UsersDataAccess.GetUser(Id);
                response = new Response()
                {
                    IsSuccess = true,
                    Status = StatusCode.OK,
                    Content = data,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = StatusCode.ERROR,
                    Content = ex
                };
            }

            return response;
        }
        public static Response InsertUser(User user)
        {
            Response response;
            try
            {
                var validationUser = UsersDataAccess.ValidateUserExists(user);
                if (validationUser == null)
                {
                    var data = UsersDataAccess.InsertUser(user);
                    response = new Response()
                    {
                        IsSuccess = true,
                        Status = StatusCode.OK,
                        Content = data,
                        Message = string.Empty
                    };
                }
                else
                {
                    response = new Response()
                    {
                        IsSuccess = false,
                        Message = "User is already created",
                        Status = StatusCode.ERROR,
                        Content = string.Empty
                    };

                    if (validationUser.Email.Equals(user.Email))
                    {
                        response.Message = "Email is already registered";
                    }
                    
                    if (validationUser.Username.Equals(user.Username))
                    {
                        response.Message = "Username is already registered";
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = StatusCode.ERROR,
                    Content = ex
                };
            }

            return response;
        }
        public static Response UpdateUser(User user)
        {
            Response response;
            try
            {
                var validationUser = UsersDataAccess.ValidateUserExists(user);
                if (validationUser == null)
                {
                    var data = UsersDataAccess.UpdateUser(user);
                    response = new Response()
                    {
                        IsSuccess = true,
                        Status = StatusCode.OK,
                        Content = data,
                        Message = string.Empty
                    };
                }
                else
                {
                    if (validationUser.Id == user.Id)
                    {
                        var data = UsersDataAccess.UpdateUser(user);
                        response = new Response()
                        {
                            IsSuccess = true,
                            Status = StatusCode.OK,
                            Content = data,
                            Message = string.Empty
                        };
                    }
                    else
                    {
                        response = new Response()
                        {
                            IsSuccess = false,
                            Message = "User is already created",
                            Status = StatusCode.ERROR,
                            Content = string.Empty
                        };

                        if (validationUser.Email.Equals(user.Email))
                        {
                            response.Message = "Email is already registered";
                        }

                        if (validationUser.Username.Equals(user.Username))
                        {
                            response.Message = "Username is already registered";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = StatusCode.ERROR,
                    Content = ex
                };
            }

            return response;
        }
        public static Response DeleteUser(int Id)
        {
            Response response;
            try
            {
                var data = UsersDataAccess.DeleteUser(Id);
                response = new Response()
                {
                    IsSuccess = true,
                    Status = StatusCode.OK,
                    Content = data,
                    Message = string.Empty
                };
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = StatusCode.ERROR,
                    Content = ex
                };
            }

            return response;
        }
    }
}
