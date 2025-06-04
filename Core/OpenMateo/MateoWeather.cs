namespace Core.OpenMateo
{
    // Code autogenarated Paste-Special (Past as Json Classes) from the OpenMeteo API response

    public class MateoWeatherForecast
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public Daily_Units daily_units { get; set; }
        public Daily daily { get; set; }
    }

    public class Daily_Units
    {
        public string time { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
        public string rain_sum { get; set; }
        public string wind_speed_10m_max { get; set; }
        public string wind_direction_10m_dominant { get; set; }
        public string wind_gusts_10m_max { get; set; }
        public string showers_sum { get; set; }
    }

    public class Daily
    {
        public string[] time { get; set; }
        public float[] temperature_2m_max { get; set; }
        public float[] temperature_2m_min { get; set; }
        public float[] rain_sum { get; set; }
        public float[] wind_speed_10m_max { get; set; }
        public int[] wind_direction_10m_dominant { get; set; }
        public float[] wind_gusts_10m_max { get; set; }
        public float[] showers_sum { get; set; }
    }

}
