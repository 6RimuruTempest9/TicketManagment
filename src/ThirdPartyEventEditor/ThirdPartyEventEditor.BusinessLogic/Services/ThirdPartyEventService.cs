using System.Collections.Generic;
using ThirdPartyEventEditor.DataAccess.Models;
using ThirdPartyEventEditor.DataAccess.Repositories;

namespace ThirdPartyEventEditor.BusinessLogic.Services
{
    public class ThirdPartyEventService : Service<ThirdPartyEvent, int>
    {
        public ThirdPartyEventService(IRepository<ThirdPartyEvent, int> repository)
            : base(repository)
        {
        }

        public override void Delete(int id)
        {
            Repository.Delete(id);
        }

        public override IList<ThirdPartyEvent> GetAll()
        {
            return Repository.GetAll();
        }

        public override void Insert(ThirdPartyEvent model)
        {
            Repository.Insert(model);
        }

        public override void Update(ThirdPartyEvent updatedModel)
        {
            Repository.Update(updatedModel);
        }
    }
}