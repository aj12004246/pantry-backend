namespace pantry_be.Models
{
    public class Step
    {
        public int Id { get; set; }
        public int? RecipeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? ItemId { get; set; }
        public string? ItemName { get; set; } = string.Empty;
        public int? ItemWeight { get; set; }

    }
}
