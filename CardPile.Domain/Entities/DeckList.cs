using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPile.Domain.Entities
{

    public class DeckList
    {

        public DeckList()
        {
            this.Cards = new HashSet<Card>();
        }

        public long DeckListID { get; set; }
        public string DeckListName { get; set; }
        public ICollection<Card> Cards { get; set; }

    }

}
