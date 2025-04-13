using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCards
{
    class Card
    {
        public int id { get; private set; }
        public string content { get; private set; }
        public DateTime lastRead { get; set; }
        public int timesRead { get; set; }
        public string user { get; private set; }
        public string answer { get; private set; }


        public Card(int id, string content, DateTime lastRead, string user, string answer)
        {
            this.id = id;
            this.content = content;
            this.lastRead = lastRead;
            this.timesRead = 0;
            this.user = user;
            this.answer = answer;
        }
    }

}
