namespace WebShop_FSharp.ViewModels.CatalogModels

type ProductCardDTO() =
    [<DefaultValue>]
    val mutable public Id: int
    
    [<DefaultValue>]
    val mutable public Name: string

    [<DefaultValue>]
    val mutable public Price: decimal

    [<DefaultValue>]
    val mutable public ImagePath: string

    [<DefaultValue>]
    val mutable private rating: decimal
    
    member public this.Rating with get() = this.rating
                                and set p = this.rating <- p

    