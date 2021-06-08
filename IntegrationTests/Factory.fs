module Factory

open Microsoft.AspNetCore.Mvc.Testing
open Microsoft.AspNetCore.Mvc.Testing
open WebShop_NULL

type TestFactory() =
    inherit WebApplicationFactory<Startup>()