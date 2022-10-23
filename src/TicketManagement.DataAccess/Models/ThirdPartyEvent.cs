using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.DataAccess.Models
{
    public class ThirdPartyEvent : Model
    {
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

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
        [Display(Name = "Poster image URL")]
        [DataType(DataType.ImageUrl)]
        public string PosterImageUrl { get; set; }

        [Display(Name = "Poster image")]
        public string PosterImageBase64 { get; set; }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}