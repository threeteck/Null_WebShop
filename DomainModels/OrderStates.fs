namespace DomainModels
open System.Collections;
open System.Collections.Generic


type public IOrderStates = 
    abstract member GetAllStates : unit->string[]
    abstract member CheckIfCorrectOrderState: string -> bool
    abstract member GetDefaultState : unit->string
    abstract member GetStateCssClass: string->string
    abstract member GetActiveStates: unit-> string[]
    abstract member GetBlockedStates: unit->string[]

type ToHomeDeliveryOrder()=
    let states = [|"Заказ формируется";"Заказ в пути";"Заказ завершён";"Заказ отменён"|]
    interface IOrderStates with
        member this.CheckIfCorrectOrderState(str: string): bool = 
            Array.contains str states
        member this.GetAllStates(): string [] = 
            states
        member this.GetDefaultState(): string = 
            "Заказ формируется"
        member this.GetStateCssClass(state: string): string =
            match state with 
            | "Заказ формируется" -> "badge-info"
            | "Заказ в пути" ->  "badge-info"
            | "Заказ завершён" -> "badge-success"
            | "Заказ отменён" -> "badge-danger"

        member this.GetActiveStates(): string[]=
            [|"Заказ формируется";"Заказ в пути";|]
        member this.GetBlockedStates(): string[]=
            [|"Заказ завершён";"Заказ отменён"|]

type ToShopDeliveryOrder()=
    let states = [|"Заказ формируется";"Заказ в пути";"Заказ прибыл в магазин";"Заказ завершён";"Заказ отменён"|]
    interface IOrderStates with
        member this.CheckIfCorrectOrderState(str: string): bool = 
            Array.contains str states
        member this.GetAllStates(): string [] = 
            states
        member this.GetDefaultState(): string =
            "Заказ формируется"
        member this.GetStateCssClass(state: string): string =
            match state with 
            | "Заказ формируется" -> "badge-info"
            | "Заказ в пути" ->  "badge-info"
            | "Заказ прибыл в магазин" -> "badge-warning"
            | "Заказ завершён" -> "badge-success"
            | "Заказ отменён" -> "badge-danger"

        member this.GetActiveStates(): string[]=
            [|"Заказ формируется";"Заказ в пути";"Заказ прибыл в магазин"|]
        member this.GetBlockedStates(): string[]=
            [|"Заказ завершён";"Заказ отменён"|]
