namespace WebShop_FSharp.ViewModels.OrderModels

open System
open System.Collections.Generic
open Microsoft.AspNetCore.Mvc.Rendering
open System.ComponentModel.DataAnnotations
open Microsoft.AspNetCore.Mvc

type DeliveryToHomeViewModel()=
    [<DefaultValue>]
    val mutable private firstName: string
    [<DefaultValue>]
    val mutable private lastName: string

    [<DefaultValue>]
    val mutable private email: string

    [<DefaultValue>]
    val mutable private phone: string

    [<DefaultValue>]
    val mutable private address:string


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

    
    [<Required(ErrorMessage = "Обязательное поле")>]
    [<DataType(DataType.EmailAddress)>]
    member public this.Email with  get() = this.email
                                and set p = this.email<-p

    member public this.TotalPrice with get() = this.totalPrice
                                    and set p = this.totalPrice<-p
    
    member public this.TotalCount with get() = this.totalCount
                                    and set p = this.totalCount <- p
    [<Required(ErrorMessage = "Обязательное поле")>]
    [<Remote("VerifyAddress","Order",ErrorMessage ="Не удаётся распознать адрес")>]
    member public this.Address with get() = this.address
                                    and set p = this.address <- p



