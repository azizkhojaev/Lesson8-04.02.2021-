using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lesson8
{
    class Person
    {  
        public void con( string con)//this is the only method that works with DB
        {
            string conString = @"Data Source=FA; Initial Catalog=master;Integrated Security=True";
            SqlConnection connection = new SqlConnection(conString);// connecting to DB

            try
            {
                connection.Open();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = con ;// con is a string that sended from other methods to send request to DB
                    var reader = command.ExecuteReader();
                    Console.WriteLine("Great!");

                    while (reader.Read())// this is just for Showing info to console, while loop works when something returns from DB
                    {
                        Console.WriteLine("ID = " + reader["id"]);
                        Console.WriteLine("FirstName: " + reader["FirstName"]);
                        Console.WriteLine("LastName: " + reader["LastName"]);
                        Console.WriteLine("MiddleName: " + reader["MiddleName"]);
                        Console.WriteLine("BirthDate: " + reader["BirthDate"]);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Disconnected from Server!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }
        public void Insert(string firstname , string lastname, string middlename, string birthday)
        {
            var comtext = "Insert into PesonTable(" +
                "FirstName," +
                "LastName," +
                "MiddleName," +
                "Birthdate) Values(" +
                $"'{firstname}'," +
                $"'{lastname}'," +
                $"'{middlename}'," +
                $"'{birthday}')";
            Console.WriteLine("Inserted !!");
            con(comtext);
            
        }
        public void SellectAll()
        {
            var comtext = $"SELECT * FROM PesonTable";
            con(comtext);
        }
        public void SellectById(int id)
        {
            var comtext = $"SELECT * FROM PesonTable  where Id = '{id}'";
            con(comtext);
        }
        public void Update(int id, string choice, string newinfo)
        {
            var comtext = $"UPDATE PesonTable  SET {choice} = '{newinfo}' WHERE Id={id}";
            con(comtext);
        }
        public void Delete(int id)
        {
            var comtext = $"DELETE FROM PesonTable WHERE Id = '{id}'";
            Console.WriteLine("Deleted !!");
            con(comtext);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Table managemant ");
            Console.WriteLine("1. Insert ");
            Console.WriteLine("2. Select All ");
            Console.WriteLine("3. Select by id ");
            Console.WriteLine("4. Update ");
            Console.WriteLine("5. Delete ");
            Console.Write("Your Choice ");

            var x = Convert.ToInt32(Console.ReadLine());
            var person = new Person();
            if (x == 1)
            {
                Console.Write("Enter person's first name ");
                var name = Console.ReadLine();
                Console.Write("Enter person's last name ");
                var lastname = Console.ReadLine();
                Console.Write("Enter person's middle name ");
                var middlename = Console.ReadLine();
                Console.Write("Enter person's birthdate [yyyy-mm-dd] ");
                var birthday = Console.ReadLine();
                person.Insert(name, lastname, middlename, birthday);
            }
            else if (x == 2)
            {
                person.SellectAll();
            }
            else if (x == 3)
            {
                Console.Write("Enter id of a person to see its information ");
                var id = Convert.ToInt32(Console.ReadLine());
                person.SellectById(id);
            }
            else if (x == 4)
            {
                Console.Write("Enter an id of a person's to change it ");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Wich part of table you want to change plz write is correctly ");
                Console.WriteLine("FirstName ");
                Console.WriteLine("LastName ");
                Console.WriteLine("MiddleName ");
                Console.WriteLine("Birthdate ");
                Console.Write("Your choice ");
                var choice = Console.ReadLine();
                Console.Write("What is your new info ");
                var newinfo = Console.ReadLine();

                person.Update(id, choice, newinfo);
            }
            else if (x == 5)
            {
                Console.Write("Enter id of a person to delete it ");
                var id = Convert.ToInt32(Console.ReadLine());
                person.Delete(id);
            }
            else
                Console.WriteLine("Something is going wrong try again!");
        }
    }
}
