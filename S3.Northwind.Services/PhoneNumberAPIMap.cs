using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace S3.Northwind.Services
{
    public class PhoneNumberAPIMap
    {
        private const string apiKey = "9e03f07b64003cf61c19019317d4ea59";        
        private string phoneNumber;

        public string Url
        {
            get
            {
                return $"http://apilayer.net/api/validate?access_key={apiKey}&number={PhoneNumber}";
            }
        }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }


        private async Task<string> CallAPI()
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync(Url);
            }
        }

        public PhoneNumberAPI PhoneNumberReturnedFromApiCall()
        {
            Task<string> resultTask = CallAPI();
            return JsonConvert.DeserializeObject<PhoneNumberAPI>(resultTask.Result);
        }
    }
}
