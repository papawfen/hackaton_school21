using Grpc.Net.Client;
using GrpcGreeterClient;

namespace WebMvc.Application.Services;

public class AuthService : IAuthService
{
    public string Echo(string name)
    {
        using var channel = GrpcChannel.ForAddress("http://localhost:5289");
        var client = new Greeter.GreeterClient(channel);
        var reply = client.SayHello(new HelloRequest() { Name = name });
        return reply.Message;
    }
}