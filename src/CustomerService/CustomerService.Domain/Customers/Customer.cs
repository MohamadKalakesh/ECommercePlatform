namespace CustomerService.Domain.Customers
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Customer(string firstName, string lastName, string email, string phoneNumber)
        {
            Id=Guid.NewGuid();
            FirstName=firstName;
            LastName=lastName;
            Email=email;
            PhoneNumber=phoneNumber;
            CreatedAt=DateTime.Now;
        }
    }
}
