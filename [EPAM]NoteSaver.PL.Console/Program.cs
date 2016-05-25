
namespace _EPAM_User.PL.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using _EPAM_BLL;
    using _EPAM_Entites;
    using _EPAM_Intefases.BLL;

    class Program
    {
        static void PrintConsole()
        {
            IUserBLL logic = new UserLogic();
            int temp = 0;
            foreach (var item in logic.GetAll())
            {
                System.Console.WriteLine("{0} - {1} {2} {3}", temp++, item.Name, item.DateOfBith.ToString("MM/dd/yyyy"), item.Age);
            }
        }

        static void PrintUserConsole(UserDTO user)
        {
                System.Console.Write(" {0} - {1} {2}",  user.Name, user.DateOfBith.ToString("MM/dd/yyyy"), user.Age);
            
        }

        static void PrintAwardConsole(AwardDTO award)
        {
            System.Console.Write(" {0}", award.Title);

        }

        static UserDTO EnterConsoleUser()
        {
            System.Console.Write("Enter Name");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter DateOfBirth в формате: 21.11.1992  ");

            DateTime DateOfBirth = new DateTime();

            string line = System.Console.ReadLine();
            if (line != null)
            {
                char[] separators = { ' ', '.', ':' };
                string[] k = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                int[] number = new int[k.Length];
                for (int i = 0; i < k.Length; i++)
                {
                    number[i] = Convert.ToInt32(k[i], 10);
                }

                DateOfBirth = new DateTime(number[2], number[1], number[0]);
            }
             UserDTO user = new UserDTO(name, DateOfBirth);
            return user;

        }

        static void Main(string[] args)
        {
            IUserBLL logic = null;
            IUserAwardBLL logic_user_award = null;
            IAwardBLL logic_award = null;
            try
            {
                logic = new UserLogic();
                logic_award = new AwardLogic();
                logic_user_award = new _EPAM_BLL.UserAwardLogic();
            }

            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            try
            {
                int count = 0;
                do
                {
                    System.Console.WriteLine("Enter action:");
                    System.Console.WriteLine("1 - View all notes");
                    System.Console.WriteLine("2 - Add");
                    System.Console.WriteLine("3 - Delete");
                    System.Console.WriteLine("4 - Update");
                    System.Console.WriteLine("5 - Middle age");
                    System.Console.WriteLine("6 - Add award");
                    System.Console.WriteLine("7 - Print AwardsByUser");
                    System.Console.WriteLine("8 - Print UsersByAward");
                    System.Console.WriteLine("9 - Exit");
                    System.Console.Write("Enter number:");
                    int.TryParse(System.Console.ReadLine(), out count);
                    switch (count)
                    {
                        case 1:
                            {
                                PrintConsole();
                            }
                            break;

                        case 2:
                            {
                                UserDTO user = EnterConsoleUser();

                                logic.Create(user);
                            }

                            break;

                        case 3:
                            {
                                PrintConsole();
                                int temp = 0;
                                int.TryParse(System.Console.ReadLine(), out temp);
                                int c = 0;
                                foreach (var item in logic.GetAll())
                                {
                                    if (c == temp)
                                    {
                                        logic.Delete(item.Id);
                                        break;
                                    }
                                    c++;
                                }
                            }
                            break;

                        case 4:
                            {
                                int temp = 0;
                                PrintConsole();
                                int.TryParse(System.Console.ReadLine(), out temp);
                                int c = 0;
                                foreach (var item in logic.GetAll())
                                {
                                    if (c == temp)
                                    {
                                        UserDTO update_user = item;

                                        System.Console.Write("Enter Name");
                                        string name = System.Console.ReadLine();
                                        System.Console.Write("Enter DateOfBirth в формате: 21.11.1992  ");

                                        DateTime DateOfBirth = new DateTime();

                                        string line = System.Console.ReadLine();
                                        if (line != null)
                                        {
                                            char[] separators = { ' ', '.', ':' };
                                            string[] k = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                                            int[] number = new int[k.Length];
                                            for (int i = 0; i < k.Length; i++)
                                            {
                                                number[i] = Convert.ToInt32(k[i], 10);
                                            }
                                            DateOfBirth = new DateTime(number[2], number[1], number[0]);
                                        }

                                        update_user.Name = name;
                                        update_user.DateOfBith = DateOfBirth;
                                        logic.Update(update_user);

                                        break;
                                    }
                                    c++;
                                }
                            }
                            break;

                        case 5:
                            {
                                System.Console.WriteLine(logic.MiddleAge());
                            }
                            break;

                        case 6:
                            {
                                System.Console.WriteLine("Введите название награды");
                                string name = System.Console.ReadLine();
                                logic_award.Create(new AwardDTO(name));
                            }
                            break;

                        case 7:
                            {
                                foreach (var item in logic_user_award.AwardsByUser())
                                {
                                    PrintUserConsole(item.Key);
                                    foreach (var item2 in item.Value)
                                    {
                                        PrintAwardConsole(item2);
                                    }
                                    System.Console.WriteLine();
                                }
                            }
                            break;

                        case 8:
                            {
                                foreach (var item in logic_user_award.UsersByAward())
                                {
                                    PrintAwardConsole(item.Key);
                                    foreach (var item2 in item.Value)
                                    {
                                        PrintUserConsole(item2);
                                    }
                                    System.Console.WriteLine();
                                }
                            }
                            break;
                    }

                    System.Console.ReadLine();
                    System.Console.Clear();
                } while (count != 9);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
