namespace WebShop_FSharp.ViewModels.ProfileModels

open System.ComponentModel.DataAnnotations

type PasswordChangeModel() =
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    [<DefaultValue>]
    val mutable private oldPassword: string
    
    [<Required(ErrorMessage = "Не указан пароль")>]
    [<DataType(DataType.Password)>]
    [<DefaultValue>]
    val mutable private newPassword: string
    
    [<Required(ErrorMessage = "Пароль введен неверно")>]
    [<DataType(DataType.Password)>]
    [<DefaultValue>]
    val mutable private confirmPassword: string
    
    member public this.OldPassword with get() = this.oldPassword
                                    and set p = this.oldPassword <- p
                                    
    member public this.NewPassword with get() = this.newPassword
                                    and set p = this.newPassword <- p
                                    
    member public this.ConfirmPassword with get() = this.confirmPassword
                                        and set p = this.confirmPassword <- p
                            