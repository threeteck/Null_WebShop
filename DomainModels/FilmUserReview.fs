namespace DomainModels

open System
open System.Collections.Generic;
open System.ComponentModel.DataAnnotations

[<CLIMutable>]
type Film =
    {
        [<Key>]
        Id:int
        Name:string
        Description:string
        Price:decimal
        Rating:decimal
        
        
        ImageId:int
        Image:ImageMetadata
        
        
        Reviews:ICollection<Review>
        
        Genres: ICollection<Genre>
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
        
        Subscriptions:ICollection<UserSubscription>
        
        IsConfirmed:bool
    }
 and [<CLIMutable>] Review =
        {
            [<Key>]
            Id:int
            
            FilmId:int
            Film:Film
            
            UserId:int
            User:User
            
            Content:string
            Rating:int
            Date:DateTime
        }
and [<CLIMutable>] UserSubscription =
    {
        Subscription: Subscription
        User: User
        EndDate: DateTime
    }