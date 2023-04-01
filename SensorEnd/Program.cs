using System.Device.Gpio;
using Iot.Device.DHTxx;
using UnitsNet;

namespace Welcome;
class Program
{
   
    static void Main(string[] args)
    {
        Dht11 dht11 = new Dht11(23);

 int ledPin = 14;

var controller = new GpioController();
  
        controller.OpenPin(ledPin,PinMode.Output);


        while (true)
        {

            double lastTemperature = 0.0;
            double lastHumidity = 0.0;

            if (dht11.TryReadHumidity(out RelativeHumidity humidity))
            {
                lastHumidity = humidity.Percent;

            }

            if (dht11.TryReadTemperature(out Temperature temperature))
            {
                lastTemperature = temperature.DegreesCelsius;
            }

            if (dht11.IsLastReadSuccessful)
            {
                Console.WriteLine($"Humidity: {lastHumidity}%");
                Console.WriteLine($"Temperature: {lastTemperature}C");
            }
 
            if (lastTemperature > 20)    
             controller.Write(ledPin,PinValue.High);
             else
             controller.Write(ledPin,PinValue.Low);

 
            Thread.Sleep(5000);
        }
    }
}