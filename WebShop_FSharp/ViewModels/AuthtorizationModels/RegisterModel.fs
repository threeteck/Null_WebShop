namespace WebShop_FSharp.ViewModels.AuthtorizationModels

open System.ComponentModel.DataAnnotations

type RegisterModel() =
    [<DefaultValue>]
    val mutable private email: string
    
    [<DefaultValue>]
    val mutable private name: string
    
    [<DefaultValue>]
    val mutable private surname: string
    
    [<DefaultValue>]
    val mutable private password: string
    
    [<DefaultValue>]
    val mutable private confirmPassword: string
    
    [<Required(ErrorMessage = "Не указан Email")>]
    member public this.Email with get() = this.email
                                    and set p = this.email <- p
                                    
    [<Required(ErrorMessage = "Не указано имя")>]
    member public this.Name with get() = this.name
                                    and set p = this.name <- p
                                    
    [<Required(ErrorMessage = "Не указана фамилия")>]
    member public this.Surname with get() = this.surname
                                        and set p = this.surname <- p
                                        
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    member public this.Password with get() = this.password
                                    and set p = this.password <- p
                                    
    [<DataType(DataType.Password)>]
    [<Compare("Password", ErrorMessage = "Пароль введен неверно")>]
    member public this.ConfirmPassword with get() = this.confirmPassword
                                        and set p = this.confirmPassword <- p
