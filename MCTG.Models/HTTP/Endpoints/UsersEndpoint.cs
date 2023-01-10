using Newtonsoft.Json;

namespace MCTG.Models.HTTP.Endpoints
{
    public class UsersEndpoint : IHttpEndpoint
    {
        private List<User> users = new(); //ToDo Change List of Users to DAL
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
            catch (NotValidAccessTokenException)
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

                if (users.Exists(x => x.Equals(user)))
                    throw new NotImplementedException(); //ToDo Change
                
                users.Add(user);

                //ToDo Call BL

                rs.ResponseCode = 201;
                rs.ResponseText = "OK";
                rs.Content = "User successfully created";
            }
            catch (Exception ex) //ToDo change exception Type specific
            {
                rs.ResponseCode = 409;
                rs.Content = "User with same username already registered";
            }
           /* catch (Exception) //ToDo Catch any exception
            {
                rs.ResponseCode = 400;
                rs.Content = "Failed to create User!";
            }*/
        }

        private void GetUser(HttpRequest rq, HttpResponse rs)
        {
            //ToDo BL
            //ToDo Admin or matching User
            var user = new User(); //ToDo
            //if(admin || user.name == matching)
            //else 

            var users = new List<User>();
            users.Add(new User(Guid.NewGuid(), "Rudi Ratlos", "1234"));
            users.Add(new User(Guid.NewGuid(), "Susi Sorglos", "0000"));

            rs.Content = JsonConvert.SerializeObject(user);
            rs.ContentType = "application/json";
            rs.ResponseCode = 200;
            rs.ResponseText = "OK";
        }
    }
}
