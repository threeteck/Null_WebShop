namespace DomainModels

open System
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
        Reviews:ICollection<Review>
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
 and [<CLIMutable>] Review =
        {
            [<Key>]
            Id:int
            
            ProductId:int
            Product:Product
            
            UserId:int
            User:User
            
            Content:string
            Rating:int
            Date:DateTime
        }