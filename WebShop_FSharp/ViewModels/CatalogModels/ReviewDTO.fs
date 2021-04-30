namespace WebShop_FSharp.ViewModels.CatalogModels

type ReviewDTO() =
    [<DefaultValue>]
    val mutable public UserName : string
    
    [<DefaultValue>]
    val mutable public UserId : int
    
    [<DefaultValue>]
    val mutable public UserImagePath : string
    
    [<DefaultValue>]
    val mutable public Rating : int
    
    [<DefaultValue>]
    val mutable public Content : string
