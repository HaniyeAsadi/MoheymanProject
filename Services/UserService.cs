namespace MoheymanProject.Services;
public class UserService
{
    private const string available = nameof(available);
    private const string NOT_AVAILABLE = "not available";

    private AppDbContext _db;
    private User LoggedInUser { get; set; }
    private AppDbContext DB
    {
        get
        {
            if (_db is null)
                _db = new AppDbContext();
            return _db;
        }
    }
    public void Register(string username, string password)
    {
        if (DB.Users.Any(u => u.Username == username))
        {
            Console.WriteLine("Register failed! Username already exists.");
            return;
        }

        DB.Users.Add(new User { Username = username, Password = password, Status = true });
        DB.SaveChanges();
        Console.WriteLine("Registered successfully!");
    }
    public void Login(string username, string password)
    {
        var user = DB.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        if (user is null)
        {
            Console.WriteLine("Login faild! Invalid information.");
            return;
        }

        LoggedInUser = user;
        Console.WriteLine($"Logged in successfully! Welcome, {username}.");
    }
    public void Change(string status)
    {
        if (LoggedInUser == null)
        {
            Console.WriteLine("Access denied! Please log in first.");
            return;
        }

        LoggedInUser.Status = status == available;
        DB.SaveChanges();

        Console.WriteLine($"User {LoggedInUser.Username} status updated to {status}.");

    }
    public void Logout()
    {
        if (LoggedInUser is null)
        {
            Console.WriteLine("Access denied! Please log in first.");
            return;
        }

        LoggedInUser = null;
        Console.WriteLine("Logged out successfully!");
    }
    public void ChangePassword(string oldPassword, string newPassword)
    {
        if (LoggedInUser is null)
        {
            Console.WriteLine("Access denied! Please log in first.");
            return;
        }

        if (LoggedInUser.Password != oldPassword)
        {
            Console.WriteLine("Password change failed! Incorrect old password.");
            return;
        }

        if (string.IsNullOrEmpty(newPassword))
        {
            Console.WriteLine("New password cannot be empty.");
            return;
        }

        LoggedInUser.Password = newPassword;
        DB.SaveChanges();
        Console.WriteLine("Password changed successfully!");
    }
    public void Search(string username)
    {
        var users = DB.Users.Where(u => u.Username.Contains(username)).ToList();

        if (users.Count == 0)
        {
            Console.WriteLine("No users match your search. Please try a different username.");
            return;
        }

        int index = 0;
        foreach (var user in users)
        {
            Console.WriteLine($"{index++}- {user.Username} | status: {(user.Status ? available : NOT_AVAILABLE)}");
        }
    }
}