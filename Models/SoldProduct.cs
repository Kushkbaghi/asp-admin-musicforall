using System.ComponentModel.DataAnnotations;

namespace MusicForAllAdmin.Models
{
    public class SoldProduct
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Antal")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Product")]
        public List<Product>? Products { get; set; }
    }
}