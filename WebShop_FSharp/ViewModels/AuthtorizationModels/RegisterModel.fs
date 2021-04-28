namespace WebShop_FSharp.ViewModels

open System.ComponentModel.DataAnnotations

type RegisterModel() =
    [<Required(ErrorMessage = "Не указан Email")>]
    [<DefaultValue>]
    val mutable private email: string
    
    [<Required(ErrorMessage = "Не указано имя")>]
    [<DefaultValue>]
    val mutable private name: string
    
    [<Required(ErrorMessage = "Не указана фамилия")>]
    [<DefaultValue>]
    val mutable private surname: string
    
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    [<DefaultValue>]
    val mutable private password: string
    
    [<DataType(DataType.Password)>]
    [<DefaultValue>]
    val mutable private confirmPassword: string
    
    member public this.Email with get() = this.email
                                    and set p = this.email <- p
                                    
    member public this.Name with get() = this.name
                                    and set p = this.name <- p
                                    
    member public this.Surname with get() = this.surname
                                        and set p = this.surname <- p

    member public this.Password with get() = this.password
                                    and set p = this.password <- p
                                    
    [<Compare("Password", ErrorMessage = "Пароль введен неверно")>]
    member public this.ConfirmPassword with get() = this.confirmPassword
                                        and set p = this.confirmPassword <- p
