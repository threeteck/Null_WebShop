namespace DomainModels

type DeliveryMethods =
     | DeliveryToHome
     | DeliveryToShop

     member this.GetString =
        match this  with
        |DeliveryToHome->"Доставка домой"
        |DeliveryToShop->"Самовывоз"
