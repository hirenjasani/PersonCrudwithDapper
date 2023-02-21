namespace PersonCrudwithDapper.DTO
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Hobbies { get; set; }
        public bool AcceptTermsAndCondition { get; set; }
    }
}
