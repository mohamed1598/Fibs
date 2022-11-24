using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Fibs.Models
{
    public class ApplicationUser:IdentityUser
    {
        public ApplicationUser()
        {
            favoriteProducts = new HashSet<FavoriteProduct>();
        }
        [Required]
        public string FullName { get; set; }
        public byte[]? profilePicture { get; set; }
        public ICollection<FavoriteProduct> favoriteProducts { get; set; }
        public ICollection<ShoppingBag> ShoppingBag { get; set; }
    }
}
