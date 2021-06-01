namespace WebShop_FSharp.ViewModels.AdminPanelModels

open DomainModels
open System.Collections.Generic
open System.ComponentModel.DataAnnotations
open Microsoft.AspNetCore.Mvc.Rendering

type ShopsViewModel()=
    [<DefaultValue>]
    val mutable private shops: IEnumerable<Shop>
    [<DefaultValue>]
    val mutable private cityNames: IEnumerable<SelectListItem>
    [<DefaultValue>]
    val mutable private cityName: string
    [<DefaultValue>]
    val mutable private shopName: string
    [<DefaultValue>]
    val mutable private shopAddress: string

    member public this.Shops with get() = this.shops
                                and set p = this.shops <- p
    member public this.CityNames with get() = this.cityNames
                                 and set p = this.cityNames <- p
    [<Required(ErrorMessage = "Поле является обязательным")>]
    member public this.CityName with get() = this.cityName
                                and set p = this.cityName <- p
    [<Required(ErrorMessage = "Поле является обязательным")>]
    member public this.ShopName with get() = this.shopName
                                    and set p = this.shopName <- p
    [<Required(ErrorMessage = "Поле является обязательным")>]
    member public this.ShopAddress with get() = this.shopAddress
                                    and set p = this.shopAddress <- p