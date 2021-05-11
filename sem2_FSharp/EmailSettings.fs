namespace sem2_FSharp

type EmailSettings() =
    
    [<DefaultValue>]
    val mutable private username : string
    
    [<DefaultValue>]
    val mutable private password : string
    
    [<DefaultValue>]
    val mutable private address : string
    
    [<DefaultValue>]
    val mutable private port : int
    
    [<DefaultValue>]
    val mutable private useSSL : bool
    
    member public this.Username with get() = this.username
                                and set p = this.username <- p
    member public this.Password with get() = this.password
                                and set p = this.password <- p
    member public this.Address with get() = this.address
                               and set p = this.address <- p
    member public this.Port with get() = this.port
                            and set p = this.port <- p
    member public this.UseSSL with get() = this.useSSL
                              and set p = this.useSSL <- p
