namespace Api.Customer.Domain
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}