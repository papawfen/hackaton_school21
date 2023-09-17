using Google.Protobuf;

namespace WebMvc.Application.Models;

public record UserInfo(string Login, ByteString Uuid, string Token, string RefreshToken);