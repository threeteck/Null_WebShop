namespace WebShop_FSharp.ViewModels

type CategoryDTO(id: int, name: string) =
    member val public Id = id with get, set
    member val public Name = name with get, set
    
    new() = CategoryDTO(0, null)
    
