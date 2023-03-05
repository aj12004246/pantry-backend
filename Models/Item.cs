namespace pantry_be.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public int Weight { get; set; }
        public int Calories { get; set; }
      
    }
}
