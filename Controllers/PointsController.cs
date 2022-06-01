using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPointCalculationAPI
{
    [Route("/")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        [HttpGet("GetPoints/{user}/{amount}")]
        public async Task<int> GetPoints(string user, uint amount)
        {
            return await Task.Run(() => {
                return PointCalculator.GetTransactionPoints(new Transaction(user, (int)amount));
            });
        }
    }
}
