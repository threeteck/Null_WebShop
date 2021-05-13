namespace WebShop_FSharp.ViewModels.ProfileModels

open System.ComponentModel.DataAnnotations

type UserViewModel() =
    [<DefaultValue>]
    val mutable Id: int
    
    [<DefaultValue>]
    val mutable private name: string
    
    [<DefaultValue>]
    val mutable private surname: string
    
    [<DefaultValue>]
    val mutable Email: string
    
    [<MaxLength(16)>]
    [<Required>]
    member public this.Name with get() = this.name
                            and set p = this.name <- p
                            
    [<MaxLength(16)>]
    [<Required>]
    member public this.Surname with get() = this.surname
                                    and set p = this.surname <- p
