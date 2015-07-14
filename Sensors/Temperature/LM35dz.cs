using RPI2.Sensors.ADConverter;

namespace RPI2.Sensors.Temperature
{
    public class LM35dz
    {
        /// <summary>
        /// the celcius degree is subdivision of mV value. 250mV = 25°C
        /// </summary>
        private const int Factor = 10;
        /// <summary>
        /// LM35dz precision sensor, fix this value in Celcius degree to adjust the correct temperature.
        /// </summary>
        public int Precision { get; set; }
        private IADConverter _converter;
     
        public LM35dz(IADConverter value)
        {
            _converter = value;
        }
        
        public float ReadmV(int channel)
        {
            return _converter.AnalogToDigital(channel) + (Precision * Factor);
        }

        public float ReadCelcius(int channel)
        {
            return ReadmV(channel) / Factor;
        }

        public float ReadFarenheit(int channel)
        {
            return (ReadCelcius(channel) * 1.8f) + 32;
        }

        public float ReadRankine(int channel)
        {
            return ReadFarenheit(channel) + 459.67f;
        }

        public float ReadKelvin(int channel)
        {
            return ReadRankine(channel) * (5f/9f);
        }

        public float ReadReaumur(int channel)
        {
            return (ReadFarenheit(channel) - 32) * (4f / 9f);
        }

        public float ReadNewton(int channel)
        {
            return (ReadFarenheit(channel) - 32) * (11f/60f);
        }

        public float ReadRomer(int channel)
        {
            return ((ReadFarenheit(channel) - 32) * (7f / 24f)) + 7.5f;
        }

        public float ReadDelisle(int channel)
        {
            return (212 - ReadFarenheit(channel)) * (5f/6f);
        }

        public float ReadLeydenApproximative(int channel)
        {
            return ReadKelvin(channel) - 20.15f;
        }
    }
}
