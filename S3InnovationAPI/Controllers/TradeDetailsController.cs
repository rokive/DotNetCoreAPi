using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Entity.Models;
using Entity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;

namespace S3InnovationAPI.Controllers
{
    [ApiController]
    public class TradeDetailsController : ControllerBase
    {
        private readonly ITradeServices _tradeServices;

        public TradeDetailsController(ITradeServices tradeServices)
        {
            _tradeServices = tradeServices;
        }
        [HttpGet("api/GetAllTrade", Name = "TradeList")]
        public ActionResult<IEnumerable<Trade>> GetAllTrade()
        {
            var tradeList = _tradeServices.GetTrades().ToList();
            return tradeList;
        }
        [HttpGet("api/GetAllLevelByTradeId/{id}", Name = "llLevelByTradeId")]
        public ActionResult<IEnumerable<Level>> GetAllLevelByTradeId(long id)
        {
            var levelList = _tradeServices.GetAllLevelByTradeId(id).ToList();
            return levelList;
        }
        [HttpGet("api/GetAllLanguage", Name = "LanguageList")]
        public ActionResult<IEnumerable<Language>> GetAllLanguage()
        {
            var LanguageList = _tradeServices.GetLanguages().ToList();
            return LanguageList;
        }

        [HttpPost("api/AddTradeDetails", Name = "TradeDetails")]
        public ActionResult AddTradeDetails([FromBody]TradeDetails tradeDetails)
        {
            if (ModelState.IsValid)
            {

                _tradeServices.CreateTradeDetails(tradeDetails);
                return new OkObjectResult(StatusCode(200));
            }
            return BadRequest();
        }

        [HttpGet("api/GetTradeDetailsList/{pageNo}/{pageSize}/{tradeId}/{levelId}", Name = "TradeDetailsList")]
        public ActionResult<TradeDetailsViewModel> GetTradeDetailsList(int pageNo = 0, int pageSize = 10, long tradeId = 0, long levelId = 0)
        {
            return _tradeServices.GetTradeDetailsList(pageNo, pageSize, tradeId, levelId);
        }

        [HttpGet("api/GetTradeDetailsById/{id}", Name = "TradeDetailsById")]
        public ActionResult<TradeDetails> GetTradeDetailsById(long id)
        {
            TradeDetails tradeDetails = _tradeServices.GetTradeDetailsById(id);
            tradeDetails.LanguageList = _tradeServices.GetLanguages().ToList();
            tradeDetails.TradeList = _tradeServices.GetTrades().ToList();
            tradeDetails.LevelList = tradeDetails.TradeList.Where(m => m.Id == tradeDetails.TradeId).FirstOrDefault().Level;
            string[] selectedlanguages = tradeDetails.Language.Split(',').ToArray();
            foreach (var language in tradeDetails.LanguageList)
            {
                foreach (var selectedlanguage in selectedlanguages)
                {
                    if (language.ShortName== selectedlanguage)
                    {
                        language.isCheck = true;
                    }
                }
            }

            return tradeDetails;
        }

        [HttpPut("api/UpdateTradeDetails/{id}", Name = "UpdateTradeDetails")]
        public ActionResult UpdateTradeDetails(long id, [FromBody]TradeDetails tradeDetails)
        {
            if (ModelState.IsValid)
            {
                TradeDetails model = _tradeServices.GetTradeDetailsById(id);
                model.Language = tradeDetails.Language;
                model.LevelId = tradeDetails.LevelId;
                model.Manager = tradeDetails.Manager;
                model.PlanFile = tradeDetails.PlanFile;
                model.SyllabusFile = tradeDetails.SyllabusFile;
                model.SyllabusName = tradeDetails.SyllabusName;
                model.TradeId = tradeDetails.TradeId;
                _tradeServices.UpdateTradeDetails(model);
                return new OkObjectResult(StatusCode(200));
            }
            return BadRequest();
        }
    }
}