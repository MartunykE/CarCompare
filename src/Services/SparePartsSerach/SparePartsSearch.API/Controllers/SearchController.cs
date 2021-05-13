using Microsoft.AspNetCore.Mvc;
using SparePartsSearch.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        ISearchService searchService;
        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        [HttpGet("{partName}/{carCharacteristics}")]
        public async Task<IActionResult> GetSparePartPrice(string partName, string carCharacteristics)
        {
            try
            {
                var sparePart = await searchService.FindSparePartPrice(partName, carCharacteristics);
                if (sparePart == null)
                {
                    return NotFound($"Prices for {partName} {carCharacteristics} wasn`t found");
                }
                return Ok(sparePart);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetSpareParts()
        {
            return Ok("search contr");
        }


    }
}
