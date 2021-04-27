namespace WebShop_FSharp.Controllers

open System.Diagnostics
open DomainModels
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open WebShop_FSharp.ViewModels
open WebShop_FSharp.NullCoalescing

type HomeController(dbContext: ApplicationContext, logger: ILogger<HomeController>) =
    inherit Controller()
    
    member private this.dbContext = dbContext
    member private this.logger = logger
    
    
    member public this.Index = this.View
    
    member public this.Privacy = this.View
    
    [<ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)>]
    member public this.Error() =
        let activityCurrent = if Activity.Current = null then null else Activity.Current.Id
        let requestId = nullCoalesce activityCurrent this.HttpContext.TraceIdentifier
        this.View(ErrorViewModel(requestId))
    