namespace WebShop_FSharp.ViewModels.OrderModels

open System
open WebShop_FSharp.ViewModels.AdminPanelModels


type OrderInfoViewModel() = 
    [<DefaultValue>]
    val mutable private orderId: int
    [<DefaultValue>]
    val mutable private createDate:DateTime
    [<DefaultValue>]
    val mutable private totalPrice: decimal
    [<DefaultValue>]
    val mutable private state: OrderState

    member public this.OrderId with get() = this.orderId
                                and set p = this.orderId <- p

    member public this.CreateDate with get() = this.createDate
                                    and set p = this.createDate <- p
    member public this.TotalPrice with get() = this.totalPrice
                                    and set p = this.totalPrice <- p
    member public this.State with get() = this.state
                                     and set p=this.state<-p