using G_APIs.Models;
using System.Collections.Generic;

namespace G_APIs.BussinesLogic.Interface
{
    public interface IStore
    {
        double GetOnlinePrice();
        double GetOnlineBuyPrice(PriceCalcVM priceCalc, string token);
        ApiResult PerformBuy(OrderPerformVM buyVM, string token);
        ApiResult PerformSell(OrderPerformVM sellVM, string token);
        GoldRepositoryStatusVM GetGoldRepositoryStatus(string token);
        GoldTypesVM GetGoldTypes(string token);
        ApiResult ChargeRepository(GoldRepositoryManagementVM repositoryManagementVM, string token);
        IEnumerable<GoldRepositoryManagementReportVM> GetGoldRepositoryReport(GoldRepositoryManagementVM repositoryManagementVM, string token);
    }
}
