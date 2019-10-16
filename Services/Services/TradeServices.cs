using Entity.Models;
using Repositories.Repositories;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entity.ViewModels;
using Repositorys.DBContext;

namespace Services.Services
{
    public class TradeServices : ITradeServices
    {
        private readonly IGenericRepository<Trade> _genericTradeRepo;
        private readonly IGenericRepository<Level> _genericLevelRepo;
        private readonly IGenericRepository<Language> _genericLanguageRepo;
        private readonly IGenericRepository<TradeDetails> _genericTradeDetailsRepo;

        public TradeServices(IGenericRepository<Trade> genericTradeRepo, IGenericRepository<Level> genericLevelRepo,
           IGenericRepository<Language> genericLanguageRepo, IGenericRepository<TradeDetails> genericTradeDetailsRepo)
        {
            _genericTradeRepo = genericTradeRepo;
            _genericLevelRepo = genericLevelRepo;
            _genericLanguageRepo = genericLanguageRepo;
            _genericTradeDetailsRepo = genericTradeDetailsRepo;
        }

        public void CreateTradeDetails(TradeDetails tradeDetails)
        {
            _genericTradeDetailsRepo.Create(tradeDetails);
            _genericTradeDetailsRepo.Save();
        }

        public IEnumerable<Level> GetAllLevelByTradeId(long id)
        {
            return _genericLevelRepo.Get(m => m.TradeId == id).ToList();
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _genericLanguageRepo.Get();
        }

        public TradeDetails GetTradeDetailsById(long id)
        {
            return _genericTradeDetailsRepo.GetById(id);
        }

        public TradeDetailsViewModel GetTradeDetailsList(int pageNo, int pageSize, long tradeId, long levelId)
        {
            int totalItem = 0; 
            List<TradeDetails> tradeDetailsList = new List<TradeDetails>();
            List<Trade> tradeList = new List<Trade>();
            if (tradeId > 0 && levelId > 0)
            {
                tradeDetailsList = _genericTradeDetailsRepo.Get(m => m.TradeId == tradeId && m.LevelId==levelId, o => o.OrderBy(m => m.Id), "Level", pageSize * pageNo, pageSize).ToList();
                totalItem = _genericTradeDetailsRepo.Get(m => m.TradeId == tradeId && m.LevelId == levelId).Count();
            }
            else if (tradeId>0)
            {
                tradeDetailsList = _genericTradeDetailsRepo.Get(m=>m.TradeId==tradeId, o => o.OrderBy(m => m.Id), "Level", pageSize * pageNo, pageSize).ToList();
                totalItem = _genericTradeDetailsRepo.Get(m => m.TradeId == tradeId).Count();
            }
            else if(levelId>0)
            {
                tradeDetailsList = _genericTradeDetailsRepo.Get(m => m.LevelId == levelId, o => o.OrderBy(m => m.Id), "Level", pageSize * pageNo, pageSize).ToList();
                totalItem = _genericTradeDetailsRepo.Get(m => m.LevelId == levelId).Count();
            }
            else
            {
                
                tradeDetailsList = _genericTradeDetailsRepo.Get(null, o => o.OrderBy(m => m.Id), "Level", pageSize * pageNo, pageSize).ToList();
                totalItem = _genericTradeDetailsRepo.GetCount();
            }
            tradeList = _genericTradeRepo.Get().ToList();
            TradeDetailsViewModel viewModel = new TradeDetailsViewModel()
            {
                TradeDetailsList = tradeDetailsList,
                TotalItem = totalItem,
                TotalPageNo = (int)Math.Ceiling((double)totalItem/ pageSize)
            };
            return viewModel;
        }

        public IEnumerable<Trade> GetTrades()
        {
            return _genericTradeRepo.Get(null,null, "Level");
        }

        public void UpdateTradeDetails(TradeDetails tradeDetails)
        {
            _genericTradeDetailsRepo.Update(tradeDetails);
            _genericTradeDetailsRepo.Save();
        }
    }
}
