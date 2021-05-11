namespace sem2_FSharp.ViewModels.AdminPanelModels

open System.Collections.Generic
open sem2_FSharp.ViewModels

type AdminPanelProductsViewModel() =
    [<DefaultValue>]
    val mutable public Category: string
    
    [<DefaultValue>]
    val mutable public Query: string

    [<DefaultValue>]
    val mutable public FilterOption: int

    [<DefaultValue>]
    val mutable public Categories: IEnumerable<CategoryDTO>
    
    [<DefaultValue>]
    val mutable public Products: IEnumerable<AdminPanelProductDTO>
