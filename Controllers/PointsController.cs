using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

using CustomerPointCalculationAPI.Logs;

namespace CustomerPointCalculationAPI
{
    [Route("/")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetAPIStatus()
        {
            return await Task.Run(() => { return new Response<APIStatus>(ResponseType.Success, StatusManager.Hit()).GetJsonString(); });
        }

        [HttpGet("GetPoints/{userID}")]
        public Task<string> GetPoints(string userID)
        {
            return Task.Run(() => {
                StatusManager.Hit();

                Response<UserPoints> response = null;

                try
                {
                    User user;

                    if ((user =  UserManager.GetUserById(userID)) == null)
                        throw new Exception($"User {user} does not exist.");

                    response = new Response<UserPoints>(ResponseType.Success,
                                            PointCalculator.GetUserPoints(user));
                }
                catch (Exception e)
                {
                    Logger.Log(e.Message, true);

                    return (new Response<string>(ResponseType.Error, e.Message)).GetJsonString();
                }

                return response.GetJsonString();
            });
        }
    }
}
