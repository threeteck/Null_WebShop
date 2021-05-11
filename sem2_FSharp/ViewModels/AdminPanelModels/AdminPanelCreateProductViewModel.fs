namespace sem2_FSharp.ViewModels.AdminPanelModels
//The class depends on binder. Maybe will rewrite binder to F#
//open System
//open Microsoft.AspNetCore.Mvc
//
//[<ModelBinder(BinderType = typeof(CreateProductPropertyInfoBinder))>]
//type CreateProductPropertyInfo() =
//    [<DefaultValue>]
//    val mutable private propertyId: int
//    
//    [<DefaultValue>]
//    val mutable private value: string
//    
//    member public this.PropertyId with get() = this.propertyId
//                                  and set p = this.propertyId <- p
//                                
//    member public this.Value with get() = this.value
//                                and set p = this.value <- p
//
//type AdminPanelCreateProductViewModel() = //TODO
//    member val public IsResponse: Nullable<int> = null with get, set
