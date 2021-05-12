namespace WebShop_FSharp.ViewModels.AdminPanelModels

type AdminPanelProductDTO() =
    [<DefaultValue>]
    val mutable public Id: int
    
    [<DefaultValue>]
    val mutable public Name: string

    [<DefaultValue>]
    val mutable public CategoryName: string

    [<DefaultValue>]
    val mutable public Rating: double
    
    [<DefaultValue>]
    val mutable public Price: double
