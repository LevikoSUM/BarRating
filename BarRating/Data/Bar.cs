using System.ComponentModel.DataAnnotations;

namespace BarRating.Data
{
    public class Bar
    {
        public Bar()
        {
            Reviews = new HashSet<Review>();
        }
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
