namespace sem2_FSharp.ViewModels.CatalogModels

type PropertyDTO() =
    [<DefaultValue>]
    val mutable Name : string
    
    [<DefaultValue>]
    val mutable Value : string
