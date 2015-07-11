using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;

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
        /// <param name="adc_channel">MCP3008 channel number 0-7 (pin 1-8 on chip).</param>
        /// <param name="spi">port SPI used</param>
        public MCP3008(List<int> adc_channel)
        {
            adcChannel = adc_channel;
            writeBuffer = new byte[3] { 0x06, 0x00, 0x00 };
            //
            if (adcChannel.Count <= 7 && adcChannel.Exists(x => x > 7 || x < 0))
            {
                //This is the range we are looking for, from CH0 to CH7. 
            }
            else
            {
                throw new IndexOutOfRangeException("MCP3008 Channel Input is out of range, Channel input should be from 0-7 (8-Channel).");
            }
        }


        protected override int readadc(int adc_channel)
        {
            if ((adc_channel > 7) || adc_channel < 0)
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
