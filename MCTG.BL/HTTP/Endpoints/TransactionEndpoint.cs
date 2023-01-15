using Newtonsoft.Json;
using MCTG.DAL;
using MCTG.DAL.Repository;
using MCTG.Models;
using MCTG.Models.HTTP;
using MCTG.Models.Exceptions;
using HttpMethod = MCTG.Models.HTTP.HttpMethod;

namespace MCTG.BL.HTTP.Endpoints
{
    public class TransactionEndpoint : IHttpEndpoint
    {
        private UserRepository userRepository = new();
        private CardRepository cardRepository = new();
        private PackageRepository packageRepository = new();
        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    PurchasePackage(rq, rs);
                    break;
            }
        }

        private void PurchasePackage(HttpRequest rq, HttpResponse rs)
        {
            try 
            {
                var token = rq.headers["Authorization"].Split(" ")[1];
                var id = userRepository.GetIdFromUsername(token.Split("-")[0]);
                var user = userRepository.GetById(id);
                if (user.Coins < Package.PRICE) 
                    throw new NotEnoughCoinsException();
                
                var next = packageRepository.GetNext();
                if (next == null) 
                    throw new NoPackagesLeftException();

                packageRepository.Delete(next);
                foreach (var card in next.Cards)
                {
                    var cardT = cardRepository.GetById(card.GetId());
                    cardT.SetOwner(user.Guid);
                    cardRepository.Update(cardT);
                }

                user.Coins -= Package.PRICE;
                userRepository.Update(user);

                rs.ContentType = "application/json";
                rs.Content = JsonConvert.SerializeObject(next);
                rs.ResponseCode = 200;
                rs.ResponseText = "A package has been successfully bought";
            }
            catch (InvalidAccessTokenException)
            {
                rs.ResponseCode = 401;
                rs.ResponseText = "Access token is missing or invalid";
            }
            catch (NotEnoughCoinsException)
            {
                rs.ResponseCode = 403;
                rs.ResponseText = "Not enough money for buying a card package";
            }
            catch (NoPackagesLeftException)
            {
                rs.ResponseCode = 404;
                rs.ResponseText = "No card package available for buying";
            }
        }
    }
}
