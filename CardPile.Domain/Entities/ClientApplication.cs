using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardPile.Domain.Entities
{

    public class ClientApplication
    {

        #region - - - - - - Constructors - - - - - -

        public ClientApplication()
        {
            this.ClientApplicationScope = new HashSet<ClientApplicationScope>();
            this.ClientApplicationGrantTypes = new HashSet<ClientApplicationGrantType>();
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public long ClientApplicationID { get; set; }
        public string AccessToken { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }


        // Navigation Properties ---------------------------------------------------------

        public ICollection<ClientApplicationScope> ClientApplicationScope { get; set; }
        public ICollection<ClientApplicationGrantType> ClientApplicationGrantTypes { get; set; }

        #endregion Properties

    }

}
