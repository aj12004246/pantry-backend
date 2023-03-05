namespace pantry_be.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int FriendId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool MyFriend { get; set; }
        public bool IsFriend { get; set; }
        public List<Recipe> FriendRecipes { get; set; }
    }
}
