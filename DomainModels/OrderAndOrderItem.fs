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
    val mutable private totalPrice: decimal
    [<DefaultValue>]
    val mutable private totalCount: int

    [<DefaultValue>]
    val mutable private orderStates: IOrderStates

    member this.Id with public get() = this.id
                             and private set p = this.id <- p

    member this.UserId with public get() = this.userId
                              and private set p = this.userId <- p
    member this.User with public get() = this.user
                                and private set p = this.user <- p
    member this.DeliveryMethod with public get() = this.deliveryMethod
                                   and private set p = this.deliveryMethod <- p
    member this.CreateDate with public get() = this.createDate
                                    and private set p = this.createDate <-p
    member this.State with public get() = this.state
                                    and private set p = this.state <-p
    member this.OrderItems with public get() = this.orderItems
                                    and private set p = this.orderItems<-p
    member this.TotalPrice with public get() = this.totalPrice
                                    and private set p = this.totalPrice<-p
    member this.TotalCount with public get() = this.totalCount
                                    and private set p = this.totalCount <- p

    [<NotMapped>]
    member this.OrderStates with public get() = this.orderStates
                                and private set p = this.orderStates <- p

    member this.SetOrderState(orderState:string) = 
        match this.OrderStates.CheckIfCorrectOrderState(orderState) with
        | true -> this.State = orderState
 
 

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
    
