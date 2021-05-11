namespace sem2_FSharp.ViewModels.AdminPanelModels
//
//open Microsoft.AspNetCore.Mvc
//The class depends on binder. Maybe will rewrite binder to F#
//[<ModelBinder(BinderType = typeof(CreateCategoryPropertyInfoBinder))>]//TODO fix
//type CreateCategoryPropertyInfo() =
//    [<DefaultValue>]
//    val mutable private name: string
//    
//    [<DefaultValue>]
//    val mutable private propertyId: List<CreateCategoryPropertyInfo>
//    
//    member public this.Name with get() = this.name
//                                    and set p = this.name <- p
//                                  
//    member public this.Type with get() = this.propertyId
//                                     and set p = this.propertyId <- p
//                                     
//type CreateCategoryOptionPropertyInfo() =
//    inherit CreateCategoryPropertyInfo()
//    
//    [<DefaultValue>]
//    val mutable private options: List<string>
//    
//    member public this.Options with get() = this.options
//                                    and set p = this.options <- p
//
//type AdminPanelCreateCategoryViewModel() =
//    [<DefaultValue>]
//    val mutable private categoryName: string
//    
//    [<DefaultValue>]
//    val mutable private propertyId: List<CreateCategoryPropertyInfo>
//    
//    member public this.CategoryName with get() = this.categoryName
//                                    and set p = this.categoryName <- p
//                                  
//    member public this.PropertyInfos with get() = this.propertyId
//                                     and set p = this.propertyId <- p
