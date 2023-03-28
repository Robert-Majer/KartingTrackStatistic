namespace KartingTrackStatistic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WritelineColor(ConsoleColor.Magenta, "Hello to the [Student's Grades Book] console app.");

            bool CloseApp = false;

            while (!CloseApp)
            {
                Console.WriteLine();
                WritelineColor(ConsoleColor.Cyan,
                    "1 - Add driver's lap times to the .txt file and show statistics\n" +
                    "2 - Show the best lap time on the track\n" +
                    "Q - Close app\n");

                WritelineColor(ConsoleColor.Yellow, "What you want to do? \nPress key 1, 2 or Q: ");
                var userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case "1":
                        AddLapTimesToTxtFile();
                        break;

                    case "2":

                        var theBestLapTime = ShowBestLapTime();
                        WritelineColor(ConsoleColor.DarkYellow, $"The best lap time is: {theBestLapTime} seconds");
                        break;

                    case "Q":
                        CloseApp = true;
                        break;

                    default:
                        WritelineColor(ConsoleColor.Red, "Invalid operation.\n");
                        continue;
                }
            }
            WritelineColor(ConsoleColor.DarkYellow, "\n\nPress any key to leave.");
            Console.ReadKey();
        }

        private static void OnNewBestLapTime(object sender, EventArgs args)
        {
            WritelineColor(ConsoleColor.DarkYellow, $"Congratulation! New best lap time!");
        }

        private static void AddLapTimesToTxtFile()
        {
            string firstName = GetValueFromUser("Please insert driver's first name: ");
            string lastName = GetValueFromUser("Please insert driver's last name: ");
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                var savedDriver = new DriverSaved(firstName, lastName);
                savedDriver.NewBestLapTime += OnNewBestLapTime;
                EnterGrade(savedDriver);
                savedDriver.ShowStatistics();
            }
            else
            {
                WritelineColor(ConsoleColor.Red, "Driver's firstname and lastname can not be empty!");
            }
        }

        private static void EnterGrade(IDriver driver)
        {
            while (true)
            {
                WritelineColor(ConsoleColor.Yellow, $"Enter lap time for {driver.FirstName} {driver.LastName}:");
                var input = Console.ReadLine();

                if (input == "q" || input == "Q")
                {
                    break;
                }
                try
                {
                    driver.AddLapTime(input);
                }
                catch (FormatException ex)
                {
                    WritelineColor(ConsoleColor.Red, ex.Message);
                }
                catch (ArgumentException ex)
                {
                    WritelineColor(ConsoleColor.Red, ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    WritelineColor(ConsoleColor.Red, ex.Message);
                }
                finally
                {
                    WritelineColor(ConsoleColor.DarkMagenta, $"To leave and show {driver.FirstName} {driver.LastName} statistics enter 'q'.");
                }
            }
        }

        private static void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static string GetValueFromUser(string comment)
        {
            WritelineColor(ConsoleColor.Yellow, comment);
            string userInput = Console.ReadLine();
            return userInput;
        }

        private static double ShowBestLapTime()
        {
            const string fileName = "DriversStatistics\\BestLapTime.txt";

            var result = 0.0;
            if (File.Exists($"{fileName}"))
            {
                using (var reader = File.OpenText($"{fileName}"))
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var number = double.Parse(line);
                        result = number;
                    }
                }
            }
            return result;
        }
    }
}