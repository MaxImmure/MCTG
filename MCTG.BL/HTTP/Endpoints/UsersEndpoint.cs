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
        private HttpServer server;

        public UsersEndpoint(HttpServer server)
        {
            this.server = server;
        }

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    CreateUser(rq, rs);
                    break;
            }
        }

        private void CreateUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<LoginCredentials>(rq.Content);

                var U = userRepository.Add(user);
                
                server.RegisterEndpoint("/users/" + user.Username, new ProfileEndpoint(U));

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
    }
}
