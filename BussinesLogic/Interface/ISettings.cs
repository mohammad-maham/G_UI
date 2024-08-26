using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface
{
    public interface ISettings
    {
        ApiResult SubmitThreshold(AmountThresholdVM thresholdsVM, string token);
    }
}