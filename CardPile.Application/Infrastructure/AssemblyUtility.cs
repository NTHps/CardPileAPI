using System.Reflection;

namespace CardPile.Application.Infrastructure
{

    public class AssemblyUtility
    {

        #region - - - - - - Methods - - - - - -

        public static Assembly GetAssembly() => typeof(AssemblyUtility).Assembly;

        #endregion Methods

    }

}
