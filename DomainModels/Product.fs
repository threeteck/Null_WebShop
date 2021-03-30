namespace DomainModels

open System.ComponentModel.DataAnnotations;
//TODO
[<CLIMutable>]
type Product =
    {
        [<Key>]
        Id:int
        Name:string
        Description:string
        Price:decimal
        Rating:decimal
        
        CategoryId:int
        
    }