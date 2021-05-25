namespace DomainModels

open System.ComponentModel.DataAnnotations
open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations.Schema


type Order() = 
    [<DefaultValue>]
    [<Key>]
    val mutable private id: int
    [<DefaultValue>]
    val mutable private userId: int
    [<DefaultValue>]
    val mutable private user: User

    [<DefaultValue>]
    val mutable private deliveryMethod: string
    [<DefaultValue>]
    val mutable private createDate:DateTime
    [<DefaultValue>]
    val mutable private state: string

    [<DefaultValue>]
    val mutable private orderItems: ICollection<OrderItems>

    [<DefaultValue>]
    val mutable private address: string
    [<DefaultValue>]
    val mutable private totalPrice: decimal
    [<DefaultValue>]
    val mutable private totalCount: int

    [<DefaultValue>]
    val mutable private orderStates: IOrderStates

    member public this.Id with  get() = this.id
                             and set p = this.id <- p

    member public this.UserId with get() = this.userId
                              and set p = this.userId <- p
    member public this.User with  get() = this.user
                                and set p = this.user <- p
    member public this.DeliveryMethod with get() = this.deliveryMethod
                                        and set p = this.deliveryMethod <- p
    member public this.CreateDate with get() = this.createDate
                                    and set p = this.createDate <-p
    member public this.State with get() = this.state
                                    and set p = this.state <-p
    member public this.OrderItems with get() = this.orderItems
                                    and set p = this.orderItems<-p
    member public this.TotalPrice with get() = this.totalPrice
                                    and set p = this.totalPrice<-p
    member public this.TotalCount with get() = this.totalCount
                                    and set p = this.totalCount <- p
    member public this.Address with get() = this.address
                                    and set p = this.address <- p

 

and OrderItems() =
        [<DefaultValue>]
        [<Key>]
        val mutable private id: int
        [<DefaultValue>]
        val mutable private orderId:int
        [<DefaultValue>]
        val mutable private order:Order
        [<DefaultValue>]
        val mutable private productName:string
        [<DefaultValue>]
        val mutable productPrice: decimal
        [<DefaultValue>]
        val mutable productQuantity:int
        [<DefaultValue>]
        val mutable productId:int

        member this.Id with public get() = this.id
                                    and private set p = this.id <- p

        member public this.OrderId with get() = this.orderId
                                        and set p = this.orderId <- p
        member public this.Order with get() = this.order
                                        and set p = this.order <- p
        member public this.ProductName with get() = this.productName
                                        and set p = this.productName <- p
        member public this.ProductPrice with get() = this.productPrice
                                        and set p = this.productPrice <- p
        member public this.ProductQuantity with get() = this.productQuantity
                                            and set p = this.productQuantity <- p
        member public this.ProductId with get() = this.productId
                                            and set p = this.productId <- p
    
