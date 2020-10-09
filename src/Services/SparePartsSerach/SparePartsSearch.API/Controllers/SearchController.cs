using Microsoft.AspNetCore.Mvc;
using SparePartsSearch.API.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SearchController : ControllerBase
    {
        ISearchService searchService;
        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }
       

        [HttpGet("{partName}")]
        public IActionResult GetSparePartPrice(string partName, [FromBody]string[] carCharacteristics)
        {
            try
            {
                var sparePart = searchService.FindSparePartPrice(partName, carCharacteristics);
                return Ok(sparePart);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpGet]
        //public IActionResult GetSpareParts()
        //{

        //}


    }
}
