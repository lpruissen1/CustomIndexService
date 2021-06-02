using AutoMapper;
using Database.Model.User.CustomIndices;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Mappers;
using UserCustomIndices.Model.Response;
using API = UserCustomIndices.Model.Response;

// convert the fomr API -> DB models
// This is where I want validation to take place
namespace UserCustomIndices.Services
{
    public class CustomIndexService : ICustomIndexService
    {
        private readonly IIndicesRepository indicesRepository;
        private readonly IMapper mapper;
        private readonly IRequestMapper responseMapper;

        public CustomIndexService(IIndicesRepository indicesRepository, IMapper mapper, IRequestMapper responseMapper)
        {
            this.indicesRepository = indicesRepository;
            this.mapper = mapper;
            this.responseMapper = responseMapper;
        }

        public async Task<ActionResult<CustomIndexRequest>> GetIndex(Guid userId, string indexId)
        {
            var index = await indicesRepository.Get(userId, indexId);


            return new ActionResult<CustomIndexRequest>(CreateResponse(index));
        }

        public async Task<ActionResult<IEnumerable<CustomIndexRequest>>> GetAllForUser(string userid)
        {
            var result = await indicesRepository.GetAllForUser(userid);

            return new ActionResult<IEnumerable<CustomIndexRequest>>(result.Select(index => CreateResponse(index)));
        }

        public IActionResult CreateIndex(string userId, CustomIndexRequest customIndex)
        {
            indicesRepository.Create(responseMapper.Map(customIndex));

            return new OkResult(); 
        }

        Task<IActionResult> ICustomIndexService.UpdateIndex(Guid userId, CustomIndexRequest customIndexUpdated)
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> ICustomIndexService.RemoveIndex(Guid userId, string id)
        {
            throw new NotImplementedException();
        }


        private CustomIndexRequest CreateResponse(CustomIndex index)
        {
            return new CustomIndexRequest
            {
				UserId = index.UserId,
				Markets = index.Markets,
				Sectors = index.Sector.Sectors,
				Industries = index.Sector.Industries
				//TimedRangeRule = 
               // Id = index.Id,
               // Markets = new API.ComposedMarkets { Markets = index.Markets.Markets },
               // DividendYield = new API.DividendYield { Lower = index.DividendYield.Lower, Upper = index.DividendYield.Upper },
               // Volitility = new API.Volitility { Lower = index.Volitility.Lower, Upper = index.Volitility.Upper },
               //// TrailingPerformance = new API.TrailingPerformance { Lower = index.TrailingPerformance.Lower, Upper = index.TrailingPerformance.Upper, TimePeriod = index.TrailingPerformance.TimePeriod },
               // // Need to access elements in the list for revenue growth
               // //RevenueGrowth = new API.RevenueGrowth { Lower = index.RevenueGrowths.Lower, Upper = index.RevenueGrowths.Upper, TimePeriod = index.RevenueGrowths.TimePeriod },
               // EarningsGrowth = new API.EarningsGrowth { Lower = index.EarningsGrowth.Lower, Upper = index.EarningsGrowth.Upper },
               // SectorAndIndsutry = new API.Sectors { SectorGroups = index.SectorAndIndsutry.SectorGroups.Select(sector => new API.Sector { Name = sector.Name, Industries = sector.Industries }).ToArray() },
               // MarketCaps = new API.MarketCaps { MarketCapGroups = index.MarketCaps.MarketCapGroups.Select(marketCap => new API.MarketCap { Lower = marketCap.Lower, Upper = marketCap.Upper }).ToArray() },
               // Test = index.Test
            };
        }
    }
}
