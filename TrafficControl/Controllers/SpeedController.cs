using Microsoft.AspNetCore.Mvc;
using TrafficControl.Models;
using TrafficControl.Controllers;
using TrafficControl.Services;
using System.Collections.Generic;

namespace TrafficControl.Controllers
{
	[ApiController]
	[Route("/api/speed-controller")]
	public class SpeedController : Controller
	{

        private readonly FileIDataService fileIDataService = new FileIDataService();
        private readonly SpeedingService speedingService = new SpeedingService();

        private readonly int startWork;
		private readonly int endWork;


		public SpeedController(IConfiguration configuration)
		{
			this.startWork = int.Parse(configuration["startWork"]);
			this.endWork = int.Parse(configuration["endWork"]);

		}

		private void ValidateWork()
		{
			var currentHour = DateTime.Now.Hour;

			if (currentHour <= this.startWork || currentHour >= this.endWork) 
			{
				throw new Exception("Service will be available from " + startWork + ":00 to " + endWork + ":00.");
			}
		}

		[HttpGet("/get-all")]
		public async Task<IEnumerable<SpeedingInf>> GetAll() => await this.fileIDataService.ReedingData();
        

        [HttpGet("/api/speeding-on-date-and-speed")]
		public async Task<IActionResult> GetSpeedingOnDateAndSpeed(DateTime date, double speed)
		{
			try
			{
				ValidateWork();
				return CreatedAtAction(nameof(GetSpeedingOnDateAndSpeed),
                    await this.speedingService.SpeedingOnDateAndSpeed(date, speed));
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
        

        [HttpGet("/api/min-and-max-speed-on-date")]
        public async Task<IActionResult> GetMinAndMaxSpeedOnDate(DateTime date)
		{
            try
            {
                ValidateWork();
				return CreatedAtAction(nameof(GetMinAndMaxSpeedOnDate),
					await this.speedingService.MinAadMaxSpeedOnDate(date));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("/api/add")]
		public async Task<IActionResult> AddSpeedingInf(SpeedingInf speedingInf)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest("Data is not valid");
            }

			try
			{
				ValidateWork();
				await this.fileIDataService.WritingData(speedingInf);
				return Ok();

            }
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}

}
