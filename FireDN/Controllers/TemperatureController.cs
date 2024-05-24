using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FireDN.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FireDN.Controllers
{
    public class TemperatureController : Controller
    {
        private readonly ILogger<TemperatureController> _logger;
        private readonly FireKLContext _context;
        private static bool scanningEnabled = false; // Biến tĩnh để lưu trạng thái quét
        private static bool savingEnabled = false; // Biến tĩnh để lưu trạng thái lưu

        public TemperatureController(ILogger<TemperatureController> logger, FireKLContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("api/data")]
        public async Task<IActionResult> ReceiveData([FromForm] float temperature, [FromForm] float humidity)
        {
            try
            {
                _logger.LogInformation($"Received temperature: {temperature}, humidity: {humidity}");

                if (scanningEnabled && savingEnabled)
                {
                    int maxTempId = _context.Temps.Any() ? _context.Temps.Max(t => t.Id) : 0;
                    int maxHumiId = _context.Humis.Any() ? _context.Humis.Max(h => h.Id) : 0;

                    var tempData = new Temp
                    {
                        Id = maxTempId + 1,
                        Statistic = temperature.ToString(),
                        ReadTime = DateTime.Now
                    };

                    var humiData = new Humi
                    {
                        Id = maxHumiId + 1,
                        Statistic = humidity.ToString(),
                        ReadTime = DateTime.Now
                    };

                    _context.Temps.Add(tempData);
                    _context.Humis.Add(humiData);

                    await _context.SaveChangesAsync();

                    tempData.TempId = tempData.Id;
                    humiData.HumiId = humiData.Id;

                    await _context.SaveChangesAsync();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ReceiveData: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("api/control")]
        public IActionResult ControlScan([FromForm] string command)
        {
            if (command == "start")
            {
                scanningEnabled = true;
                savingEnabled = false; // Dừng lưu khi bắt đầu quét
            }
            else if (command == "stop")
            {
                scanningEnabled = false;
            }
            else if (command == "save")
            {
                savingEnabled = true;
                scanningEnabled = true; // Bắt đầu quét khi lưu
            }
            else if (command == "stopsave")
            {
                savingEnabled = false;
            }

            _logger.LogInformation($"Scanning enabled: {scanningEnabled}, Saving enabled: {savingEnabled}");
            return Ok();
        }

        [HttpGet]
        [Route("api/control")]
        public IActionResult GetControlSignal()
        {
            return Ok(scanningEnabled ? "start" : "stop");
        }

        [HttpGet]
        [Route("api/data")]
        public IActionResult GetTemperatureAndHumidity()
        {
            var latestTemp = _context.Temps.OrderByDescending(t => t.Id).FirstOrDefault();
            var latestHumi = _context.Humis.OrderByDescending(h => h.Id).FirstOrDefault();

            if (latestTemp != null && latestHumi != null)
            {
                return Ok(new { temperature = latestTemp.Statistic, humidity = latestHumi.Statistic });
            }
            else
            {
                return NotFound();
            }
        }
    }
}