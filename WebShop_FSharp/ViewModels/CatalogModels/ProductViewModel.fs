namespace WebShop_FSharp.ViewModels.CatalogModels

open System.Collections.Generic
open WebShop_FSharp.ViewModels

type ProductViewModel() =
    [<DefaultValue>]
    val mutable private category : CategoryDTO
    
    [<DefaultValue>]
    val mutable private name : string
    
    [<DefaultValue>]
    val mutable private description : string
    
    [<DefaultValue>]
    val mutable ImagePath : string
    
    [<DefaultValue>]
    val mutable private price : decimal
    
    [<DefaultValue>]
    val mutable private id : int
    
    [<DefaultValue>]
    val mutable private rating : decimal
    
    [<DefaultValue>]
    val mutable Properties : IEnumerable<PropertyDTO>

    [<DefaultValue>]
    val mutable Reviews : IEnumerable<ReviewDTO>
    
    member public this.Category with get() = this.category
                                and set p = this.category <- p
    member public this.Name with get() = this.name
                                and set p = this.name <- p
    member public this.Description with get() = this.description
                                    and set p = this.description <- p
                               
    member public this.Price with get() = this.price
                                and set p = this.price <- p
                            
    member public this.Id with get() = this.id
                              and set p = this.id <- p
                              
    member public this.Rating with get() = this.rating
                              and set p = this.rating <- p
                              