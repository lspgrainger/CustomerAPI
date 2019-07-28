namespace Api.Customer.Controllers
{
    public class CustomerControllerDto
    {
        public static Domain.Customer MapToDomain(RequestCreate @new)
        {
            var customer = new Domain.Customer
            {
                Forename = @new.Forename,
                Surname = @new.Surname,
                EmailAddress = @new.EmailAddress,
                Password = @new.Password
            };
            return customer;
        }

        public static Domain.Customer MapToDomain(RequestUpdate update)
        {
            var customer = new Domain.Customer
            {
                CustomerId = update.CustomerId,
                Forename = update.Forename,
                Surname = update.Surname,
                EmailAddress = update.EmailAddress,
                Password = update.Password
            };
            return customer;
        }

        public class RequestLogin
        {
            public int CustomerId { get; set; }
            public string Password { get; set; }
        }

        public class RequestCreate
        {
            public string Forename { get; set; }
            public string Surname { get; set; }
            public string EmailAddress { get; set; }
            public string Password { get; set; }
        }

        public class RequestUpdate
        {
            public int CustomerId { get; set; }
            public string Forename { get; set; }
            public string Surname { get; set; }
            public string EmailAddress { get; set; }
            public string Password { get; set; }
        }
    }
}