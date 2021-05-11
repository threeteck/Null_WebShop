namespace sem2_FSharp.ViewModels.CatalogModels

open System.Collections.Generic

type CatalogViewModel() =    
    [<DefaultValue>]
    val mutable private page: int
    
    [<DefaultValue>]
    val mutable private numberOfPages: int
    
    [<DefaultValue>]
    val mutable private sortingOption: int
        
    [<DefaultValue>]
    val mutable private productList: List<ProductCardDTO>
    
    member public this.Page with get() = this.page
                                and set p = this.page <- p
    member public this.NumberOfPages with get() = this.numberOfPages
                                     and set p = this.numberOfPages <- p
                               
    member public this.SortingOption with get() = this.sortingOption
                                     and set p = this.sortingOption <- p
                              
    member public this.ProductList with get() = this.productList
                                   and set p = this.productList <- p
                              
