using System.Collections.Generic;
using ThirdPartyEventEditor.DataAccess.Models;

namespace ThirdPartyEventEditor.BusinessLogic.Services
{
    public interface IService<TModel, TIdType>
        where TModel : Model<TIdType>
    {
        IList<TModel> GetAll();

        void Insert(TModel model);

        void Update(TModel updatedModel);

        void Delete(TIdType id);
    }
}