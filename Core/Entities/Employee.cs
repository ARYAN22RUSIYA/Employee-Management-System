namespace Core.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Age { get; set; }
        public ICollection<Document> Documents { get; set; } = new List<Document>();

    }
}
