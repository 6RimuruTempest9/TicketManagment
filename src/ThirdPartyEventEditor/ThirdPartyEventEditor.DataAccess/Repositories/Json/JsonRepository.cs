using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ThirdPartyEventEditor.CommonElements.Exceptions;
using ThirdPartyEventEditor.DataAccess.Models;
using ThirdPartyEventEditor.DataAccess.Repositories.Json.Options;

namespace ThirdPartyEventEditor.DataAccess.Repositories.Json
{
    public abstract class JsonRepository<TModel, TIdType> : IRepository<TModel, TIdType>
        where TModel : Model<TIdType>
    {
        private object fileLock = new object();

        private readonly string _pathToJsonFile;

        private protected JsonRepository(JsonRepositoryOptions<TModel, TIdType> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _pathToJsonFile = options.PathToJsonFile;
        }

        public virtual void Delete(TIdType id)
        {
            lock (fileLock)
            {
                var content = File.ReadAllText(_pathToJsonFile);

                var models = JsonConvert.DeserializeObject<IList<TModel>>(content);

                var modelToRemove = models.FirstOrDefault(model => model.Id.Equals(id));

                models.Remove(modelToRemove);

                var newContent = JsonConvert.SerializeObject(models);

                File.WriteAllText(_pathToJsonFile, newContent);
            }
        }

        public virtual IList<TModel> GetAll()
        {
            lock (fileLock)
            {
                var content = File.ReadAllText(_pathToJsonFile);

                return JsonConvert.DeserializeObject<IList<TModel>>(content);
            }
        }

        public virtual void Insert(TModel model)
        {
            lock (fileLock)
            {
                var content = File.ReadAllText(_pathToJsonFile);

                var models = JsonConvert.DeserializeObject<IList<TModel>>(content);

                var uniqueId = GenerateUniqueId();

                model.Id = uniqueId;

                models.Add(model);

                var newContent = JsonConvert.SerializeObject(models);

                File.WriteAllText(_pathToJsonFile, newContent);
            }
        }

        public virtual void Update(TModel updatedModel)
        {
            lock (fileLock)
            {
                var content = File.ReadAllText(_pathToJsonFile);

                var models = JsonConvert.DeserializeObject<IList<TModel>>(content);

                var modelToRemove = models.FirstOrDefault(m => m.Id.Equals(updatedModel.Id));

                if (modelToRemove == null)
                {
                    throw new ModelNotFoundException<TModel>();
                }

                models.Remove(modelToRemove);

                models.Add(updatedModel);

                var newContent = JsonConvert.SerializeObject(models);

                File.WriteAllText(_pathToJsonFile, newContent);
            }
        }

        private protected abstract TIdType GenerateUniqueId();
    }
}