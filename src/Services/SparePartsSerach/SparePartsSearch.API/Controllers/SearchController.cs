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
       
        //TODO: how part name from two words


        [HttpGet("{partName}/{carCharacteristics}")]
        public IActionResult GetSparePartPrice(string partName, string carCharacteristics)
        {
            try
            {
                var sparePart = searchService.FindSparePartPrice(partName, carCharacteristics);
                return Ok(sparePart);
            }
            catch (KeyNotFoundException ex)
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
