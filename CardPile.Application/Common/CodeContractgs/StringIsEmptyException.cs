namespace CardPile.Application.Common.CodeContractgs
{

    public class StringIsEmptyException : ArgumentException
    {

        #region - - - - - - Constructors - - - - - -

        public StringIsEmptyException(string paramName) : base($"{paramName} cannot be String.Empty.", paramName) { }

        #endregion

    }

}
