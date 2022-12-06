﻿namespace CardPile.Domain.Entities
{

    public class DeckList
    {

        #region - - - - - - Properties - - - - - -

        public long DeckListID { get; set; }
        public ICollection<Card> Cards { get; set; }
        public string Name { get; set; } = "";

        #endregion Properties

    }

}
