namespace DomainModels

open System.ComponentModel.DataAnnotations;

[<CLIMutable>]
type ImageMetadata =
    {
        [<Key>]
        Id:int
        ImagePath:string
        ContentType:string
    }
    with    
        static member DefaultImage:ImageMetadata =
            {
                Id = 0;
                ImagePath = "applicationData/profileImages/default.jpg";
                ContentType = "image/jpeg"
            }
