using System.ComponentModel.DataAnnotations;

namespace LevelUp.Models
{
    public class RegisrationModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}