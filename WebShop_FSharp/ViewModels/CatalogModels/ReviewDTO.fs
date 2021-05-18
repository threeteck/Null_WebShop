namespace WebShop_FSharp.ViewModels.CatalogModels

open DomainModels

type FilterViewModel() =
    [<DefaultValue>]
    val mutable private propertyId : int
    
    [<DefaultValue>]
    val mutable private propertyName : string
    
    [<DefaultValue>]
    val mutable private propertyType : PropertyTypeEnum
                                
    member public this.PropertyId with get() = this.propertyId
                                  and set p = this.propertyId <- p
                                
    member public this.PropertyName with get() = this.propertyName
                                    and set p = this.propertyName <- p
                                
    member public this.PropertyType with get() = this.propertyType
                                    and set p = this.propertyType <- p                                                                      