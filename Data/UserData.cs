using System;
using SampleSecureWeb.Models;
using BC = BCrypt.Net.BCrypt; 

namespace SampleSecureWeb.Data;

public class UserData : IUser
{
    private readonly ApplicationDbContext _db;

    public UserData(ApplicationDbContext db)
    {
        _db = db;
    }
public User Login(User user)
{
    var _user = _db.Users.FirstOrDefault(u => u.Username == user.Username);
    if (_user == null)
    {
        Console.WriteLine("User not found"); // Tambahkan log
        throw new Exception("User not found");
    }
    if (!BCrypt.Net.BCrypt.Verify(user.Password, _user.Password))
    {
        Console.WriteLine("Password is incorrect"); // Tambahkan log
        throw new Exception("Password is incorrect");
    }
    return _user;
}

    public User Registration (User user)
    {
        try
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    
    }

}
