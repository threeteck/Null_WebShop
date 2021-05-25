namespace WebShop_FSharp.ViewModels.OrderModels

open System

type OrderInfoViewModel() = 
    [<DefaultValue>]
    val mutable private orderId: int
    [<DefaultValue>]
    val mutable private createData:DateTime
    [<DefaultValue>]
    val mutable private totalPrice: decimal
    [<DefaultValue>]
    val mutable private state:string

    member public this.OrderId with get() = this.orderId
                                and set p = this.orderId <- p

    member public this.CreateDate with get() = this.createData
                                    and set p = this.createData <- p
    member public this.TotalPrice with get() = this.totalPrice
                                    and set p = this.totalPrice <- p
    member public this.State with get() = this.state
                                     and set p=this.state<-p