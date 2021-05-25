namespace WebShop_FSharp.ViewModels.OrderModels

open System
open System.Collections.Generic
open Microsoft.AspNetCore.Mvc.Rendering
open System.ComponentModel.DataAnnotations

type DeliveryToShopViewModel()=
    [<DefaultValue>]
    val mutable private firstName: string
    [<DefaultValue>]
    val mutable private lastName: string

    [<DefaultValue>]
    val mutable private city: string
    [<DefaultValue>]
    val mutable private cities: SelectList

    [<DefaultValue>]
    val mutable private shopAddress: string
    [<DefaultValue>]
    val mutable private shopAddresses: SelectList

    [<DefaultValue>]
    val mutable private totalPrice: decimal
    [<DefaultValue>]
    val mutable private totalCount: int

    [<Required(ErrorMessage = "Обязательное поле")>]
    member public this.FirstName with  get() = this.firstName
                                    and set p = this.firstName <- p
    [<Required(ErrorMessage = "Обязательное поле")>]
    member public this.LastName with  get() = this.lastName
                                and set p = this.lastName <- p
    member public this.TotalPrice with get() = this.totalPrice
                                    and set p = this.totalPrice<-p
    
    member public this.TotalCount with get() = this.totalCount
                                    and set p = this.totalCount <- p
    [<Required(ErrorMessage = "Обязательное поле")>]
    member public this.City with get() = this.city
                                    and set p = this.city <- p
    member public this.Cities with get() = this.cities
                                    and set p = this.cities <- p
    //[<Required(ErrorMessage = "Обязательное поле")>]
    member public this.ShopAddress with get() = this.shopAddress
                                    and set p = this.shopAddress <- p
    member public this.ShopAddresses with get() = this.shopAddresses
                                        and set p = this.shopAddresses <- p

