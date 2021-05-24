namespace DomainModels

open System.ComponentModel.DataAnnotations
[<CLIMutable>]
type Shop=
    {
        [<Key>]
        Id:int
        Name:string
        Address:string
        CityName:string
    }