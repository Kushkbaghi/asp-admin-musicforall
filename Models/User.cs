using System.ComponentModel.DataAnnotations;

namespace MusicForAllAdmin.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Användarnamn krävs!")]
        [Display(Name = "Användarnamn")]
        public string? Name { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "E-post krävs!")]
        [Display(Name = "E-post")]
        public string? Email { get; set; }

        [StringLength(32)]
        [Display(Name = "Lössenord")]
        [Required(ErrorMessage = "Lösenord krävs!")]
        public string? Password { get; set; }
    }
}