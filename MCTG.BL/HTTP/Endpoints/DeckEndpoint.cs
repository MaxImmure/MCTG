using System.Data;
using System.Net.Http.Headers;
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
    public class DeckEndpoint: IHttpEndpoint
    {
        private CardRepository cardRepository = new();
        private UserRepository userRepository = new();
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.GET:
                    ShowDeck(rq,rs);
                    break;
                case HttpMethod.PUT:
                    ConfigureDeck(rq,rs);
                    break;
            }
        }

        private void ConfigureDeck(HttpRequest rq, HttpResponse rs)
        {
            try
            {

                //ToDo

                rs.ResponseCode = 200;
                rs.ResponseText = "The deck has been successfully configured";
            }
            catch (InvalidAccessTokenException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
            catch (IndexOutOfRangeException)
            {
                rs.ResponseCode = 400;
                rs.ResponseText = "The provided deck did not include the required amount of cards";
            }
            catch (CardBelongsToAnotherUserException)
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "At least one of the provided cards does not belong to the user or is not available.";
            }
        }

        private void ShowDeck(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                var userId = userRepository.GetIdFromUsername(token.Split('-')[0]);
                var user = userRepository.GetById(userId);

                if (user == null) 
                    throw new InvalidAccessTokenException();

                var deck = userRepository.GetDeckById(user.Guid);
                if (rq.QueryParams["format"].Equals("plain"))
                {
                    rs.ContentType = "text/plain";
                    rs.Content = deck.ToString();
                } 
                else
                {
                    rs.ContentType = "application/json";
                    rs.Content = JsonConvert.SerializeObject(deck);
                }

                rs.ResponseText = "The deck has cards, the response contains these";
                rs.ResponseCode = 200;
            }
            catch (EmptyDeckException)
            {
                rs.ResponseCode = 204;
                rs.ResponseText = "The request was fine, but the deck doesn't have any cards";
            }
            catch (InvalidAccessTokenException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
        }
    }
}
