namespace KartingTrackStatistic
{
    public abstract class DriverBase : Person, IDriver
    {
        public delegate void NewBestLapTimeDelegade(object sender, EventArgs args);

        public event NewBestLapTimeDelegade NewBestLapTime;

        public override string FirstName { get; set; }
        public override string LastName { get; set; }

        public DriverBase(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public abstract void AddLapTime(double lapTime);

        public void AddLapTime(string lapTime)
        {
            if (lapTime.Contains(':'))
            {
                var lapTimeSplit = lapTime.Split(':');
                if (double.TryParse(lapTimeSplit[0], out double lapMinutes) && double.TryParse(lapTimeSplit[1], out double lapSeconds))
                {
                    var minutesToSeconds = lapMinutes * 60;
                    var lapTimeInSeconds = minutesToSeconds + lapSeconds;
                    AddLapTime(lapTimeInSeconds);
                }
            }
            else if (double.TryParse(lapTime, out double lapTimeInDouble))
            {
                AddLapTime(lapTimeInDouble);
            }
            else
            {
                throw new ArgumentException($"Invalid argument: {nameof(lapTime)}. Only lap times in seconds or [mm:ss] format are allowed!");
            }
        }

        public abstract void ShowLapTimes();

        public abstract Statistics GetStatistics();

        public void ShowStatistics()
        {
            var stat = GetStatistics();
            if (stat.Count != 0)
            {
                ShowLapTimes();
                Console.WriteLine($"{FirstName} {LastName} statistics:");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Total lap times: {stat.Count}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Highest lap time: {stat.High:N2} seconds");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Lowest lap time: {stat.Low:N2} seconds");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Average lap time: {stat.Average:N2} seconds");
                Console.WriteLine();
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Couldn't get statistics for {this.FirstName} {this.LastName} because no grade has been added.");
                Console.ResetColor();
            }
        }

        protected void CheckEventNewBestLapTime()
        {
            if (NewBestLapTime != null)
            {
                NewBestLapTime(this, new EventArgs());
            }
        }

        public double GetBestLapTime()
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