using G_APIs.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Threading.Tasks;
using static G_APIs.Common.Enums;

namespace G_APIs.Services
{
    public class GoldApi
    {
        private string ApiPath { get; set; }
        public GoldHost Host { get; set; }
        public string Authorization { get; set; }
        public string Action { get; set; }
        public Method _Method { get; set; }
        public object Data { get; set; }

        public GoldApi(GoldHost host, string action, object data, Method method = Method.Post, string authorization = null)
        {
            switch (host)
            {
                case GoldHost.Accounting:
                    ApiPath = ConfigurationManager.AppSettings["Accounting"];
                    break;
                case GoldHost.IPG:
                    ApiPath = ConfigurationManager.AppSettings["IPG"];
                    break;
                case GoldHost.Store:
                    ApiPath = ConfigurationManager.AppSettings["Store"];
                    break;
                case GoldHost.Wallet:
                    ApiPath = ConfigurationManager.AppSettings["Wallet"];
                    break;
                case GoldHost.Gateway:
                    ApiPath = ConfigurationManager.AppSettings["Gateway"];
                    break;
                default:
                    break;
            }

            Action = action;
            Authorization = authorization;
            Data = data;
            _Method = method;
        }

        public async Task<ApiResult> PostAsync()
        {
            try
            {
                RestClient client = new RestClient(ApiPath + Action);
                RestRequest request = new RestRequest
                {
                    Method = _Method,
                    Timeout = TimeSpan.FromSeconds(20),
                };

                if (Authorization != null)
                {
                    request.AddHeader("Authorization", "Bearer " + Authorization);
                }

                request.AddHeader("content-type", "application/json");
                request.AddHeader("cache-control", "no-cache");

                request.AddJsonBody(Data);

                RestResponse response = await client.ExecuteAsync(request);
                ApiResult res = JsonConvert.DeserializeObject<ApiResult>(response.Content);

                if (res != null && res.Message != null && res.Message.ToLower().Contains("unauthorize"))
                {
                    res.Message = "ورود غیر مجاز لطفا دوباره وارد شوید.";
                }

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

        public ApiResult Post()
        {
            try
            {
                RestClient client = new RestClient(ApiPath + Action);
                RestRequest request = new RestRequest
                {
                    Method = _Method,
                    Timeout = TimeSpan.FromSeconds(20),
                };

                if (Authorization != null)
                {
                    request.AddHeader("Authorization", "Bearer " + Authorization);
                }

                request.AddHeader("content-type", "application/json");
                request.AddHeader("accept-charset", "utf-8");

                request.AddJsonBody(Data);

                RestResponse response = client.Execute(request);
                ApiResult res = JsonConvert.DeserializeObject<ApiResult>(response.Content);

                if (res != null && res.Message != null && res.Message.ToLower().Contains("unauthorize"))
                {
                    res.Message = "ورود غیر مجاز لطفا دوباره وارد شوید.";
                }

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