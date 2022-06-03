using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPointCalculationAPI
{
    [Route("/")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetAPIStatus()
        {
            return await Task.Run(() => StatusManager.Hit().GetJsonString());
        }

        [HttpGet("GetPoints/{user}/{amount}")]
        public async Task<int> GetPoints(string user, uint amount)
        {
            StatusManager.Hit();

            return await Task.Run(() => {
                return PointCalculator.GetTransactionPoints(new Transaction(user, (int)amount, new DateTime()));
            });
        }
    }
}
