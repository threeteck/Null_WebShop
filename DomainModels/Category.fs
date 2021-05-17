namespace DomainModels

open System.Collections.Generic;
open System.ComponentModel.DataAnnotations

[<CLIMutable>]
type Category =
    {
        [<Key>]
        Id:int
        Name:string
        
        Properties:ICollection<Property>
    }