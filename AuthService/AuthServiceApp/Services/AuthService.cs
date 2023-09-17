using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Grpc.Core;
using AuthServiceApp.Models;
using Google.Protobuf;
using Microsoft.IdentityModel.Tokens;

namespace AuthServiceApp.Services;

public class AuthService : Auth.AuthBase
{
    private readonly ILogger<AuthService> _logger;

    public AuthService(ILogger<AuthService> logger)
    {
        _logger = logger;
    }

    public override Task<UserAuthData> UserRegister(UserCredentials request, ServerCallContext _)
    {
        return Task.FromResult(Register(request));
    }

    public override Task<UserAuthData> UserAuthenticate(UserCredentials request, ServerCallContext _)
    {
        return Task.FromResult(Authenticate(request));
    }

    public override Task<UserAuthData> UserRefreshJwt(UserAuthData request, ServerCallContext _)
    {
        return Task.FromResult(RefreshJwt(request));
    }

    public override Task<UserAuthData> UserValidateJwt(UserAuthData request, ServerCallContext _)
    {
        try
        {
            return Task.FromResult(new UserAuthData(request) { IsJwtValid = ValidateToken(request.JwtToken) });
        }
        catch
        {
            return Task.FromResult(new UserAuthData(request) { Status = AuthStatus.JwtExpired, IsJwtValid = false });
        }
    }

    private static string GetHashedString(string str)
    {
        var data = Encoding.Default.GetBytes(str);
        return Convert.ToBase64String(SHA1.HashData(data));
    }

    private static bool CompareHashedString(string hashedStr, string str)
    {
        return hashedStr.Equals(GetHashedString(str));
    }


    private static UserAuthData Register(UserCredentials request)
    {
        var responseData = new UserAuthData() { Status = AuthStatus.UnknownError };
        try
        {
            using var db = new ApplicationContext();
            var user = db.Users.FirstOrDefault(u => u.Login.Equals(request.Login));

            if (user == null)
            {
                var added = db.Users.Add(new User()
                    { Login = request.Login, Password = GetHashedString(request.Password) });
                db.SaveChanges();
                responseData.Status = AuthStatus.SignUpComplete;
                responseData.UserUuid = ByteString.CopyFrom(added.Entity.Id.ToByteArray());
            }
            else
            {
                responseData.Status = AuthStatus.UserAlreadyExists;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return responseData;
    }

    private static UserAuthData Authenticate(UserCredentials request)
    {
        var responseData = new UserAuthData() { Status = AuthStatus.UnknownError };
        try
        {
            using var db = new ApplicationContext();
            var user = db.Users.FirstOrDefault(u => u.Login.Equals(request.Login));

            if (user != null)
            {
                if (CompareHashedString(user.Password, request.Password))
                {
                    responseData.Status = AuthStatus.SignInComplete;
                    responseData.UserUuid = ByteString.CopyFrom(user.Id.ToByteArray());
                    responseData.JwtToken = GenerateJwt();
                    user.RefreshToken = responseData.RefreshToken = GenerateRefreshToken();
                    user.RefreshTokenExpired = DateTime.Now.AddMonths(1);
                    db.Users.Update(user);
                    db.SaveChanges();
                }
                else
                {
                    responseData.Status = AuthStatus.IncorrectCredentials;
                }
            }
            else
            {
                responseData.Status = AuthStatus.IncorrectCredentials;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return responseData;
    }

    private static UserAuthData RefreshJwt(UserAuthData request)
    {
        var isJwtValid = ValidateToken(request.JwtToken);
        var responseData = new UserAuthData() { Status = AuthStatus.UnknownError };
        try
        {
            using var db = new ApplicationContext();
            var user = db.Users.Find(request.UserUuid);

            if (user != null)
            {
                if (user.RefreshTokenExpired == null)
                {
                }
                else if (user.RefreshTokenExpired.Value.Ticks > DateTime.Now.Ticks)
                {
                    responseData.Status = AuthStatus.SignInComplete;
                    responseData.JwtToken = GenerateJwt();
                    user.RefreshToken = responseData.RefreshToken = GenerateRefreshToken();
                    user.RefreshTokenExpired = DateTime.Now.AddMonths(1);
                }
                else
                {
                    responseData.Status = AuthStatus.RtExpired;
                    responseData.JwtToken = null;
                    user.RefreshToken = responseData.RefreshToken = null;
                    user.RefreshTokenExpired = null;
                }

                db.Users.Update(user);
                db.SaveChanges();
            }
            else
            {
                responseData.Status = AuthStatus.IncorrectCredentials;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return responseData;
    }

    private static string GenerateJwt()
    {
        var certificate = new X509Certificate2("Services/snakeoil.pfx");
        var securityKey = new X509SecurityKey(certificate);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(),
            Issuer = "Self",
            IssuedAt = DateTime.Now,
            Audience = "Others",
            Expires = DateTime.Now.AddMinutes(10),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static bool ValidateToken(string token)
    {
        var certificate = new X509Certificate2("Services/snakeoil.pfx");
        var securityKey = new X509SecurityKey(certificate);
        var validationParameters = new TokenValidationParameters
        {
            ValidAudience = "Others",
            ValidIssuer = "Self",
            IssuerSigningKey = securityKey,
            ValidateLifetime = true
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
        return claimsPrincipal != null && securityToken != null;
    }

    public static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(randomBytes)
            .Replace('+', '-')
            .Replace('/', '_')
            .Trim('=');
    }
}