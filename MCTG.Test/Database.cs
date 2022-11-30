using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.DAL;
using MCTG.Models;

namespace MCTG.Test
{
    internal class Database
    {
        private DataLayerPostgres dal;

        [SetUp]
        public void Setup()
        {
            dal = new();
        }

        //selbe DB? -> MOcking
        [Test]
        public void CreateAndDeleteUser()
        {
            User aUser = new User(Guid.NewGuid(), "Test1", "*****", 100, "");
            User cUser = new User(Guid.NewGuid(), "Test3", "*****", 100, "");

            try
            {
                dal.CreateUser(aUser);
                Assert.That(dal.FindUserByName("Test1").Equals(aUser));
                Assert.IsNull(dal.FindUserByName(cUser.Username));
                Assert.NotNull(dal.FindUserByName(aUser.Username));
            }
            finally
            {
                dal.DeleteUser(aUser);//-> TryToDeleteUser
                Assert.IsNull(dal.FindUserByName(aUser.Username));
            }
        }

        [Test]
        public void UpdateUser()
        {
            Guid guid = Guid.NewGuid();
            User aUser = new User(guid, "Test1", "*****", 100, "");
            User cUser = new User(guid, "Test1", "*****", 150, "");

            dal.CreateUser(aUser);
            Assert.That(dal.GetUser(guid.ToString()).Equals(aUser));
            Assert.That(!dal.GetUser(guid.ToString()).Equals(cUser));
            dal.UpdateUser(cUser);
            Assert.That(dal.GetUser(guid.ToString()).Equals(cUser));
            Assert.That(!dal.GetUser(guid.ToString()).Equals(aUser));
            dal.DeleteUser(cUser);
        }

        [Test]
        public void ListUser()
        {
            User aUser = new User(Guid.NewGuid(), "Test1", "*****", 100, "");
            User bUser = new User(Guid.NewGuid(), "Test2", "*****", 100, "");
            User cUser = new User(Guid.NewGuid(), "Test3", "*****", 100, "");

            Assert.Zero(dal.GetAllUsers().Count);
            dal.CreateUser(aUser);
            Assert.That(dal.GetAllUsers().Count == 1);
            dal.CreateUser(bUser);
            Assert.That(dal.GetAllUsers().Count == 2);
            dal.CreateUser(cUser);
            Assert.That(dal.GetAllUsers().Count == 3);

            dal.DeleteUser(aUser);
            Assert.That(dal.GetAllUsers().Count == 2);
            dal.DeleteUser(bUser);
            Assert.That(dal.GetAllUsers().Count == 1);
            dal.DeleteUser(cUser);
            Assert.Zero(dal.GetAllUsers().Count);
        }
    }
}