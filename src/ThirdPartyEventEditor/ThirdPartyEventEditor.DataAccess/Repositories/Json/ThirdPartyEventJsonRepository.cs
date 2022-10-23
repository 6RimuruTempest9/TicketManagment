using System.Linq;
using ThirdPartyEventEditor.DataAccess.Models;
using ThirdPartyEventEditor.DataAccess.Repositories.Json.Options;
using ThirdPartyEventEditor.CommonElements.Converters;

namespace ThirdPartyEventEditor.DataAccess.Repositories.Json
{
    public class ThirdPartyEventJsonRepository : JsonRepository<ThirdPartyEvent, int>
    {
        public ThirdPartyEventJsonRepository(JsonRepositoryOptions<ThirdPartyEvent, int> options)
            : base(options)
        {
        }

        public override void Insert(ThirdPartyEvent model)
        {
			var imageUrl = model.PosterImageUrl;

            model.PosterImageBase64 = ImageConverter.ConvertImageURLToBase64String(imageUrl);

            base.Insert(model);
        }

		private protected override int GenerateUniqueId()
        {
            var models = GetAll();

            if (models.Count == 0)
            {
                return 1;
            }

            var minId = models.Min(model => model.Id);
            var maxId = models.Max(model => model.Id);

            var modelCount = models.Count;

            var uniqueId = maxId + 1;

            if (maxId - minId + 1 > modelCount)
            {
                for (var id = minId + 1; id < maxId; id++)
                {
                    if (!models.Any(model => model.Id == id))
                    {
                        uniqueId = id;

                        break;
                    }
                }
            }

            return uniqueId;
        }
    }
}