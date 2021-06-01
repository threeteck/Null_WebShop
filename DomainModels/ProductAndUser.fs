namespace DomainModels

open System
open System.Collections.Generic;
open System.ComponentModel.DataAnnotations
open System.Text.Json
open Microsoft.EntityFrameworkCore.Metadata.Internal

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
        
        Basket:ICollection<ShoppingCartEntry>
        
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
 and [<CLIMutable>] ShoppingCartEntry =
     {
         [<Key>]
         Id:int
         
         UserId:int
         User:User
         
         ProductId:int
         Product:Product
          
         Quantity:int
     }