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
        services.AddSingleton(typedefof<IMessenger<_>>,typedefof<Messenger<_>>) |> ignore
        services.AddSingleton(typedefof<ICache<_>>,typedefof<Cache<_>>) |> ignore