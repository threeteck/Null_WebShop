namespace DomainModels

open System.ComponentModel.DataAnnotations

[<CLIMutable>]
type UserRole =
    {
        [<Key>]
        Id:int
        Name:string
    }