using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
                    GetUsers(rq, rs);
                    break;
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

        private void GetUsers(HttpRequest rq, HttpResponse rs)
        {
            //ToDo BL
            var users = new List<User>();
            users.Add(new User(Guid.NewGuid(), "Rudi Ratlos", "1234"));
            users.Add(new User(Guid.NewGuid(), "Susi Sorglos", "0000"));

            rs.Content = JsonConvert.SerializeObject(users);
            rs.ContentType = "application/json";
            rs.ResponseCode = 200;
            rs.ResponseText = "OK";
        }
    }
}
