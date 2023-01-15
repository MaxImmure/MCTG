using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.DAL;
using MCTG.Models;
using MCTG.Models.Exceptions;
using MCTG.Models.HTTP;
using Newtonsoft.Json;
using HttpMethod = MCTG.Models.HTTP.HttpMethod;

namespace MCTG.BL.HTTP.Endpoints
{
    public class SessionsEndpoint : IHttpEndpoint
    {
        private UserRepository userRepository = new UserRepository();

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    PerformLoginAction(rq, rs);
                    break;
            }
        }

        private void PerformLoginAction(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var user = JsonConvert.DeserializeObject<LoginCredentials>(rq.Content);
                if(userRepository.LoginUser(user) == null) throw new InvalidLoginCredentialsException();

                rs.ResponseCode = 200;
                rs.ResponseText = "User login successful";
                rs.ContentType = "application/json";
                rs.Content = $"{user.Username.ToLower()}-mtcgToken";
            }
            catch (InvalidLoginCredentialsException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Invalid username/password provided";
            }
        }
    }
}
