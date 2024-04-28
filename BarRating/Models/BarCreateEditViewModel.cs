using BarRating.Data;

namespace BarRating.Models
{
    public class BarCreateEditViewModel : Bar
    {
        public IFormFile Picture { get; set; }
    }
}
