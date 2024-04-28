using Microsoft.AspNetCore.Identity;

namespace BarRating.Data
{
    public class User : IdentityUser
    {
        public User()
        {
            Reviews = new HashSet<Review>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
