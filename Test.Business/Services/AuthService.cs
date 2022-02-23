using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.Models;
using Test.Models.Communication;

namespace Test.Business.Services
{
    public class AuthService
    {
        public static Response Login(Credential credential)
        {
            Response response;
            try
            {
                var data = UsersDataAccess.GetUserByUsername(credential.Username);

                if (data == null)
                {
                    response = new Response()
                    {
                        IsSuccess = false,
                        Message = "User not found",
                        Status = StatusCode.ERROR,
                        Content = string.Empty
                    };
                }
                else
                {
                    if (data.Status)
                    {
                        if ((data.Password + credential.Salt).Equals(credential.Password))
                        {
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
                                Message = "Wrong password",
                                Status = StatusCode.ERROR,
                                Content = string.Empty
                            };
                        }
                    }
                    else
                    {
                        response = new Response()
                        {
                            IsSuccess = false,
                            Message = "Inactive user",
                            Status = StatusCode.ERROR,
                            Content = string.Empty
                        };
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
    }
}
