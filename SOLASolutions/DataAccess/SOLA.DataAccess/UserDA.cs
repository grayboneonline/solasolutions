﻿using System.Collections.Generic;
using PetaPoco;
using SOLA.DataAccess.Contracts;
using SOLA.Models;

namespace SOLA.DataAccess
{
    public class UserDA : IUserDA
    {
        
        public UserDA()
        {

        }

        public IEnumerable<User> GetAll()
        {
            var db = new Database("");
            return db.Query<User>("SELECT * FROM [Users]");
        }

        public User GetById(int id)
        {
            var db = new Database("");
            return db.SingleOrDefault<User>("SELECT * FROM [Users] WHERE UserId = @0", id);
        }

        public void Add(User user)
        {
            var db = new Database("");
            db.Insert(user);
        }

        public void Update(User user)
        {
            var db = new Database("");
            db.Update(user);
        }

        public void Delete(int id)
        {
            var db = new Database("");
            db.Delete<User>(id);
        }
    }
}