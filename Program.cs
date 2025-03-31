﻿public class Program
{
    public static void Main()
    {
        Console.WriteLine("Hello. Welcome to the Console Application!");
        Console.WriteLine("Enter your commands here:");

        string line;
        using (var db = new AppDbContext())
        {
            var userService = new UserService(db);

            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                string[] args = line.Split(' ');
                ProcessCommand(args, userService);
            }
        }
    }
    public static void ProcessCommand(string[] args, UserService userService)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Invalid command.");
            return;
        }

        switch (args[0].ToLower())
        {
            case "register":
                if (args.Length == 5)
                    userService.Register(args[2], args[4]);
                else
                    Console.WriteLine("Invalid register command.");

                break;

            case "login":
                if (args.Length == 5)
                    userService.Login(args[2], args[4]);
                else
                    Console.WriteLine("Invalid login command.");

                break;

            case "change":
                if (args.Length == 3)
                    userService.ChangeStatus(args[2]);
                else if (args.Length == 4)
                    userService.ChangeStatus(args[2] + ' ' + args[3]);
                else
                    Console.WriteLine("Invalid change status command.");

                break;

            case "logout":
                if (args.Length == 1)
                    userService.Logout();

                break;

            case "changepassword":
                if(args.Length == 5)
                    userService.ChangePassword(args[2], args[4]);
                else
                    Console.WriteLine("Invalid change password command.");

                break;

            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }
}
