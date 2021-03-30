namespace DomainModels

open System.Collections.Generic;
open System.ComponentModel.DataAnnotations
open System.Text.Json

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
        Category:Category
        
        ImageId:int
        Image:ImageMetadata
        
        AttributeValues:JsonDocument
        
        InBasketOf:ICollection<User>
    }
and [<CLIMutable>] User =
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