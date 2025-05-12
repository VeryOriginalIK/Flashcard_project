using Newtonsoft.Json;
using System.Collections.Generic;

namespace MemoryCards
{
    public class User
    {
        public string password { get; private set; }
        public string name { get; private set; }
        public List<int> cardIds { get; set; }
        public int points { get; set; }

        public User(string name, string password)
        {
            this.name = name;
            this.password = passwordEncryption.Encrypt(password);
            this.cardIds = new List<int>();
            this.points = 0;
        }

        [JsonConstructor]
        public User(string name, string password, List<int> cardIds, int points)
        {
            this.name = name;
            this.password = password;
            this.cardIds = cardIds;
            this.points = points;
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