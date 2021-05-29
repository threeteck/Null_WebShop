namespace WebShop_FSharp.ViewModels.OrderModels

open System
open System.Collections.Generic
open DomainModels

type OrderPageViewModel() = 
    [<DefaultValue>]
    val mutable private orderId: int
    [<DefaultValue>]
    val mutable private orderState: string
    [<DefaultValue>]
    val mutable private deliveryMethod: string
    [<DefaultValue>]
    val mutable private createDate: DateTime
    [<DefaultValue>]
    val mutable private address: string
    [<DefaultValue>]
    val mutable private orderItems: ICollection<OrderItems>
    [<DefaultValue>]
    val mutable private totalPrice: decimal
    [<DefaultValue>]
    val mutable private totalCount: int

    member public this.OrderId with get() = this.orderId
                                and set p = this.orderId <- p

    member public this.OrderState with get() = this.orderState
                                    and set p = this.orderState <- p

    member public this.DeliveryMethod with get() = this.deliveryMethod
                                      and set p = this.deliveryMethod <- p

    member public this.CreateDate with get() = this.createDate
                                    and set p = this.createDate <- p

    member public this.Address with get() = this.address
                                    and set p = this.address <- p

    member public this.OrderItems with get() = this.orderItems
                                    and set p = this.orderItems <- p

    member public this.TotalPrice with get() = this.totalPrice
                                    and set p = this.totalPrice <- p

    member public this.TotalCount with get() = this.totalCount
                                    and set p = this.totalCount <- p