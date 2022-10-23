using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Web.ViewModels.User
{
    public class AddBalanceViewModel
    {
        [Required]
        public decimal AdditionalBalance { get; set; }
    }
}