using System.Runtime.CompilerServices;
using System.Xml;
using MCTG.DAL;
using MCTG.DAL.Repository;
using MCTG.Models;
using MCTG.Models.Cards;
using MCTG.Models.Exceptions;
using MCTG.Models.HTTP;
using Newtonsoft.Json;
using HttpMethod = MCTG.Models.HTTP.HttpMethod;

namespace MCTG.BL.HTTP.Endpoints
{
    public class StatsEndpoint: IHttpEndpoint
    {
        private UserRepository userRepository = new();

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.GET:
                    ListScoreboard(rq,rs);
                    break;
            }
        }

        private record OutputScoreboard()
        {
            public string Name;
            public int elo;
            public int win;
            public int lose;
            public int draw;
        }

        private void ListScoreboard(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                var userId = userRepository.GetIdFromUsername(token.Split('-')[0]);
                var user = userRepository.GetById(userId);
                
                if (user == null) throw new UserNotFoundException();

                if (!token.Equals(user.Credentials.Username.ToLower() + "-mtcgToken")) throw new InvalidAccessTokenException();

                var scoreboard = userRepository.GetScoreboard();
                List<OutputScoreboard> result = new();

                foreach (var records in scoreboard)
                {
                    result.Add(new OutputScoreboard()
                    {
                        Name = records.Name
                        , elo = records.GameStats.elo
                        , win = records.GameStats.wins
                        , lose = records.GameStats.looeses
                        , draw = records.GameStats.draws
                    });
                }

                rs.ContentType = "application/json";
                rs.Content = JsonConvert.SerializeObject(result);
                rs.ResponseCode = 200;
                rs.ResponseText = "The scoreboard could be retrieved successfully.";
            }
            catch (Exception ex) when (ex is InvalidAccessTokenException or UserNotFoundException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
        }
    }
}
