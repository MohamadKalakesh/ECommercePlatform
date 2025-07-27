namespace CustomerService.Application.Customers.Commands
{
    public class RegisterCustomerCommand
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }

        public RegisterCustomerCommand(string firstName, string lastName, string email, string phoneNumber)
        {
            FirstName=firstName;
            LastName=lastName;
            Email=email;
            PhoneNumber=phoneNumber;
        }
    }
}
