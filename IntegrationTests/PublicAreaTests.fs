module IntegrationTests.PublicAreaTests

open System.Net
open Xunit
open Factory

[<Theory>]
[<InlineData("/Home")>]
[<InlineData("/")>]
[<InlineData("/1/search")>]
[<InlineData("/Product/1")>]
[<InlineData("/Account/Login")>]
[<InlineData("/Account/Register")>]
[<InlineData("/Account/ConfirmEmail")>]
[<InlineData("/Account/EmailConfirmationEnd?key=12345&userId=23")>]
let readTest(url : string) =
    let factory = new TestFactory()
    let client = ref (factory.CreateClient())
    let response = client.Value.GetAsync(url) |> Async.AwaitTask |> Async.RunSynchronously
    Assert.True(response.Content.Headers.ContentType.MediaType = "text/html")
    Assert.True(response.StatusCode = HttpStatusCode.OK)
   
//[<Theory>]
//[<InlineData("/Order/VerifyAddress?address=qqqqqqqqqqqqqqq")>]
//let jsonTest(url : string) =
//    let factory = new TestFactory()
//    let client = ref (factory.CreateClient())
//    let response = client.Value.GetAsync(url) |> Async.AwaitTask |> Async.RunSynchronously
//    Assert.True(response.Content.Headers.ContentType.MediaType = "application/json")
//    //Assert.True(response.StatusCode = HttpStatusCode.OK)
