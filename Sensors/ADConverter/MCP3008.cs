using System;

// Original author: Eric Noyau - git://gist.github.com/3249416.git
// Hackster article: https://microsoft.hackster.io/en-US/4796/temperature-sensor-sample
// Ported from Hackster.io temperature sensor

namespace RPI2.Sensors.ADConverter
{
    /// <summary>
    /// Raspberry Pi using MCP3008 A/D Converters with SPI Serial Interface
    /// <seealso cref="http://ww1.microchip.com/downloads/en/DeviceDoc/21295d.pdf"/>
    /// </summary>
    public class MCP3008: SPIADConverterBase
    {
        /// <summary>
        /// Connect MCP3008 with clock, Serial Peripheral Interface(SPI) and channel
        /// </summary>
        public MCP3008()
        {
            maxADCChannel = 7;
        }


        protected override int readadc(int adc_channel)
        {
            if ((adc_channel > maxADCChannel) || adc_channel < 0)
            {
                return -1;
            }
            writeBuffer[1] = Convert.ToByte(adc_channel);
            SpiDisplay.TransferFullDuplex(writeBuffer, readBuffer);
            int result = readBuffer[1] & 0x0F;
            result <<= 8;
            result += readBuffer[2];
            return result;
        }
    }
}
