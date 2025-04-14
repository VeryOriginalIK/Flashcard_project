using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryCards;

namespace MemoryCards
{
    class User
    {
        public int id { get; private set; }
        private string password;
        public string name { get; private set; }
        public List<int> cardIds { get; set; }
        public int points { get; set; }

        public User(int id, string name, string password, int points)
        {
            this.id = id;
            this.name = name;
            this.password = (passwordEncryption.Encrypt(password));
            this.cardIds = new List<int> {};
            this.points = points;
        }

        public List<int>? Login(string password)
        {
            if (passwordEncryption.Decrypt(password, this.password))
            {
                return this.cardIds;
            }
            else
            {
                return null;
            }
        }
    }
}