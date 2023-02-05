namespace CardPile.Application.Common.CodeContractgs
{

    public class ItemExpressionIsNullException : Exception
    {

        #region - - - - - - Constructors - - - - - -

        public ItemExpressionIsNullException() : base("Item Expression cannot be null.") { }

        #endregion

    }

}
