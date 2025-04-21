namespace Core.Entities
{
    public class Document : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
