using RPI2.Sensors.ADConverter;

namespace RPI2.Sensors.Temperature
{
    public class LM35dz
    {
        private IADConverter _converter;
        public LM35dz(IADConverter value)
        {
            _converter = value;
        }
        
        public float ReadmV(int channel)
        {
            return _converter.AnalogToDigital(channel);
        }

        public float ReadCelcius(int channel)
        {
            return ReadmV(channel) / 10;
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
            return ReadRankine(channel) * (5/9);
        }

        public float ReadReaumur(int channel)
        {
            return (ReadFarenheit(channel) - 32) * (4 / 9);
        }

        public float ReadNewton(int channel)
        {
            return (ReadFarenheit(channel) - 32) * (11/60);
        }

        public float ReadRomer(int channel)
        {
            return ((ReadFarenheit(channel) - 32) * (7 / 24)) + 7.5f;
        }

        public float ReadDelisle(int channel)
        {
            return (212 - ReadFarenheit(channel)) * (5/6);
        }

        public float ReadLeydenApproximative(int channel)
        {
            return ReadKelvin(channel) - 20.15f;
        }
    }
}
