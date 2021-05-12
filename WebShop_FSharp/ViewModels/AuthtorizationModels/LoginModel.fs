namespace WebShop_FSharp.ViewModels.AuthtorizationModels

open System.ComponentModel.DataAnnotations

type LoginModel() =
    [<Required(ErrorMessage = "Не указан Email")>]
    [<DefaultValue>]
    val mutable private email: string
    
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    [<DefaultValue>]
    val mutable private password: string
    
    [<DefaultValue>]
    val mutable private rememberMe: string
    
    member public this.Email with get() = this.email
                                    and set p = this.email <- p
                                    
    member public this.Password with get() = this.password
                                    and set p = this.password <- p
                                    
    member public this.RememberMe with get() = this.rememberMe
                                        and set p = this.rememberMe <- p
