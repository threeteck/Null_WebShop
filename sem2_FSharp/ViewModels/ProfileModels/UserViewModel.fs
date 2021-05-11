namespace sem2_FSharp.ViewModels.ProfileModels

open System.ComponentModel.DataAnnotations

type UserViewModel() =
    [<DefaultValue>]
    val mutable Id: int
    
    [<MaxLength(16)>]
    [<Required>]
    [<DefaultValue>]
    val mutable private name: string
    
    [<MaxLength(16)>]
    [<Required>]
    [<DefaultValue>]
    val mutable private surname: string
    
    [<DefaultValue>]
    val mutable Email: string
    
    member public this.Name with get() = this.name
                            and set p = this.name <- p
                                    
    member public this.Surname with get() = this.surname
                                    and set p = this.surname <- p
