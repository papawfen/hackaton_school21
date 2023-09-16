using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthServiceApp.Models;

[Table("users")]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public Guid Id { get; set; }
    [Column("login"), Required]
    public string Login { get; set; }
    [Column("password"), Required]
    public string Password { get; set; }
    [Column("refresh_token")]
    public string? RefreshToken { get; set; }
    [Column("rt_expired")]
    public DateTime? RefreshTokenExpired { get; set; }
}