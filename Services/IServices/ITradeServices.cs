using Entity.Models;
using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IServices
{
    public interface ITradeServices
    {
        void CreateTradeDetails(TradeDetails tradeDetails);

        void UpdateTradeDetails(TradeDetails tradeDetails);

        TradeDetailsViewModel GetTradeDetailsList(int pageNo,int pageSize,long tradeId,long levelId);

        TradeDetails GetTradeDetailsById(long id);

        IEnumerable<Trade> GetTrades();
        IEnumerable<Level> GetAllLevelByTradeId(long id);
        IEnumerable<Language> GetLanguages();
    }
}
