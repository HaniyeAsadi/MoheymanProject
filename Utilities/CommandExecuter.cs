namespace MoheymanProject.Utilities;
public class CommandExecuter
{
    private const string username = "--username";
    private const string password = "--password";
    private const string status = "--status";
    private const string old = "--old";
    private const string new_password = "--new";
    private const string available = "available";
    private const string NOT_AVAILABLE = "not available";
    private UserService _userService;
    private UserService UserService
    {
        get
        {
            if (_userService is null)
                _userService = new UserService();

            return _userService;
        }
    }
    public void Execute(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid command.");
            return;
        }
        
        ExecuteCommand(args);
    }

    private void ExecuteSearchCommand(string[] args)
    {
        if (args.Length == 3 && args[1] == username)
            UserService.Search(args[2]);
        else
            Console.WriteLine("Invalid search command.");
    }

    private void ExecuteLogoutCommand(string[] args)
    {
        if (args.Length == 1)
            UserService.Logout();
    }

    private void ExecuteChangePasswordCommand(string[] args)
    {
        if (args.Length == 5 && args[1] == old && args[3] == new_password)
            UserService.ChangePassword(args[2], args[4]);
        else
            Console.WriteLine("Invalid change password command.");
    }

    private void ExecuteChangeCommand(string[] args)
    {
        if (args.Length == 3 && args[2] == status)
            UserService.Change(args[2]);
        else if (args.Length == 4 && args[2] + ' ' + args[3] == NOT_AVAILABLE)
            UserService.Change(args[2] + ' ' + args[3]);
        else
            Console.WriteLine("Invalid change status command.");
    }

    private void ExecuteLoginCommand(string[] args)
    {
        if (args.Length == 5 && args[1] == username && args[3] == password)
            UserService.Login(args[2], args[4]);
        else
            Console.WriteLine("Invalid login command.");
    }

    private void ExecuteRegisterCommand(string[] args)
    {
        if (args.Length == 5 && args[1] == username && args[3] == password)
            UserService.Register(args[2], args[4]);
        else
            Console.WriteLine("Invalid register command.");
    }

    public void ExecuteCommand(string[] args)
    {
        var commandName = args[0];
        var methodName = "Execute" + commandName + "Command";
        MethodInfo method =
            typeof(CommandExecuter)
            .GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (method is null)
        {
            Console.WriteLine($"Invalid command: {commandName}");
            return;
        }

        method.Invoke(this, new object[] {args});
    }
}