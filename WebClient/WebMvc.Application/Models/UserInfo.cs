namespace WebMvc.Application.Models;

public record UserInfo(byte[] Token, byte[] RefreshToken);