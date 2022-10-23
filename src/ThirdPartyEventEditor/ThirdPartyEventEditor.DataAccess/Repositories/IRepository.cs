using System.Collections.Generic;
using ThirdPartyEventEditor.DataAccess.Models;

namespace ThirdPartyEventEditor.DataAccess.Repositories
{
    public interface IRepository<TModel, TIdType>
        where TModel : Model<TIdType>
    {
        IList<TModel> GetAll();

        void Insert(TModel model);

        void Update(TModel updatedModel);

        void Delete(TIdType id);
    }
}