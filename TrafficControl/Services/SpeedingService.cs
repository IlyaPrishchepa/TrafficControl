using System.Globalization;
using TrafficControl.Models;

namespace TrafficControl.Services
{
    public class SpeedingService
    {

        private FileIDataService fileIDataService = new FileIDataService();
        private ParserService parserService = new ParserService();

        public async Task<IEnumerable<SpeedingInf>> GetAll() => await this.fileIDataService.ReedingData();

        public async Task<String> SpeedingOnDateAndSpeed(DateTime date, double speed)
        {

            List<SpeedingInf> speedingInfs = new List<SpeedingInf>();
            speedingInfs = await this.fileIDataService.ReedingData();

            String result = "";

            foreach (SpeedingInf speedingInf in speedingInfs)
            {
                if (speedingInf.date.Date == date.Date && speedingInf.speed > speed)
                {
                    result += parserService.ParseDataToString(speedingInf)+"\n";
                }
            }

            return result;
        }

        public async Task<String> MaxSpeedOnDate(DateTime date)
        {

            List<SpeedingInf> speedingInfs = new List<SpeedingInf>();
            speedingInfs = await this.fileIDataService.ReedingData();

            String result = "";
            double maxSpeed = 0;

            foreach (SpeedingInf speedingInf in speedingInfs)
            {
                if (speedingInf.date.Date == date.Date && speedingInf.speed > maxSpeed)
                {
                    result = parserService.ParseDataToString(speedingInf);
                    maxSpeed = speedingInf.speed;
                }
            }

            return result;
        }

        public async Task<String> MinSpeedOnDate(DateTime date)
        {

            List<SpeedingInf> speedingInfs = new List<SpeedingInf>();
            speedingInfs = await this.fileIDataService.ReedingData();

            String result = "";
            double minSpeed = double.MaxValue;

            foreach (SpeedingInf speedingInf in speedingInfs)
            {
                if (speedingInf.date.Date == date.Date && speedingInf.speed < minSpeed)
                {
                    result = parserService.ParseDataToString(speedingInf);
                    minSpeed = speedingInf.speed;
                }
            }

            return result;
        }

        public async Task<String> MinAadMaxSpeedOnDate(DateTime date) {
            string min = await this.MinSpeedOnDate(date);
            string max = await this.MaxSpeedOnDate(date);
            return min + "\n" + max;
        } 
    }
}
