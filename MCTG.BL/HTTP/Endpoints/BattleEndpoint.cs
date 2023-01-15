using System.Runtime.CompilerServices;
using MCTG.DAL;
using MCTG.Models;
using MCTG.Models.Exceptions;
using MCTG.Models.HTTP;
using HttpMethod = MCTG.Models.HTTP.HttpMethod;

namespace MCTG.BL.HTTP.Endpoints
{
    public class BattleEndpoint : IHttpEndpoint
    {
        private Battle b = Battle.Instance;
        private UserRepository userRepository = new();

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    JoinLobbyAction(rq, rs);
                    break;
            }
        }

        private void JoinLobbyAction(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                var userId = userRepository.GetIdFromUsername(token.Split('-')[0]);
                var user = userRepository.GetById(userId);

                if (!token.Equals(user.Credentials.Username.ToLower() + "-mtcgToken")) throw new InvalidAccessTokenException();
                if (userRepository.GetById(user.Guid) == null) throw new UserNotFoundException();
                
                var result = b.QueuePlayer(user);

                rs.ContentType = "text/plain";
                rs.Content = result;
                rs.ResponseText = "The battle has been carried out successfully.";
                rs.ResponseCode = 200;
            }
            catch (Exception ex) when (ex is UserNotFoundException or InvalidAccessTokenException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
        }
    }
}
