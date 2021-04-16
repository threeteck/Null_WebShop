namespace WebShop_FSharp.Middleware

open System.Collections.Generic
open DomainModels
open Microsoft.AspNetCore.Http
open System.Linq

type CitiesMiddleware(next: RequestDelegate) =
    let mutable cities: List<string> = null

    member this.Next = next
    
    
    member this.InvokeAsync(context: HttpContext, dbContext: ApplicationContext) =
        cities <- if cities = null then dbContext.Cities.Select(fun x -> x.Name).ToList() else cities;
        context.Items.["cities"] <- cities;
        next.Invoke(context);


