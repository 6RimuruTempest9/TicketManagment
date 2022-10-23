using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TicketManagement.DataAccess.Entities;

namespace TicketManagement.Web.ViewModels.Event
{
    public class CreateEventViewModel
    {
        [Required]
        [Display(Name = "Layout Id")]
        public int LayoutId { get; set; }

        [Required]
        [Display(Name = "Start date")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End date")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Image URL")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public static bool HasAnySeats(Venue venue, IEnumerable<Layout> layouts, IEnumerable<Area> areas, IEnumerable<Seat> seats)
        {
            if (venue == null)
            {
                throw new ArgumentNullException(nameof(venue));
            }

            if (!layouts.Any(layout => layout.VenueId == venue.Id))
            {
                return false;
            }

            foreach (var layout in layouts.Where(layout => layout.VenueId == venue.Id))
            {
                if (HasAnySeats(layout, areas, seats))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HasAnySeats(Layout layout, IEnumerable<Area> areas, IEnumerable<Seat> seats)
        {
            if (layout == null)
            {
                throw new ArgumentNullException(nameof(layout));
            }

            if (seats.Any(seat => areas.Where(area => area.LayoutId == layout.Id).Any(area => area.Id == seat.AreaId)))
            {
                return true;
            }

            return false;
        }
    }
}