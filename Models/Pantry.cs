namespace pantry_be.Models
{
    public class Pantry
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public List<Item> Items { get; set; }
    }
}
