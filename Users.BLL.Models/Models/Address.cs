namespace Users.BLL.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public User User { get; set; }
    }
}