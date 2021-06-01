namespace WebShop_FSharp.ViewModels.AdminPanelModels


open System
open System.Collections.Generic
open Microsoft.AspNetCore.Mvc.Rendering
open System.ComponentModel.DataAnnotations
open System.Configuration

type SearchByEnum = 
    |Order = 0
    |User = 1

type AdminPanelOrdersViewModel() = 
    [<DefaultValue>]
    val mutable private queryId: int
    [<DefaultValue>]
    val mutable private orders: IEnumerable<AdminPanelOrderInfoViewModel>
    [<DefaultValue>]
    val mutable private searchBy: IEnumerable<SelectListItem>
    [<DefaultValue>]
    val mutable private stringSearchBy: string

    [<Range(1,Int32.MaxValue, ErrorMessage = "Число должно быть положительным")>]
    member public this.QueryId  with get() = this.queryId
                                            and set p = this.queryId <- p
    member public this.Orders with get() = this.orders
                                and set p = this.orders <- p
    member public this.SearchBy with get() = this.searchBy
                                and set p = this.searchBy <- p
    member public this.StringSearchBy with get() = this.stringSearchBy
                                        and set p = this.stringSearchBy <- p




