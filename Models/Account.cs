namespace pantry_be.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<User> Users { get; set; }
        public List<Friend> Friends { get; set; }
        public List<Item> Items { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
