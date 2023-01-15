using MCTG.DAL;
using MCTG.Models;
using MCTG.Models.Exceptions;
using MCTG.Models.HTTP;
using Newtonsoft.Json;
using HttpMethod = MCTG.Models.HTTP.HttpMethod;

namespace MCTG.BL.HTTP.Endpoints
{
    public class ProfileEndpoint : IHttpEndpoint
    {
        private User user;
        private UserRepository userRepository = new();

        public ProfileEndpoint(User user)
        {
            this.user = user;
        }

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.GET:
                    GetUser(rq, rs);
                    break;
                case HttpMethod.PUT:
                    UpdatesUser(rq, rs);
                    break;
            }
        }

        private void GetUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];

                if (!token.Equals(user.Credentials.Username.ToLower() + "-mtcgToken") && !userRepository.IsAdmin(userRepository.GetIdFromUsername(token.Split('-')[0]))) throw new InvalidAccessTokenException();
                if (userRepository.GetById(user.Guid) == null) throw new UserNotFoundException();

                    rs.Content = JsonConvert.SerializeObject(user);
                    rs.ContentType = "application/json";
                    rs.ResponseCode = 200;
                    rs.ResponseText = "OK";
            }
            catch (UserNotFoundException)
            {
                rs.ResponseCode = 404;
                rs.ResponseText = "User not found.";
            }
            catch (Exception ex) when (ex is InvalidAccessTokenException or KeyNotFoundException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
        }

        private record Parsing()
        {
            public String Name;
            public String Bio;
        }

        private void UpdatesUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                if (!user.Credentials.Username.Equals(token.Split('-')[0])) throw new InvalidAccessTokenException();
                
                if (!token.Equals(user.Credentials.Username.ToLower() + "-mtcgToken")) throw new InvalidAccessTokenException();

                var u = JsonConvert.DeserializeObject<Parsing>(rq.Content);

                user.Description = u.Bio;
                user.Name = u.Name;
                userRepository.Update(user);

                rs.ResponseCode = 200;
                rs.ResponseText = "OK";
                rs.Content = "User sucessfully updated.";
            }
            catch (InvalidAccessTokenException)
            {
                rs.ResponseCode = 401;
                rs.Content = "Access token is missing or invalid";
            }
            catch (UserNotFoundException ex)
            {
                rs.ResponseCode = 404;
                rs.Content = "User not found.";
            }
        }
    }
}
