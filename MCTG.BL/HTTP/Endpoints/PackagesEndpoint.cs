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
    public class PackagesEndpoint : IHttpEndpoint
    {
        private CardRepository cardRepository = new();
        private UserRepository userRepository = new();
        private PackageRepository packageRepository = new();
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    CreateNewCardPackage(rq,rs);
                    break;
            }
        }

        private class ParsingCard
        {
            public String id;
            public String name;
            public double damage;
        }

        private void CreateNewCardPackage(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                var id = userRepository.GetIdFromUsername(token.Split("-")[0]);
                if (!userRepository.IsAdmin(id))
                    throw new UserIsNotAdminException();

                var cards = JsonConvert.DeserializeObject<ParsingCard[]>(rq.Content);

                foreach (var card in cards)
                {
                    if (cardRepository.GetById(new Guid(card.id)) != null) throw new EntityAlreadyExistsException();
                }

                List<ICard> tmpCards = new();
                foreach (var card in cards)
                {
                    var c = CardFactory.GetCard(card.name, card.damage, new Guid(card.id), Guid.Empty);
                    cardRepository.Add(c);
                    tmpCards.Add(c);
                }

                Package package = new()
                {
                    Cards = tmpCards.ToArray(),
                    p_id = Guid.NewGuid()
                };
                packageRepository.Add(package);
                rs.ResponseCode = 201;
                rs.ResponseText = "Package and cards successfully created";
            }
            catch (UserIsNotAdminException)
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "Provided user is not \"admin\"";
            }
            catch (Exception ex) when (ex is InvalidAccessTokenException or KeyNotFoundException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
            catch (EntityAlreadyExistsException)
            {
                rs.ResponseCode = 409;
                rs.ResponseText = "At least one card in the packages already exists";
            }
        }
    }
}
