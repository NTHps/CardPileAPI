using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public class CreateCardDto
    {

        #region - - - - - - Properties - - - - - -

        public Func<long> CardID { get; set; }

        public string Name { get; set; }

        #endregion Properties

    }

}
