using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.ViewModels.Event
{
    public class UpdateEventViewModel
    {
        [Required]
        public int Id { get; set; }

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
    }
}