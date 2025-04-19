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
        private string password;
        public string name { get; private set; }
        public List<int> cardIds { get; set; }
        public int points { get; set; }

        public User(string name, string password)
        {
            this.name = name;
            this.password = (passwordEncryption.Encrypt(password));
            this.cardIds = new List<int> {};
            this.points = 0;
        }

        public User? Login(string password)
        {
            if (passwordEncryption.Decrypt(password, this.password))
            {
                return this;
            }
            else
            {
                return null;
            }
        }
    }
}