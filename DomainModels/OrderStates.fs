namespace DomainModels
open System.Collections;
open System.Collections.Generic


type IOrderStates = 
    abstract member GetAllStates : uint->string[]
    abstract member CheckIfCorrectOrderState: string -> bool

type ToHomeDeliveryOrder()=
    let states = [|"Заказ формируется";"Заказ в пути";"Заказ завершён";"Заказ отменён"|]
    interface IOrderStates with
        member this.CheckIfCorrectOrderState(str: string): bool = 
            Array.contains str states
        member this.GetAllStates(arg1: uint): string [] = 
            states

type ToShopDeliveryOrder()=
    let states = [|"Заказ формируется";"Заказ в пути";"Заказ прибыл в магазин";"Заказ завершён";"Заказ отменён"|]
    interface IOrderStates with
        member this.CheckIfCorrectOrderState(str: string): bool = 
            Array.contains str states
        member this.GetAllStates(arg1: uint): string [] = 
            states