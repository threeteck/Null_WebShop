namespace DomainModels

open System.ComponentModel.DataAnnotations

[<CLIMutable>]
type Subscription =
    {
        [<Key>]
        Id:int
        Name:string
        Description:string
        Price:decimal
        
        ImageId:int
        Image:ImageMetadata
        
    }
