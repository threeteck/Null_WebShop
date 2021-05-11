namespace DomainModels

open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Infrastructure

type ApplicationContext(options : DbContextOptions<ApplicationContext>) =
    inherit DbContext(options)
    
    [<DefaultValue>]
    val mutable categories : DbSet<Category>
    
    [<DefaultValue>]
    val mutable reviews : DbSet<Review>
    
    [<DefaultValue>]
    val mutable products : DbSet<Film>
    
    [<DefaultValue>]
    val mutable properties : DbSet<Property>
    
    [<DefaultValue>]
    val mutable propertyTypes : DbSet<PropertyType>
    
    [<DefaultValue>]
    val mutable users : DbSet<User>
    
    [<DefaultValue>]
    val mutable imageMetadata : DbSet<ImageMetadata>
    
    [<DefaultValue>]
    val mutable userRole : DbSet<UserRole>
    
    [<DefaultValue>]
    val mutable cities : DbSet<City>

    member public this.Reviews with get() = this.reviews
                                  and set p = this.reviews <- p
                                  
    member public this.Categories with get() = this.categories
                                  and set p = this.categories <- p
    member public this.Products with get() = this.products
                                  and set p = this.products <- p
    member public this.Properties with get() = this.properties
                                  and set p = this.properties <- p
    member public this.PropertyTypes with get() = this.propertyTypes
                                     and set p = this.propertyTypes <- p
    member public this.Users with get() = this.users
                             and set p = this.users <- p
    member public this.ImageMetadata with get() = this.imageMetadata
                                     and set p = this.imageMetadata <- p
    member public this.UserRoles with get() = this.userRole
                                  and set p = this.userRole <- p
    member public this.Cities with get() = this.cities
                                  and set p = this.cities <- p
                                  
//    override __.OnConfiguring(optionsBuilder : DbContextOptionsBuilder) =
//        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=dotnetsem") |> ignore
