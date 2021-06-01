namespace WebShop_FSharp.ViewModels.BasketModels

open System.Collections.Generic

type BasketViewModel() =
    [<DefaultValue>]
    val mutable private products : IEnumerable<BasketProductViewModel>
    [<DefaultValue>]
    val mutable private totalSum : decimal
    [<DefaultValue>]
    val mutable private totalQuantity : int
    member public this.Products with get() = this.products
                                and set p = this.products <- p
    member public this.TotalSum with get() = this.totalSum
                                and set p = this.totalSum <- p
    member public this.TotalQuantity with get() = this.totalQuantity
                                        and set p = this.totalQuantity <- p