namespace CustomerService.Application.Customers.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string PhoneNumber { get; init; } = default!;
        public DateTime CreatedAt { get; init; } = default!;
    }
}
