module WebShop_FSharp.Utilities

open System
open System.Linq

let removeNonLettersFromBeginning(s: string) =
        new string(s.SkipWhile(fun x -> not(Char.IsLetter(x))).ToArray())