namespace MoheymanProject.Services;
public class UserService
{
    private readonly AppDbContext _db;
    private User _loggedInUser = null;
    public UserService(AppDbContext db)
    {
        _db = db;
    }
    public void Register(string username, string password)
    {
        if (_db.Users.Any(u => u.Username == username))
        {
            Console.WriteLine("Register failed! Username already exists.");
            return;
        }

        _db.Users.Add(new User { Username = username, Password = password, Status = true });
        _db.SaveChanges();
        Console.WriteLine("Registered successfully!");
    }
    public void Login(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user == null)
        {
            Console.WriteLine("Login faild! Invalid information.");
            return;
        }

        _loggedInUser = user;
        Console.WriteLine($"Logged in successfully! Welcome, {username}.");
    }
    public void ChangeStatus(string status)
    {
        if (_loggedInUser == null)
        {
            Console.WriteLine("Access denied! Please log in first.");
            return;
        }

        _loggedInUser.Status = status == "available";
        _db.SaveChanges();

        Console.WriteLine($"User {_loggedInUser.Username} status updated to {status}.");

    }
    public void Logout()
    {
        if (_loggedInUser == null)
        {
            Console.WriteLine("Access denied! Please log in first.");
            return;
        }

        _loggedInUser = null;
    }
    public void ChangePassword(string oldPassword, string newPassword)
    {
        if (_loggedInUser == null)
        {
            Console.WriteLine("Access denied! Please log in first.");
            return;
        }

        if (_loggedInUser.Password != oldPassword)
        {
            Console.WriteLine("Password change failed! Incorrect old password.");
            return;
        }

        _loggedInUser.Password = newPassword;
        _db.SaveChanges();
        Console.WriteLine("Password changed successfully!");
    }
    public void Search(string username)
    {
        var users = _db.Users.Where(u => u.Username.Contains(username)).ToList();
        
        int index = 0;
        foreach (var user in users)
        {
            Console.WriteLine($"{index++}- {user.Username} | status: {(user.Status ? "available" : "not available")}");
        }
    }
}