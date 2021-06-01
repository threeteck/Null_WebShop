namespace WebShop_FSharp.ViewModels.CatalogModels

open System.Collections.Generic
open WebShop_FSharp.ViewModels

type CatalogViewModel() =
    [<DefaultValue>]
    val mutable private category: CategoryDTO
    
    [<DefaultValue>]
    val mutable private page: int
    
    [<DefaultValue>]
    val mutable private query: string
    
    [<DefaultValue>]
    val mutable private priceMin: decimal
    
    [<DefaultValue>]
    val mutable private priceMax: decimal
    
    [<DefaultValue>]
    val mutable private ratingMin: decimal
    
    [<DefaultValue>]
    val mutable private ratingMax: decimal
    
    [<DefaultValue>]
    val mutable private numberOfPages: int
    
    [<DefaultValue>]
    val mutable private productsCount: int
    
    [<DefaultValue>]
    val mutable private sortingOption: int
    
    [<DefaultValue>]
    val mutable private categories: ICollection<CategoryDTO>
    
    [<DefaultValue>]
    val mutable private productList: List<ProductCardDTO>
    
    [<DefaultValue>]
    val mutable private filters: List<FilterViewModel>
    
    member public this.Category with get() = this.category
                                and set p = this.category <- p
    member public this.Query with get() = this.query
                                and set p = this.query <- p
    member public this.PriceMin with get() = this.priceMin
                                and set p = this.priceMin <- p
    member public this.PriceMax with get() = this.priceMax
                                and set p = this.priceMax <- p
                                
    member public this.RatingMin with get() = this.ratingMin
                                 and set p = this.ratingMin <- p
                                
    member public this.RatingMax with get() = this.ratingMax
                                 and set p = this.ratingMax <- p
                                
    member public this.Page with get() = this.page
                                and set p = this.page <- p
                                
    member public this.ProductsCount with get() = this.productsCount
                                     and set p = this.productsCount <- p
                                     
    member public this.NumberOfPages with get() = this.numberOfPages
                                     and set p = this.numberOfPages <- p
                               
    member public this.SortingOption with get() = this.sortingOption
                                     and set p = this.sortingOption <- p
                            
    member public this.Categories with get() = this.categories
                                  and set p = this.categories <- p
                              
    member public this.ProductList with get() = this.productList
                                   and set p = this.productList <- p
                              
    member public this.Filters with get() = this.filters
                                   and set p = this.filters <- p