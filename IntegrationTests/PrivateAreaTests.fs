module IntegrationTests.ProfileTests

open System
open System.Collections.Generic
open System.Net
open System.Net.Http
open DomainModels
open Factory
open Microsoft.AspNetCore.Mvc.Testing.Handlers
open Microsoft.EntityFrameworkCore
open System.Linq
open Xunit

//let applicationContext =
//    let connectionString = "Host=ec2-54-155-208-5.eu-west-1.compute.amazonaws.com;Port=5432;Username=ecqzufaphmcntf;Password=a3aaa4d2d0f6ecf3f207f462e6797f4a4754949d16fb0a0b78a59b3881153291;Database=d8uiu9uo8ff3av;SSL Mode=Require;TrustServerCertificate=True;"
//    let contextOptions = (new DbContextOptionsBuilder<ApplicationContext>()).UseNpgsql(connectionString).Options
//    new ApplicationContext(contextOptions)

//let user = applicationContext.Users.First(fun u -> u.Email = "artemgur01@gmail.com")

let baseAddress = Uri("http://localhost:5001")

let cookieContainer = CookieContainer()
cookieContainer.Add(baseAddress, Cookie("email", "artemgur01@gmail.com"))
cookieContainer.Add(baseAddress, Cookie("password", "a"))

let factory = new TestFactory()

let cookieContainerHandler = new CookieContainerHandler(cookieContainer)

let client =
    let c = ref (factory.CreateDefaultClient(cookieContainerHandler))
    let response = c.Value.PostAsync("/Account/Login", new StringContent("")) |> Async.AwaitTask |> Async.RunSynchronously
    c


[<Theory>]
[<InlineData("/Profile")>]
[<InlineData("/Profile/Orders")>]
[<InlineData("/Profile/ProfileEdit")>]
[<InlineData("/Profile/ShoppingCart")>]
[<InlineData("/adminpanel/CreateCategory")>]
[<InlineData("/adminpanel/Products")>]
[<InlineData("/adminpanel/CreateProduct")>]
[<InlineData("/adminpanel/CommandLine")>]
//[<InlineData("/adminpanel/Index")>]
[<InlineData("/adminpanel/Shops")>]
[<InlineData("/adminpanel/OrderPage?orderId=15")>]
[<InlineData("/adminpanel/Cities")>]
[<InlineData("/Order/ChooseDeliveryMethod")>]
[<InlineData("/Order/GetShops")>]
[<InlineData("/Basket")>]
[<InlineData("/Order/DeliveryToShop")>]
[<InlineData("/Order/GetShops?cityName=Казань")>]
[<InlineData("/Order/DeliveryToHome")>]
let readTest(url : string) =
    let response = client.Value.GetAsync(url) |> Async.AwaitTask |> Async.RunSynchronously
    Assert.True(response.Content.Headers.ContentType.MediaType = "text/html")
    Assert.True(response.StatusCode = HttpStatusCode.OK)
    