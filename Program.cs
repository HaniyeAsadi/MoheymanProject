using System;
using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
public class MoheymanProject
{
    public static string connectionString = "Server=.;Database=MoheymanProject;Integrated Security=True;Encrypt=false;";
    public static bool CheckIntegrity(string userName)
    {
        try{
            string query = "SELECT TOP 1 * FROM Users WHERE UserName = '" + userName + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) 
                {
                    return false;
                }

                connection.Close();
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return true;
    }
    public static bool InsertUser(string userName, string password){
        try{
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertUser", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@userName", userName);
                command.Parameters.AddWithValue("@password", password);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();;
                connection.Close();

                if(rowsAffected > 0){
                    Console.WriteLine("Registered successfully!");
                }
                return rowsAffected > 0;
            }     
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    public static void Register(string userName, string password){
        try{
            using (SqlConnection connection = new SqlConnection(connectionString)){
                if(!CheckIntegrity(userName)){
                    Console.WriteLine("output: register failed! Username already exists.");
                    return;
                }

                if(!InsertUser(userName, password)){
                    Console.WriteLine("output: register failed! An error occured while registering. Try again later.");
                    return;
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    public static void Main()
    {
        Console.WriteLine("Hello. Welcome to the Console Application!");
        Console.WriteLine("Enter your commands here:");

        string line;
        while (!string.IsNullOrEmpty(line = Console.ReadLine())){
            string[] command = line.Split(' ');
            switch (command[0])
            {
                case "Register":
                    Register(command[2], command[4]);
                    break;

                case "Login":
                    Console.WriteLine("Login1");
                    break;

                default:
                    Console.WriteLine("hi");
                    break;
            }
        }
    }
}
