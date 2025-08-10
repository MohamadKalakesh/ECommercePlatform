using CustomerService.Application.Abstractions;
using Grpc.Core;
using Shared.Protos;

namespace CustomerService.API.Services
{
    public class CustomerGrpcService : Shared.Protos.CustomerService.CustomerServiceBase
    {
        private readonly ICustomerService _customerService;

        public CustomerGrpcService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public override async Task<CustomerResponse> GetCustomerByEmail(GetCustomerByEmailRequest request, ServerCallContext context)
        {
            try
            {
                var customer = await _customerService.GetByEmailAsync(request.Email);

                if (customer == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, $"Customer with email {request.Email} not found"));
                }

                return new CustomerResponse
                {
                    Id = customer.Id.ToString(),
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    CreatedAt = customer.CreatedAt.ToString("O") // ISO 8601 format
                };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error retrieving customer: {ex.Message}"));
            }
        }
    }
}