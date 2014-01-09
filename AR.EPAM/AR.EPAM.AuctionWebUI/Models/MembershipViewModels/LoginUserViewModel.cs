using System.ComponentModel.DataAnnotations;

namespace AR.EPAM.AuctionWebUI.Models.MembershipViewModels
{
    public class LoginUserViewModel : ViewModel
    {
        [Required]
        [MaxLength(40)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [MaxLength(40)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}