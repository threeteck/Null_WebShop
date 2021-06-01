namespace WebShop_FSharp

open Newtonsoft.Json
open Dadata.Model
open Dadata
open System.Threading.Tasks


type AddressValidator() = 
    member private this.GetBool(address: Task<Address>) =
        async {
            let! res = address|>Async.AwaitTask
            return res.qc_house.Equals("2") && (not (res.house.Equals("0")))         
        }
    interface IAddressValidator with
        member this.IsAddressValid(address:string): bool = 
            let token = "c5b043354a04009bbeeca1da66740ca6f5d458b6"
            let secret = "585e1c3e21794ea714d8316d400c5e8f1e785aeb"
            let api = CleanClientAsync(token,secret)
            let addressResult = api.Clean<Address>(address)
            let result = this.GetBool(addressResult)|>Async.RunSynchronously
            result
            
        

