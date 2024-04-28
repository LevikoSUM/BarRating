namespace BarRating.Data
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public virtual User? User { get; set; }
        public int BarId { get; set; }
        public virtual Bar? Bar { get; set; }
    }
}
