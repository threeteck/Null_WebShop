namespace sem2_FSharp

open System.Linq
open System.Runtime.CompilerServices
open DomainModels

[<Extension>]
type QueryableExtensions =
    [<Extension>]
    static member inline public ById(userSet: IQueryable<User>, id: int) = userSet.Where(fun user -> user.Id = id)
    
    [<Extension>]
    static member inline public ImageById(userSet: IQueryable<User>, id: int) = userSet.ById(id).Select(fun user -> user.Image)
    
    [<Extension>]
    static member inline public ById(productSet: IQueryable<Film>, id: int) = productSet.Where(fun product -> product.Id = id)
    
    [<Extension>]
    static member inline public ImageById(productSet: IQueryable<Film>, id: int) = productSet.ById(id).Select(fun product -> product.Image)
