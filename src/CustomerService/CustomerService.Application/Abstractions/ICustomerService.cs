using CustomerService.Application.Customers.Commands;
using CustomerService.Application.Customers.DTOs;

namespace CustomerService.Application.Abstractions
{
    public interface ICustomerService
    {
        Task<CustomerDto> CreateAsync(RegisterCustomerCommand command);
        Task<CustomerDto?> GetByEmailAsync(string email);
    }
}
