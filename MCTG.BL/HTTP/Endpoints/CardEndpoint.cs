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
    public class CardEndpoint: IHttpEndpoint
    {
        private CardRepository cardRepository = new();
        private UserRepository userRepository = new();
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.GET:
                    ListAllCard(rq,rs);
                    break;
            }
        }

        private void ListAllCard(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                var userId = userRepository.GetIdFromUsername(token.Split('-')[0]);
                var user = userRepository.GetById(userId);
                
                if (user == null) throw new UserNotFoundException();

                if (!token.Equals(user.Username.ToLower() + "-mtcgToken")) throw new InvalidAccessTokenException();
                
                var cards = cardRepository.GetAllFromUserId(user.Guid);

                rs.ContentType = "application/json";
                rs.Content = JsonConvert.SerializeObject(cards);
                rs.ResponseCode = 200;
                rs.ResponseText = "The user has cards, the response contains these";
            }
            catch (UserHasNoCardsException)
            {
                rs.ResponseCode = 204;
                rs.ResponseText = "The request was fine, but the user doesn't have any cards";
            }
            catch (Exception ex) when (ex is InvalidAccessTokenException or UserNotFoundException or KeyNotFoundException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
        }
    }
}
