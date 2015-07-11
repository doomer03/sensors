using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPI2.Sensors.ADConverter
{
    /// <summary>
    /// Raspberry Pi using MCP3002 A/D Converters with SPI Serial Interface
    /// <seealso cref="http://ww1.microchip.com/downloads/en/DeviceDoc/21295d.pdf"/>
    /// </summary>
    public class MCP3002 : SPIADConverterBase
    {
        public MCP3002(int adc_channel)
        {
            writeBuffer = new byte[3] { 0x68, 0x00, 0x00 };
        }

        protected override int readadc(int adc_channel)
        {
            if ((adc_channel > 7) || adc_channel < 0)
            {
                return -1;
            }
            writeBuffer[1] = Convert.ToByte(adc_channel);
            SpiDisplay.TransferFullDuplex(writeBuffer, readBuffer);
            int result = readBuffer[0] & 0x03;
            result <<= 8;
            result += readBuffer[1];
            return result;
        }

    }
}
