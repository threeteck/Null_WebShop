namespace WebShop_FSharp

open System.Linq
open System.Runtime.CompilerServices
open DomainModels

[<Extension>]
type QueryableExtensions =
    [<Extension>]
    static member inline ById(userSet: IQueryable<User>, id: int) = userSet.Where(fun user -> user.Id = id)
    
    [<Extension>]
    static member inline ImageById(userSet: IQueryable<User>, id: int) = userSet.ById(id).Select(fun user -> user.Image)
    
    [<Extension>]
    static member inline ById(productSet: IQueryable<Product>, id: int) = productSet.Where(fun product -> product.Id = id)
    
    [<Extension>]
    static member inline ImageById(productSet: IQueryable<Product>, id: int) = productSet.ById(id).Select(fun product -> product.Image)
