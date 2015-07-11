using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Spi;

namespace RPI2.Sensors.ADConverter
{
    public class SPIADConverterBase : IDisposable
    {
        #region properties
        protected SpiDevice SpiDisplay;
        protected List<int> adcChannel = new List<int>();
        protected byte[] readBuffer = new byte[3];
        protected byte[] writeBuffer = new byte[3];
        #endregion

        public void Dispose()
        {
            if (SpiDisplay != null)
            {
                SpiDisplay.Dispose();
            }
        }
        public async void initSPI(SPIPort spi)
        {
            try
            {
                var settings = new SpiConnectionSettings((int)spi);
                settings.ClockFrequency = 500000;
                settings.Mode = SpiMode.Mode0;

                string spiAqs = SpiDevice.GetDeviceSelector(Enum.GetName(typeof(SPIPort), spi));
                var deviceInfo = await DeviceInformation.FindAllAsync(spiAqs);
                SpiDisplay = await SpiDevice.FromIdAsync(deviceInfo[0].Id, settings);
            }

            /* If initialization fails, display the exception and stop running */
            catch (Exception ex)
            {
                throw new Exception("SPI Initialization Failed", ex);
            }
        }
        /// <summary>
        /// Analog to digital conversion
        /// </summary>
        public int AnalogToDigital(int adc_channel)
        {
            return readadc(adc_channel);
        }

        virtual protected int readadc(int adc_channel)
        { return 0; }
        
    }
}
