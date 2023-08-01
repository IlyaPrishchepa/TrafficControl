using System.Globalization;
using TrafficControl.Models;

namespace TrafficControl.Services
{
	public class FileIDataService
	{

        private readonly String path = "C:\\Users\\pryshchepaI\\source\\repos\\TrafficControl\\TrafficControl\\data.txt";

        private ParserService parserService = new ParserService();

        public async Task<String> WritingData(SpeedingInf speedingInf)
        {
            var file = File.Exists(this.path) ? File.AppendText(path) : File.CreateText(path);
            await file.WriteLineAsync(parserService.ParseDataToString(speedingInf));
            await file.FlushAsync();
            file.Close();
            return "Recorded successfully.";
        }

        public async Task<List<SpeedingInf>> ReedingData()
        {
            List<SpeedingInf> speedingInfs = new List<SpeedingInf>();
            SpeedingInf speedingInf = new SpeedingInf();

            var lines = (await File.ReadAllLinesAsync(path)).ToList();

            foreach (string line in lines)
            {
                speedingInf = parserService.ParseStringToData(line);
                speedingInfs.Add(speedingInf);

            }

            return speedingInfs;
        }

    }
}
