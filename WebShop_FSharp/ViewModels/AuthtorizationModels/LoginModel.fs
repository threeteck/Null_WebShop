namespace WebShop_FSharp.ViewModels.AuthtorizationModels

open System.ComponentModel.DataAnnotations

type LoginModel() =
    [<DefaultValue>]
    val mutable private email: string
    
    [<DefaultValue>]
    val mutable private password: string
    
    [<DefaultValue>]
    val mutable private rememberMe: string
    
    [<Required(ErrorMessage = "Не указан Email")>]
    member public this.Email with get() = this.email
                                    and set p = this.email <- p
                                    
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    member public this.Password with get() = this.password
                                    and set p = this.password <- p
                                    
    member public this.RememberMe with get() = this.rememberMe
                                        and set p = this.rememberMe <- p
