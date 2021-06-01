namespace WebShop_FSharp

open System.Threading.Tasks

type public IAddressValidator =
    abstract member IsAddressValid: string -> bool