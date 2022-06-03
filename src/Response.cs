using System;
using Newtonsoft.Json;

namespace CustomerPointCalculationAPI
{
    public enum ResponseType
    {
        Unknown = -1,
        Success,
        Error
    }

    /// <summary>
    /// Basic HTTP response with a specific type and generic payload.
    /// </summary>
    public class Response<T>
    {
        public ResponseType Type;

        public string TypeString;

        public T Payload;

        public static string[] ResponseTypeStrings = new string[] { "Unknown", "Success", "Error" };

        protected void AssertType(ref ResponseType type)
        {
            if ((int)type < 0 || (int)type > 2)
                type = ResponseType.Unknown;
        }

        public string GetJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void SetType(ResponseType type) // Sets the type of the current response to the provided ResponseType without breaking the Json Seralization.
        {
            this.AssertType(ref type);

            this.Type = type;

            this.SetTypeString();
        }

        protected void SetTypeString()
        {
            this.TypeString = Response<string>.ResponseTypeStrings[(int)this.Type + 1];
        }

        public Response()
        {
            this.Type = ResponseType.Unknown;

            this.SetTypeString();

            this.Payload = default(T);
        }

        public Response(ResponseType type, T payload)
        {
            this.Type = type;

            this.AssertType(ref this.Type);
            this.SetTypeString();

            this.Payload = payload;
        }
    }
}
