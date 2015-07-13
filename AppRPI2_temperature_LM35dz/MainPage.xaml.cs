using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Spi;
using RPI2.Sensors;
using RPI2.Sensors.ADConverter;
using RPI2.Sensors.Temperature;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppRPI2_temperature_LM35dz
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        ///*RaspBerry Pi2  Parameters*/
        private const Int32 SPI_CHIP_SELECT_LINE = 0;       /* Line 0 maps to physical pin number 24 on the Rpi2        */

        // create a timer
        private DispatcherTimer timer;
        private MCP3008 mcp = null;
        private LM35dz LM35 = null;
        float res;

        public void DisplayTextBoxContents()
        {
            res = LM35.ReadmV(SPI_CHIP_SELECT_LINE);
            textPlaceHolder.Text = res.ToString() + " mV\r\n";
            textPlaceHolder.Text += "T (celcius)= " + (LM35.ReadCelcius(SPI_CHIP_SELECT_LINE)).ToString() + " °C\r\n";
            textPlaceHolder.Text += "T (Farenheit)= " + (LM35.ReadFarenheit(SPI_CHIP_SELECT_LINE)).ToString() + " °F\r\n";
            textPlaceHolder.Text += "T (Kelvin)= " + (LM35.ReadKelvin(SPI_CHIP_SELECT_LINE)).ToString() + " °K\r\n";
            textPlaceHolder.Text += "T (Rankine)= " + (LM35.ReadRankine(SPI_CHIP_SELECT_LINE)).ToString() + " °Ra\r\n";
            textPlaceHolder.Text += "T (Réaumur)= " + (LM35.ReadReaumur(SPI_CHIP_SELECT_LINE)).ToString() + " °Ré\r\n";
            textPlaceHolder.Text += "T (Newton)= " + (LM35.ReadNewton(SPI_CHIP_SELECT_LINE)).ToString() + " °N\r\n";
            textPlaceHolder.Text += "T (Romer)= " + (LM35.ReadRomer(SPI_CHIP_SELECT_LINE)).ToString() + " °Ro\r\n";
            textPlaceHolder.Text += "T (Delisle)= " + (LM35.ReadDelisle(SPI_CHIP_SELECT_LINE)).ToString() + " °De\r\n";
            textPlaceHolder.Text += "T (Leyden)= " + (LM35.ReadLeydenApproximative(SPI_CHIP_SELECT_LINE)).ToString() + " °L";

        }
        public MainPage()
        {
            this.InitializeComponent();
            
            mcp = new MCP3008(); //new List<int>() { SPI_CHIP_SELECT_LINE });
            mcp.Init(new List<int>() { SPI_CHIP_SELECT_LINE },SPIPort.SPI0, 500000, SpiMode.Mode0);
            LM35 = new LM35dz(mcp);

            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(500);
            this.timer.Tick += Timer_Tick;
            this.timer.Start();

            // ...
        }
        private void Timer_Tick(object sender, object e)
        {
            DisplayTextBoxContents();
        }
    }
}
