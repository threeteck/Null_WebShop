namespace WebShop_FSharp.ViewModels.AdminPanelModels

open System

type OrderState()=
    [<DefaultValue>]
    val mutable private state:string

    [<DefaultValue>]
    val mutable private cssClass:string


    member public this.State with get() = this.state
                                and set p=this.state<-p

    member public this.CssClass with get() = this.cssClass
                                and set p=this.cssClass<-p

type AdminPanelOrderInfoViewModel() = 
    [<DefaultValue>]
    val mutable private orderId: int
    [<DefaultValue>]
    val mutable private createDate:DateTime
    [<DefaultValue>]
    val mutable private ownerId: int
    [<DefaultValue>]
    val mutable private orderState:OrderState

    member public this.OrderId with get() = this.orderId
                                and set p = this.orderId <- p

    member public this.CreateDate with get() = this.createDate
                                    and set p = this.createDate <- p
    member public this.OwnerId with get() = this.ownerId
                                    and set p = this.ownerId <- p
    member public this.OrderState with get() = this.orderState
                                     and set p=this.orderState<-p

