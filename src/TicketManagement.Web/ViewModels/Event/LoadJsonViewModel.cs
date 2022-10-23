using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TicketManagement.Web.Models;

namespace TicketManagement.Web.ViewModels.Event
{
    public class LoadJsonViewModel
    {
        [Required]
        public IFormFile File { get; set; }

        public static async Task ConvertImagesFromBase64StringToPngAndSave(
            IList<ThirdPartyEvent> thirdPartyEvents,
            string pathToFolderWithFolder)
        {
            foreach (var thirdPartyEvent in thirdPartyEvents)
            {
                var imageFileName = thirdPartyEvent.Name + "_" +
                            thirdPartyEvent.StartDate.ToString() + "_" +
                            thirdPartyEvent.EndDate.ToString() + ".png";

                var imageFilePath = Path.Combine(pathToFolderWithFolder, imageFileName.Replace('/', '.').Replace(':', '.'));

                using (var stream = new MemoryStream())
                {
                    var imageBytes = Convert.FromBase64String(thirdPartyEvent.PosterImageBase64);

                    await stream.WriteAsync(imageBytes, 0, imageBytes.Length);

                    using (var image = Image.FromStream(stream))
                    {
                        image.Save(imageFilePath, ImageFormat.Png);
                    }
                }
            }
        }
    }
}