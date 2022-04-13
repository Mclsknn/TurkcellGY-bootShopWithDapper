using bootShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bootShop.Business
{
    public class UserService : IUserService
    {
        private List<User> users;
        public UserService()
        {
            users = new List<User>()
            {
                new User{Id=1, Email = "user1@test.com", FullName="user one", Password="123", UserName="user1", Role="Admin"},
                new User{Id=2, Email = "user2@test.com", FullName="user two", Password="123", UserName="user2", Role="Editor"},
                new User{Id=3, Email = "user3@test.com", FullName="user three", Password="123", UserName="user3", Role="Client"}
            };
        }

        public User ValidateUser(string userName, string password) 
        {
            return users.FirstOrDefault(x => x.UserName == userName && x.Password == password);
        }
    }
}
