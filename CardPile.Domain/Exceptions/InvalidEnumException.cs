using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPile.Domain.Exceptions
{

    public class InvalidEnumException : Exception
    {

        #region - - - - - - Constructors - - - - - -

        public InvalidEnumException(string resourceName) : base($"{resourceName} is not a valid enum.") { }

        #endregion Constructors

    }

}
