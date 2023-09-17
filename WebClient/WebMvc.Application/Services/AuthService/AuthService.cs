using System;
using System.Text;
using System.Threading.Tasks;
using WebMvc.Application.Models;
using AuthServiceApp;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using WebMvc.Application.Services.Configuration;

namespace WebMvc.Application.Services.AuthService;

public sealed class AuthService : IAuthService
{
    private readonly AuthServiceConfiguration _authServiceConfiguration;

    public AuthService(IOptions<AuthServiceConfiguration> authService)
    {
        _authServiceConfiguration = authService.Value;
    }

    public async Task RegisterAsync(User user)
    {
        var channel = GrpcChannel.ForAddress(_authServiceConfiguration.Address);
        var client = new Auth.AuthClient(channel);
        var reply = await client.UserRegisterAsync(new UserCredentials
            { Login = user.Login, Password = user.Password });

        if (reply.Status is not AuthStatus.SignUpComplete)
        {
            if (reply.Status is AuthStatus.UserAlreadyExists)
            {
                throw new Exception("User already exists");
            }

            throw new Exception("Unknown error");
        }
    }

    public async Task<UserInfo> LoginAsync(User user)
    {
        var channel = GrpcChannel.ForAddress(_authServiceConfiguration.Address);
        var client = new Auth.AuthClient(channel);
        var reply = await client.UserAuthenticateAsync(new UserCredentials
            { Login = user.Login, Password = user.Password });

        if (reply.Status is not AuthStatus.SignInComplete)
        {
            if (reply.Status is AuthStatus.IncorrectCredentials)
            {
                throw new Exception("Incorrect credentials");
            }

            throw new Exception("Unknown error");
        }

        return new UserInfo(user.Login, reply.UserUuid, reply.JwtToken, reply.RefreshToken);
    }

    public async Task<bool> ContainsAsync(string login, UserInfo userInfo)
    {
        var channel = GrpcChannel.ForAddress(_authServiceConfiguration.Address);
        var client = new Auth.AuthClient(channel);
        var reply = await client.UserValidateJwtAsync(new UserAuthData { JwtToken = userInfo.Token });
        if (reply.Status is AuthStatus.JwtExpired)
        {
            reply = await client.UserRefreshJwtAsync(new UserAuthData
                { UserUuid = userInfo.Uuid, RefreshToken = userInfo.RefreshToken });

            if (reply.Status is not AuthStatus.SignInComplete)
            {
                throw new Exception("Token authorization error, relogin");
            }
        }

        return reply.IsJwtValid;
    }
}