using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebMvc.Application.Models;

namespace WebMvc.Application.Services;

public class DummyAuthService : IAuthService
{
    private readonly Dictionary<User, UserInfo> _dataBase = new();
    private readonly object _lock = new();

    public async Task Register(User user)
    {
        var task = Task.Run(() =>
            {
                lock (_lock)
                {
                    if (_dataBase.ContainsKey(user))
                    {
                        throw new Exception("User already exists");
                    }

                    var keys = new UserInfo(Guid.NewGuid().ToByteArray(), Guid.NewGuid().ToByteArray());
                    _dataBase[user] = keys;
                }
            }
        );
        await task;
    }

    public async Task<UserInfo> Login(User user)
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

    protected Dictionary<User, UserInfo> GetDataBase()
    {
        lock (_lock)
        {
            return new Dictionary<User, UserInfo>(_dataBase);
        }
    }
}