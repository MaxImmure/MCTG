using Newtonsoft.Json;
using MCTG.DAL;
using MCTG.Models;
using MCTG.Models.HTTP;
using MCTG.Models.Exceptions;
using HttpMethod = MCTG.Models.HTTP.HttpMethod;

namespace MCTG.BL.HTTP.Endpoints
{
    public class UsersEndpoint : IHttpEndpoint
    {
        private UserRepository userRepository = new UserRepository();

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    CreateUser(rq, rs);
                    break;
                case HttpMethod.GET:
                    GetUser(rq, rs);
                    break;
                case HttpMethod.PUT:
                    UpdatesUser(rq, rs);
                    break;
            }
        }

        private void UpdatesUser(HttpRequest rq, HttpResponse rs)
        {
            throw new NotImplementedException(); //ToDo

            //ToDo Admin or matching User

            try
            {
                rs.ResponseCode = 200;
                rs.ResponseText = "OK";
                rs.Content = "User sucessfully updated.";
            }
            catch (InvalidAccessTokenException)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
            }
            catch (Exception ex)//ToDo => catch (UserNotFoundException)
            {
                rs.ResponseCode = 404;
                rs.Content = "User not found.";
            }
        }

        private void CreateUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<User>(rq.Content);

                userRepository.Add(user);

                rs.ResponseCode = 201;
                rs.ResponseText = "User successfully created";
            }
            catch (EntityAlreadyExistsException)
            {
                rs.ResponseCode = 409;
                rs.ResponseText = "User with same username already registered";
            }
            catch (Exception ex)
            {
                 rs.ResponseCode = 400;
                 rs.ResponseText = "Failed to create User!";
                 Console.WriteLine(ex.Message);
            }
        }

        private void GetUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                var user = JsonConvert.DeserializeObject<User>(rq.Content);

                if (!token.Equals(user.Username.ToLower() + "-mtcgToken")) throw new InvalidAccessTokenException();
                if (userRepository.GetById(user.Guid) == null) throw new UserNotFoundException();

                if (userRepository.IsAdmin(user.Guid)) //ToDo Token matching Username
                {
                    rs.Content = JsonConvert.SerializeObject(user);
                    rs.ContentType = "application/json";
                    rs.ResponseCode = 200;
                    rs.ResponseText = "OK";
                }
            }
            catch (UserNotFoundException)
            {
                rs.ResponseCode = 404;
                rs.ResponseText = "User not found.";
            }
            catch (Exception ex) when(ex is InvalidAccessTokenException or KeyNotFoundException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
        }
    }
}
