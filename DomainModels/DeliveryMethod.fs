namespace DomainModels

type DeliveryMethods =
     | DeliveryToHome
     | DeliveryToShop

     member this.GetString =
        match this  with
        |DeliveryToHome->"Самовывоз"
        |DeliveryToShop->"Доставка домой"
