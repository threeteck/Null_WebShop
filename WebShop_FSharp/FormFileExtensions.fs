namespace WebShop_FSharp

open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http

[<Extension>]
type FormFileExtensions =
     [<Extension>]
     static member inline public IsImage(services: IFormFile) =
         services.ContentType.Contains("image")
