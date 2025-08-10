using Grpc.Net.Client;
using Shared.Protos;
using System.Net.Http;

namespace ProductService.API.Services
{
    public interface ICustomerGrpcClient
    {
        Task<CustomerResponse?> GetCustomerByEmailAsync(string email);
    }

    public class CustomerGrpcClient : ICustomerGrpcClient
    {
        private readonly CustomerService.CustomerServiceClient _client;
        private readonly ILogger<CustomerGrpcClient> _logger;

        public CustomerGrpcClient(ILogger<CustomerGrpcClient> logger, IConfiguration configuration)
        {
            _logger = logger;
            
            var customerServiceUrl = configuration["CustomerService:GrpcUrl"] 
                ?? throw new InvalidOperationException("CustomerService:GrpcUrl configuration is missing");
            
            var channel = GrpcChannel.ForAddress(customerServiceUrl, new GrpcChannelOptions
            {
                HttpHandler = new SocketsHttpHandler
                {
                    EnableMultipleHttp2Connections = true,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                    KeepAlivePingPolicy = HttpKeepAlivePingPolicy.WithActiveRequests,
                    SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                    {
                        RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
                    }
                }
            });
            _client = new CustomerService.CustomerServiceClient(channel);
        }

        public async Task<CustomerResponse?> GetCustomerByEmailAsync(string email)
        {
            try
            {
                var request = new GetCustomerByEmailRequest { Email = email };
                var response = await _client.GetCustomerByEmailAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling CustomerService gRPC: {Message}", ex.Message);
                return null;
            }
        }
    }
} 