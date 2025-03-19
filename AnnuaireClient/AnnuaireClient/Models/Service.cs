namespace AnnuaireClient.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
