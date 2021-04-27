namespace WebShop_FSharp

open System.Reflection
open System.Runtime.CompilerServices
open Microsoft.Extensions.DependencyInjection

[<Extension>]
type ServicesExtensions =
     [<Extension>]
     static member inline public AddControllersFromAssembly(services: IServiceCollection, assemblyName: string) =
        let assembly = Assembly.Load assemblyName
        services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices
