namespace sem2_FSharp

open System.Threading.Tasks

type IEmailSender =
    abstract member SendEmailAsync: string -> string -> string -> Task<bool>