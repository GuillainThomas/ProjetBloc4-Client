namespace AnnuaireClient.Models
{
    public class Agency
    {
        public int Id { get; set; }
        
        public string City { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
