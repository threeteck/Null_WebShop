namespace DomainModels

open System.ComponentModel.DataAnnotations

type PropertyTypeEnum =
    | Integer = 0
    | Decimal = 1
    | Option = 2
    | Nominal = 3
    
[<CLIMutable>]
type PropertyType =
    {
        [<Key>]
        Id:int
        Name:string
    }