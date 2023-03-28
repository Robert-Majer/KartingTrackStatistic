using System.Text;

namespace KartingTrackStatistic
{
    public class DriverSaved : DriverBase
    {
        private const string fileName = "_lapTimes.txt";

        private string firstName;
        private string lastName;
        private string fullFileName;

        public override string FirstName
        {
            get
            {
                return $"{char.ToUpper(firstName[0])}{firstName.Substring(1, firstName.Length - 1).ToLower()}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    firstName = value;
                }
            }
        }

        public override string LastName
        {
            get
            {
                return $"{char.ToUpper(lastName[0])}{lastName.Substring(1, lastName.Length - 1).ToLower()}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    lastName = value;
                }
            }
        }

        public DriverSaved(string firstName, string lastName) : base(firstName, lastName)
        {
            fullFileName = $"DriversStatistics\\{firstName}_{lastName}{fileName}";
        }

        public override void AddLapTime(double lapTime)
        {
            if (lapTime > 0)
            {
                using (var writer = File.AppendText($"{fullFileName}"))
                using (var writer2 = File.AppendText($"audit.txt"))

                {
                    writer.WriteLine(lapTime);
                    writer2.WriteLine($"{FirstName} {LastName} - {lapTime}        {DateTime.UtcNow}");
                    if (lapTime < GetBestLapTime())
                    {
                        if (File.Exists($"DriversStatistics\\BestLapTime.txt"))
                        {
                            File.Delete($"DriversStatistics\\BestLapTime.txt");
                        }
                        using (var writerNewBestLapTime = File.AppendText($"DriversStatistics\\BestLapTime.txt"))
                        {
                            writerNewBestLapTime.WriteLine($"{lapTime}");
                        }
                        CheckEventNewBestLapTime();
                    }
                }
            }
            else
            {
                throw new ArgumentException($"Invalid argument: {nameof(lapTime)}. Lap time must be greater than zero!");
            }
        }

        public override void ShowLapTimes()
        {
            StringBuilder sb = new StringBuilder($"{this.FirstName} {this.LastName} lap times are: ");

            using (var reader = File.OpenText(($"{fullFileName}")))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    sb.Append($"{line} seconds; ");
                    line = reader.ReadLine();
                }
            }
            Console.WriteLine($"\n{sb}");
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            if (File.Exists($"{fullFileName}"))
            {
                using (var reader = File.OpenText($"{fullFileName}"))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var number = double.Parse(line);
                        result.Add(number);
                        line = reader.ReadLine();
                    }
                }
            }
            return result;
        }
    }
}