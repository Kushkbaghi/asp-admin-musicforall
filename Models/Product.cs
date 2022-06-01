using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicForAllAdmin.Models
{
    public class Product

    {
        [Key]
        public int Id { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Produktnamn krävs!")]
        [Display(Name = "Produktnamn")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Pris krävs!")]
        [Display(Name = "Pris")]
        public int? Price { get; set; }

        [Required(ErrorMessage = "Antal produkt krävs!")]
        [Display(Name = "Antal i lager")]
        public int? Quantity { get; set; } = 1;

        [Display(Name = "Använd som banner")]
        public int? Banner { get; set; } = 0; // If it's 1 set as Banner

        [Display(Name = "Anävnd som CTA")]
        public int? Cta { get; set; } = 0; // If it's 1 set as CTA

        [StringLength(32)]
        [Display(Name = "Produkt bild")]
        public string? Image { get; set; }

        [StringLength(32)]
        [Required(ErrorMessage = "Bildebeskrivning krävs!")]
        [Display(Name = "Bild beskrivning")]
        public string? ImageAlt { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }       // To store image files

        [Display(Name = "Betyg")]
        public float? Rate { get; set; } = 0;

        [StringLength(32)]
        [Display(Name = "Skapat av")]
        public string? CreatedBy { get; set; } = "Okänd";

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
               ApplyFormatInEditMode = true)]
        [Display(Name = "Skapat i")]
        public DateTime? CreateAt { get; set; } = DateTime.Now;

        //// Navigation properties
        //public int SoldProductId { get; set; }

        //public SoldProduct? SoldProduct { get; set; }
    }
}