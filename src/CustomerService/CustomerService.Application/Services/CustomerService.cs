using CustomerService.Application.Abstractions;
using CustomerService.Application.Customers.Commands;
using CustomerService.Application.Customers.DTOs;
using CustomerService.Domain.Customers;

namespace CustomerService.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository=repository;
        }

        public async Task<CustomerDto> CreateAsync(RegisterCustomerCommand command)
        {
            var customer = await _repository.AddAsync(new Customer(command.FirstName, command.LastName, command.Email, command.PhoneNumber));
            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt
            };
        }

        public async Task<CustomerDto?> GetByEmailAsync(string email)
        {
            var customer = await _repository.GetByEmailAsync(email);
            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
