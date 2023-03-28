using static KartingTrackStatistic.DriverBase;

namespace KartingTrackStatistic
{
    public interface IDriver
    {
        string FirstName { get; set; }
        string LastName { get; set; }

        event NewBestLapTimeDelegade NewBestLapTime;

        void AddLapTime(double lapTime);

        void AddLapTime(string lapTime);

        void ShowLapTimes();

        Statistics GetStatistics();

        void ShowStatistics();

        double GetBestLapTime();
    }
}