namespace DomainModels

open System.ComponentModel.DataAnnotations
type Shop=
    {
        [<Key>]
        Id:int
        Name:string
        Address:string
        CityName:string
    }