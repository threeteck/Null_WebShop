module IntegrationTests.PublicAreaTests

open System.Net
open Xunit
open Factory

[<Theory>]
[<InlineData("/Home")>]
[<InlineData("/")>]
[<InlineData("/Product/1")>]
[<InlineData("/Account/Login")>]
[<InlineData("/Account/Register")>]
[<InlineData("/Account/ConfirmEmail")>]
//[<InlineData("/Account/EmailConfirmationEnd")>]
let readTest(url : string) =
    let factory = new TestFactory()
    let client = ref (factory.CreateClient())
    let response = client.Value.GetAsync(url) |> Async.AwaitTask |> Async.RunSynchronously
    Assert.True(response.Content.Headers.ContentType.MediaType = "text/html")
    Assert.True(response.StatusCode = HttpStatusCode.OK)