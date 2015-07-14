using System;

namespace RPI2.Sensors.ADConverter
{
    /// <summary>
    /// Raspberry Pi using MCP3002 A/D Converters with SPI Serial Interface
    /// <seealso cref="http://ww1.microchip.com/downloads/en/DeviceDoc/21295d.pdf"/>
    /// </summary>
    public class MCP3002 : SPIADConverterBase
    {
        protected MCP3002()
        {
            maxADCChannel = 1;
            writeBuffer = new byte[3] { 0x68, 0x00, 0x00 };
        }

        protected override int readadc(int adc_channel)
        {
            if ((adc_channel > maxADCChannel) || adc_channel < 0)
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
