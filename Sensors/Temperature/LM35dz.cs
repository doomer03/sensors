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
        
        public int Read(int channel)
        {
            return _converter.AnalogToDigital(channel);
        }
    }
}
