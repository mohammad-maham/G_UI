using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using System;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic
{
    public class Settings : ISettings
    {
        public ApiResult SubmitThreshold(AmountThresholdVM thresholdsVM, string token)
        {
            try
            {
                ApiResult response = new GoldApi(GoldHost.Store, "/api/Shopping/ManageThresholds", thresholdsVM, authorization: token).Post();
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}