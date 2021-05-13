namespace WebShop_FSharp

open System
open System.IO
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Mvc
open System.Linq

[<Extension>]
type CustomTagHelpers =
    [<Extension>]
     static member inline public ImageOrPlaceholder(url: IUrlHelper, imagePath: string, placeholderPath: string) =
        let filePath = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), Utilities.removeNonLettersFromBeginning(imagePath))
        Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
        Console.WriteLine(filePath)
        if File.Exists(filePath)
        then url.Content(imagePath)
        else url.Content(placeholderPath)
        
    [<Extension>]
     static member inline public ProductImage(url: IUrlHelper, imagePath: string) =
        url.ImageOrPlaceholder(imagePath, "/applicationData/productImages/placeholder.jpeg")
