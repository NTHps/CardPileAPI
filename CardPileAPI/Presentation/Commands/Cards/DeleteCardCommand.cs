using System.Text.Json.Serialization;

namespace CardPileAPI.Presentation.Commands.Cards
{

    /// <summary>
    /// The Command to delete a Card
    /// </summary>
    public class DeleteCardCommand
    {

        #region - - - - - - Properties - - - - - -

        [JsonIgnore]
        public long CardID { get; set; }

        #endregion Properties

    }

}
