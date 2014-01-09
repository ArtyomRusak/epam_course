using System.ComponentModel.DataAnnotations;

namespace AR.EPAM.AuctionWebUI.Models.MembershipViewModels
{
    public class RegisterUserViewModel : ViewModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(40)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}