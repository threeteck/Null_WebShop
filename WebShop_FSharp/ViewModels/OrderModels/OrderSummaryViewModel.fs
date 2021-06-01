namespace WebShop_FSharp.ViewModels.OrderModels

type OrderSummaryViewModel() =
    [<DefaultValue>]
    val mutable private totalPrice : decimal;
    [<DefaultValue>]
    val mutable private totalCount : int;

    member public this.TotalPrice with get() = this.totalPrice
                                    and set p = this.totalPrice <- p
    member public this.TotalCount with get() = this.totalCount
                                    and set p = this.totalCount <- p

