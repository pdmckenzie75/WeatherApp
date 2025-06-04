namespace Core
{
    public class DailyForecast
    {
        public DateOnly Date { get; set; }

        public double MinTemperatureC { get; set; }

        public double MaxTemperatureC { get; set; }
        
        public double Rainfall { get; set; }
        
        public double WindSpeedMph { get; set; }

        public double WindDirection { get; set; }
    }
}
