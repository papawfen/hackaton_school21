namespace WebMvc.Application.Services;

public record UserInfo(byte[] JwtToken, byte[] RefreshToken);