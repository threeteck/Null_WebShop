namespace sem2_FSharp.ViewModels

type ErrorViewModel(requestId: string) =
    let mutable requestId = requestId
    
    member public this.RequestId with get() = requestId
                                 and set p = requestId <- p
                                 
    member public this.ShowRequestId = not(System.String.IsNullOrEmpty(this.RequestId))
    
    new() = ErrorViewModel(null)