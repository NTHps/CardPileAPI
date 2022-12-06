namespace CardPile.Application.Dtos
{

    public class DeckListDto
    {

        #region - - - - - - Properties - - - - - -

        public long DeckListID { get; set; }
        public string Name { get; set; } = "";
        public List<CardDto> Cards { get; set; } = new();

        #endregion Properties

    }

}
