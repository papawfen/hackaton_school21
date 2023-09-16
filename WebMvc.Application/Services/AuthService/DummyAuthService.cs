using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Application.Models;

namespace WebMvc.Application.Services.AuthService;

public class DummyAuthService : IAuthService
{
    private readonly Dictionary<User, UserInfo> _dataBase = new();
    private readonly object _lock = new();

    public async Task RegisterAsync(User user)
    {
        var task = Task.Run(() =>
            {
                lock (_lock)
                {
                    if (_dataBase.ContainsKey(user))
                    {
                        throw new Exception("User already exists");
                    }

                    if (user.Login.Length < 6)
                    {
                        throw new Exception("The login length must be greater than 6");
                    }

                    if (user.Password.Length < 6)
                    {
                        throw new Exception("The password length must be greater than 6");
                    }

                    var keys = new UserInfo(Guid.NewGuid().ToByteArray(), Guid.NewGuid().ToByteArray());
                    _dataBase[user] = keys;
                }
            }
        );
        await task;
    }

    public async Task<UserInfo> LoginAsync(User user)
    {
        var task = Task.Run(() =>
            {
                lock (_lock)
                {
                    if (!_dataBase.ContainsKey(user))
                    {
                        throw new Exception("User doesnt exists");
                    }

                    return _dataBase[user];
                }
            }
        );

        return await task;
    }

    public async Task<bool> ContainsAsync(string login, Guid token)
    {
        var task = Task.Run(() =>
        {
            lock (_lock)
            {
                var record = _dataBase.FirstOrDefault(pair => pair.Key.Login.Equals(login));
                if (record.Value is null || record.Key is null)
                {
                    return false;
                }

                return token.ToByteArray().SequenceEqual(record.Value.Token);
            }
        });

        return await task;
    }

    protected Dictionary<User, UserInfo> GetDataBase()
    {
        lock (_lock)
        {
            return new Dictionary<User, UserInfo>(_dataBase);
        }
    }
}