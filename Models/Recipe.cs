namespace pantry_be.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public List<Step> Steps { get; set; }
    }
}
