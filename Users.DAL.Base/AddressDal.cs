namespace Users.DAL.Base
{
    public class AddressDal
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public virtual UserDal User { get; set; }
    }
}