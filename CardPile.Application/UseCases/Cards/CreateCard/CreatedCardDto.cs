namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public class CreatedCardDto
    {

        #region - - - - - - Properties - - - - - -

        public Func<long> CardID { get; set; }

        public string Name { get; set; }

        #endregion Properties

    }

}
