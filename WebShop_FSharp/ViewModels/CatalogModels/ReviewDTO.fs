namespace WebShop_FSharp.ViewModels.CatalogModels

type ReviewDTO() =
    [<DefaultValue>]
    val mutable private userName : string
    
    [<DefaultValue>]
    val mutable private userId : int
    
    [<DefaultValue>]
    val mutable private userImagePath : string
    
    [<DefaultValue>]
    val mutable private rating : int
    
    [<DefaultValue>]
    val mutable private content : string
    
    member public this.UserName with get() = this.userName
                                and set p = this.userName <- p
                                
    member public this.UserId with get() = this.userId
                              and set p = this.userId <- p
                                
    member public this.UserImagePath with get() = this.userImagePath
                                     and set p = this.userImagePath <- p
                                
    member public this.Rating with get() = this.rating
                              and set p = this.rating <- p
                                
    member public this.Content with get() = this.content
                                and set p = this.content <- p                                                                                     