namespace DomainModels

type UserRoleEnum =
    | Normal = 0
    | Admin = 1
    
open System.Collections.Generic;
open System.ComponentModel.DataAnnotations;
    
[<CLIMutable>]
type User =
    {
        [<Key>]
        Id:int
        
        Name:string
        Surname:string
        Email:string
        HashedPassword:string

        ImageId:int
        Image:ImageMetadata
        
        Role:UserRole
        RoleId:int
        
        TotalPayment:decimal
        
        Basket:ICollection<Product>
        
        IsConfirmed:bool
    }
