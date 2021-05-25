namespace DomainModels
open System.Collections;
open System.Collections.Generic


type public IOrderStates = 
    abstract member GetAllStates : uint->string[]
    abstract member CheckIfCorrectOrderState: string -> bool
    abstract member GetDefaultState : unit->string

type ToHomeDeliveryOrder()=
    let states = [|"Заказ формируется";"Заказ в пути";"Заказ завершён";"Заказ отменён"|]
    interface IOrderStates with
        member this.CheckIfCorrectOrderState(str: string): bool = 
            Array.contains str states
        member this.GetAllStates(arg1: uint): string [] = 
            states
        member this.GetDefaultState(): string = 
            "Заказ формируется"

type ToShopDeliveryOrder()=
    let states = [|"Заказ формируется";"Заказ в пути";"Заказ прибыл в магазин";"Заказ завершён";"Заказ отменён"|]
    interface IOrderStates with
        member this.CheckIfCorrectOrderState(str: string): bool = 
            Array.contains str states
        member this.GetAllStates(arg1: uint): string [] = 
            states
        member this.GetDefaultState(): string =
            "Заказ формируется"