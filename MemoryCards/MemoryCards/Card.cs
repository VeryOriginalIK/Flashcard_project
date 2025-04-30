using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MemoryCards
{
    public class Card
    {
        public int id { get; private set; }
        public string content { get; private set; }
        public DateTime lastRead { get; set; }
        public int timesRead { get; set; }
        public string answer { get; private set; }
        public int type { get; set; }
        public string[] options { get; set; } = new string[4];


        public Card(int id, string content, DateTime lastRead, string answer, int type, string[] options)
        {
            this.id = id;
            this.content = content;
            this.lastRead = lastRead;
            this.timesRead = 0;
            this.answer = answer;
            this.type = type;
            this.options = [options[0], options[1], options[2], options[3]];
        }
    }
    public class CardList
    {
        public List<Card> Cards { get; set; } = new List<Card>() { };
    }

}
