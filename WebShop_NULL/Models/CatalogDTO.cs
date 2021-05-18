using System;
using System.Collections.Generic;
using WebShop_NULL.Infrastructure.Filters;

namespace WebShop_NULL.Models
{
    public class CatalogDTO
    {
        public CatalogDTO(int? categoryId = null, int page = 0, int sortingOption = 0, string query = "", decimal priceMin = Int64.MinValue, decimal priceMax = Int64.MaxValue)
        {
            CategoryId = categoryId;
            Page = page;
            SortingOption = sortingOption;
            Query = query;
            PriceMin = priceMin;
            PriceMax = priceMax;
        }

        public CatalogDTO()
        {
            
        }

        public int? CategoryId { get; set; } = null;
        public int Page { get; set; } = 0;
        public int SortingOption { get; set; } = 0;
        public string Query { get; set; } = null;
        public decimal PriceMin { get; set; } = long.MinValue;
        public decimal PriceMax { get; set; } = long.MaxValue;
        
        public decimal RatingMin { get; set; } = 0;
        public decimal RatingMax { get; set; } = 10;
        public List<FilterDTO> Filters { get; set; } = new List<FilterDTO>();
    }
}