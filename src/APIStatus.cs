using System;
using Newtonsoft.Json;
using CustomerPointCalculationAPI.Logs;

namespace CustomerPointCalculationAPI
{
    public enum Status
    {
        Stopped,
        Running
    }

    public class StatusManager
    {
        public static int TotalRequests = 0;

        public static APIStatus DefaultAPIStatus = new APIStatus();

        public static APIStatus Hit()
        {
            StatusManager.TotalRequests++;
            StatusManager.DefaultAPIStatus.Update();

            return StatusManager.DefaultAPIStatus;
        }
    }

    /// <summary>
    /// Stores the status of all the API's services.
    /// </summary>
    public class APIStatus
    {
        public int TotalRequests { get; set; }

        public DateTime ServerDateTime { get; set; }

        public Status DatabaseServerStatus { get; set; }

        public void Update()
        {
            this.TotalRequests = StatusManager.TotalRequests;
            this.ServerDateTime = DateTime.Now;
            this.DatabaseServerStatus = ((Database.DefaultSqlConnection == null) ? Status.Stopped : Status.Running);
        }

        public string GetJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public APIStatus()
        {
            this.TotalRequests = StatusManager.TotalRequests;
            this.ServerDateTime = DateTime.Now;
            this.DatabaseServerStatus = ((Database.DefaultSqlConnection == null) ? Status.Stopped : Status.Running);
        }
    }
}
