namespace WebShop_FSharp.ViewModels.ProfileModels

open System.ComponentModel.DataAnnotations

type PasswordChangeModel() =
    [<DefaultValue>]
    val mutable private oldPassword: string
    
    [<DefaultValue>]
    val mutable private newPassword: string
    
    [<DefaultValue>]
    val mutable private confirmPassword: string
    
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    member public this.OldPassword with get() = this.oldPassword
                                    and set p = this.oldPassword <- p
                                    
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    member public this.NewPassword with get() = this.newPassword
                                    and set p = this.newPassword <- p
                                    
    [<Required(ErrorMessage = "Пароль введен неверно")>]
    [<DataType(DataType.Password)>]
    member public this.ConfirmPassword with get() = this.confirmPassword
                                        and set p = this.confirmPassword <- p
                            