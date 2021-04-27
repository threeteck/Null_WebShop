namespace WebShop_FSharp.ViewModels.CatalogModels

type ReviewDTO() =
    [<DefaultValue>]
    val mutable UserName : string
    
    [<DefaultValue>]
    val mutable UserId : int
    
    [<DefaultValue>]
    val mutable UserImagePath : string
    
    [<DefaultValue>]
    val mutable Rating : int
    
    [<DefaultValue>]
    val mutable Content : string
