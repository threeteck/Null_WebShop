using System.Collections.Generic;
using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebShop_NULL.Infrastructure;
using WebShop_NULL.Infrastructure.Filters;
using WebShop_NULL.Models;
using WebShop_NULL.Models.ViewModels;

 namespace WebShop_NULL.Controllers
{
    public class SearchController : Controller
    {
        private ILogger _logger;
        private FilterViewModelProvider _filterViewModelProvider;
        private List<FilterViewModel> _testFilterViewModelList; //ONLY FOR TESTING TODO: DELETE

        public SearchController(ILogger<SearchController> logger, FilterViewModelProvider filterViewModelProvider)
        {
            _logger = logger;
            _filterViewModelProvider = filterViewModelProvider;
            
            //FOR TESTING ONLY
            _testFilterViewModelList = new List<FilterViewModel>();
            _testFilterViewModelList.Add(_filterViewModelProvider.GetFilterViewModel(
                "1 Attribute", (int)PropertyTypeEnum.Integer, 0, JsonConvert.DeserializeObject("{min:-10, max:10}")));
            _testFilterViewModelList.Add(_filterViewModelProvider.GetFilterViewModel(
                "2 Attribute", (int)PropertyTypeEnum.Integer, 5, JsonConvert.DeserializeObject("{min:-5, max:5}")));
            _testFilterViewModelList.Add(_filterViewModelProvider.GetFilterViewModel(
                "3 Attribute", (int)PropertyTypeEnum.Integer, 2, JsonConvert.DeserializeObject("{min:-1, max:1}")));
            _testFilterViewModelList.Add(_filterViewModelProvider.GetFilterViewModel(
                "4 Attribute", (int)PropertyTypeEnum.Option, 3, JsonConvert.DeserializeObject("{options:[\"kekw\", \"lulz\"]}")));
        }

        [HttpGet("~/{categoryId:int}/search")]
        public IActionResult Search(int categoryId)
        {
            var model = new SearchViewModel();
            model.Filters = _testFilterViewModelList;
            return View(model);
        }

        [HttpPost("~/{categoryId:int}/search")]
        public IActionResult Search(int categoryId, List<FilterDTO> filters)
        {
            var model = new SearchViewModel();
            model.Filters = _testFilterViewModelList;
            return View(model);
        }
    }
}