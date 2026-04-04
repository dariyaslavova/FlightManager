using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightManager.Data.Models;

namespace FlightManager.Services.Services
{
    public interface IUserService
    {
        User? Login(string username, string password);
        IEnumerable<User> GetAll();
        User? GetById(int id);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
        bool IsFirstUserCreated();
    }
}