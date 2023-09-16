namespace WebMvc.Application.Models;

public record UserInfo(byte[] JwtToken, byte[] RefreshToken);