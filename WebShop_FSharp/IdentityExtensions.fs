namespace WebShop_FSharp

open System
open System.Runtime.CompilerServices
open System.Security.Claims

[<Extension>]
type IdentityExtensions =
    [<Extension>]
     static member inline public GetId(principal: ClaimsPrincipal) =
        if principal.Identity.IsAuthenticated then
            let first = principal.FindFirst("id")
            let id = if first = null then null else first.Value
            let parseSuccessful, result = Int32.TryParse id
            if parseSuccessful then result else -1
        else -1
       
    static member inline public GetName(principal: ClaimsPrincipal) =
        if principal.Identity.IsAuthenticated then
            let first = principal.FindFirst("username")
            if first = null then null else first.Value
        else null
