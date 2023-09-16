namespace WebMvc.Application.Services.Models;

public record UserInfo(byte[] JwtToken, byte[] RefreshToken);