namespace WebShop_FSharp.ViewModels.AdminPanelModels

type CommandLineResponse(isResponse: bool, isSuccessful: bool, message: string) =
    member val public IsResponse = isResponse with get, set
    member val public IsSuccessful = isSuccessful with get, set
    member val public Message = message with get, set
    
    new() = CommandLineResponse(true, true, null)
    
    static member public Success(message: string) = CommandLineResponse(true, true, message)
    
    static member public Success() = CommandLineResponse.Success(null)
    
    static member public Failure(message: string) = CommandLineResponse(true, false, message)
    
    static member public Failure() = CommandLineResponse.Failure(null)
    
    static member public Empty() = CommandLineResponse(true, false, null)
