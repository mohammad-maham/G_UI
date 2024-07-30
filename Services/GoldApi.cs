using Newtonsoft.Json;
using G_APIs.Models;
using RestSharp;
using static G_APIs.Common.Enums;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Text.Json.Nodes;
using System.Configuration;
using System.Threading.Tasks;
using System;

namespace G_APIs.Services
{

    public class GoldApi
    {

        public GoldApi()
        {
        }

        private string ApiPath { get; set; }
        public GoldHost Host { get; set; }
        public string Authorization { get; set; }
        public string Action { get; set; }
        public Method _Method { get; set; }
        public object Data { get; set; }


        //private readonly IConfiguration _config;


        //public GoldApi(IConfiguration config)
        //{
        //    _config = config;

        //}

        public GoldApi(GoldHost host, string action, object data, Method method = Method.Post, string authorization = null)
        {
            if (host == GoldHost.Accounting)
                this.ApiPath = ConfigurationManager.AppSettings["Accounting"];

            if (host == GoldHost.IPG)
                this.ApiPath = ConfigurationManager.AppSettings["GoldApi:IPG"];

            if (host == GoldHost.Store)
                this.ApiPath = ConfigurationManager.AppSettings["GoldApi:Store"];

            if (host == GoldHost.Wallet)
                this.ApiPath = ConfigurationManager.AppSettings["GoldApi:Wallet"];

            this.Action = action;
            this.Authorization = authorization;
            this.Data = data;
            this._Method = method;
        }

        public async Task<ApiResult> PostAsync()
        {
            try
            {
                //var json = JsonConvert.SerializeObject(this.Data);
                var client = new RestClient(this.ApiPath + this.Action);
                var request = new RestRequest
                {
                    Method = this._Method,
                    Timeout = TimeSpan.FromSeconds(20),
                };

                if (this.Authorization != null)
                    request.AddHeader("Authorization", "Bearer " + this.Authorization);

                request.AddHeader("content-type", "application/json");
                request.AddHeader("cache-control", "no-cache");

                //request.AddParameter("username", "1382532326");
                //request.AddParameter("password", "admin");

                request.AddJsonBody(this.Data);

                var response = await client.ExecuteAsync(request);
                var res = JsonConvert.DeserializeObject<ApiResult>(response.Content);

                if (res != null && res.Message != null && res.Message.ToLower().Contains("unauthorize"))
                    res.Message = "ورود غیر مجاز لطفا دوباره وارد شوید.";

                return res;

            }
            catch (Exception ex)
            {
                return new ApiResult()
                {
                    StatusCode = -1,
                    Message = ex.Message
                };
            }
        }
    }
}


