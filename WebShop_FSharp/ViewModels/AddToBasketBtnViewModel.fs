namespace WebShop_FSharp.ViewModels.CatalogModels

type AddToBasketBtnViewModel() =
    [<DefaultValue>]
    val mutable private userId : int
    [<DefaultValue>]
    val mutable private productId : int
    [<DefaultValue>]
    val mutable private isInBasket : bool

    member public this.UserId with get() = this.userId
                                and set p = this.userId <- p
    member public this.ProductId with get() = this.productId
                                    and set p = this.productId <- p
    member public this.IsInBasket with get() = this.isInBasket
                                    and set p = this.isInBasket <- p