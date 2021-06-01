namespace WebShop_FSharp.ViewModels.BasketModels

type BasketProductViewModel() =
    [<DefaultValue>]
    val mutable private productId : int
    [<DefaultValue>]
    val mutable private name : string
    [<DefaultValue>]
    val mutable private price : decimal
    [<DefaultValue>]
    val mutable private imagePath : string
    [<DefaultValue>]
    val mutable private quantity : int
    [<DefaultValue>]
    val mutable private sum : decimal

    member public this.ProductId with get() = this.productId
                                   and set p = this.productId <- p
    member public this.Name with get() = this.name
                                   and set p = this.name <- p
    member public this.Price with get() = this.price
                                   and set p = this.price <- p
    member public this.ImagePath with get() = this.imagePath
                                        and set p = this.imagePath <- p
    member public this.Quantity with get() = this.quantity
                                   and set p = this.quantity <- p
    member public this.Sum with get() = this.sum
                                   and set p = this.sum <- p