namespace pantry_be.Models
{
    public class User
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
