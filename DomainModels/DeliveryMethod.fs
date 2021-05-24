namespace DomainModels

type DeliveryMethod =
     | DeliveryToHome
     | DeliveryToShop

     member this.GetString =
        match this  with
        |DeliveryToHome->"Самовывоз"
        |DeliveryToShop->"Доставка домой"
