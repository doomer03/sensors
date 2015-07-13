using System.Collections.Generic;
using Windows.Devices.Spi;

namespace RPI2.Sensors.ADConverter
{
    public interface IADConverter
    {
        void Init(List<int> adc_channel, SPIPort spi, int clockfrequency, SpiMode mode);
        int AnalogToDigital(int adc_channel);

    }
}
