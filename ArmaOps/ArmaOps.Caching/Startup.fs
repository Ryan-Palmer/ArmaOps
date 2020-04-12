namespace ArmaOps.Caching

open Microsoft.Extensions.DependencyInjection
open System.Runtime.CompilerServices
open CachingTypes
open Messenger
open Cache

[<Extension>]
type Startup  () =
        
    [<Extension>]
    static member inline ConfigureCaching (services : IServiceCollection) =
        services.AddScoped(typedefof<IMessenger<_>>,typedefof<Messenger<_>>) |> ignore
        services.AddScoped(typedefof<ICache<_>>,typedefof<Cache<_>>) |> ignore