using System;


namespace TelegramBot
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public CloudsInfo Clouds { get; set; }
        public int Visibility { get; set; }
        public CoordInfo Coord { get; set; }
        public int Timezone { get; set; }


    }
}
