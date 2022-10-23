using ThirdPartyEventEditor.DataAccess.Models;

namespace ThirdPartyEventEditor.DataAccess.Repositories.Json.Options
{
    public class JsonRepositoryOptions<TModel, TIdType>
        where TModel : Model<TIdType>
    {
        public string PathToJsonFile { get; set; }
    }
}