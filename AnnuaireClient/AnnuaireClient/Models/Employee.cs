namespace AnnuaireClient.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string MobilePhoneNumber { get; set; } = null!;

        public string FixedPhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int AgencyId { get; set; }

        public int ServiceId { get; set; }
    }
}
