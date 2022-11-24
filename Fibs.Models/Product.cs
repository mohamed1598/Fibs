using System.ComponentModel.DataAnnotations;

namespace Fibs.Models
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
            FavoritesNo = 0;
            favoriteProducts = new HashSet<FavoriteProduct>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public int NoOfPieces { get; set; }
        [Required]
        public string Material { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public int FavoritesNo { get; set; }
        public byte[]? Picture { get; set; }
        public ICollection<FavoriteProduct> favoriteProducts { get; set; }
        public ICollection<ShoppingBag> ShoppingBags { get; set; }
    }
}
