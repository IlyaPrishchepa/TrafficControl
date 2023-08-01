using System.Globalization;
using TrafficControl.Models;

namespace TrafficControl.Services
{
	public class ParserService
	{

        public String ParseDataToString(SpeedingInf speedingInf)
        {
            return speedingInf.date.ToString("dd.MM.yyyy HH:mm:ss") + " " + speedingInf.vehicleNum + " " + speedingInf.speed.ToString();
        }

        public SpeedingInf ParseStringToData(string text)
        {

            var speedingInf = new SpeedingInf();
            var data = text.Split(" ", 5).ToList();

            speedingInf.speed = double.Parse(data[4]);
            speedingInf.vehicleNum = data[2] + " " + data[3];
            speedingInf.date = DateTime.ParseExact(data[0] + " " + data[1], "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);


            return speedingInf;
        }
    }
}
