using System;
using System.Collections.Generic;
using ThirdPartyEventEditor.DataAccess.Models;
using ThirdPartyEventEditor.DataAccess.Repositories;

namespace ThirdPartyEventEditor.BusinessLogic.Services
{
    public abstract class Service<TModel, TIdType> : IService<TModel, TIdType>
        where TModel : Model<TIdType>
    {
        private readonly IRepository<TModel, TIdType> _repository;

        public Service(IRepository<TModel, TIdType> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        private protected IRepository<TModel, TIdType> Repository => _repository;

        public abstract void Delete(TIdType id);

        public abstract IList<TModel> GetAll();

        public abstract void Insert(TModel model);

        public abstract void Update(TModel updatedModel);
    }
}