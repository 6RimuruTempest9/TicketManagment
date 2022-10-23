using System;

namespace TicketManagement.Web.Models
{
    public class EventModel
    {
        public int Id { get; set; }

        public int LayoutId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ImageUrl { get; set; }

        public static string GetLocalImageUrlByImageFileName(string imageFileName)
        {
            return "~/images/" + imageFileName.Replace('/', '.').Replace(':', '.');
        }

        public static string GetImageFileName(EventModel eventModel)
        {
            var imageFileName = eventModel.Name
                                + "_" + eventModel.StartDate
                                + "_" + eventModel.EndDate + ".png";

            return imageFileName;
        }
    }
}