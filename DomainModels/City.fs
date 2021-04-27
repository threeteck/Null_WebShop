namespace DomainModels

open System.ComponentModel.DataAnnotations;
open System.ComponentModel.DataAnnotations.Schema;

[<CLIMutable>]
type City =
    {
        [<Key>]
        [<DatabaseGenerated(DatabaseGeneratedOption.None)>]
        Name:string
    }