[<AutoOpen>]
module Giraffe.Middleware

open System.Runtime.CompilerServices
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection

type GiraffeMiddleware =
    new: next: RequestDelegate * handler: HttpHandler * loggerFactory: ILoggerFactory -> GiraffeMiddleware
    member Invoke: ctx: HttpContext -> Task<unit>

type GiraffeErrorHandlerMiddleware =
    new:
        next: RequestDelegate * errorHandler: ErrorHandler * loggerFactory: ILoggerFactory ->
            GiraffeErrorHandlerMiddleware

    member Invoke: ctx: HttpContext -> Task<unit>

[<Extension>]
type ApplicationBuilderExtensions =
    new: unit -> ApplicationBuilderExtensions

    /// <summary>
    /// Adds the <see cref="GiraffeMiddleware" /> into the ASP.NET Core pipeline. Any web request which doesn't get handled by a surrounding middleware can be picked up by the Giraffe <see cref="HttpHandler" /> pipeline.
    ///
    /// It is generally recommended to add the <see cref="GiraffeMiddleware" /> after the error handling, static file and any authentication middleware.
    /// </summary>
    /// <param name="builder">The ASP.NET Core application builder.</param>
    /// <param name="handler">The Giraffe <see cref="HttpHandler" /> pipeline. The handler can be anything from a single handler to an entire web application which has been composed from many smaller handlers.</param>
    /// <returns><see cref="Microsoft.FSharp.Core.Unit"/></returns>
    [<Extension>]
    static member UseGiraffe: builder: IApplicationBuilder * handler: HttpHandler -> unit

    /// <summary>
    /// Adds the <see cref="GiraffeErrorHandlerMiddleware" /> into the ASP.NET Core pipeline. The <see cref="GiraffeErrorHandlerMiddleware" /> has been configured in such a way that it only invokes the <see cref="ErrorHandler" /> when an unhandled exception bubbles up to the middleware. It therefore is recommended to add the <see cref="GiraffeErrorHandlerMiddleware" /> as the very first middleware above everything else.
    /// </summary>
    /// <param name="builder">The ASP.NET Core application builder.</param>
    /// <param name="handler">The Giraffe <see cref="ErrorHandler" /> pipeline. The handler can be anything from a single handler to a bigger error application which has been composed from many smaller handlers.</param>
    /// <returns>Returns an <see cref="Microsoft.AspNetCore.Builder.IApplicationBuilder"/> builder object.</returns>
    [<Extension>]
    static member UseGiraffeErrorHandler: builder: IApplicationBuilder * handler: ErrorHandler -> IApplicationBuilder

[<Extension>]
type ServiceCollectionExtensions =
    new: unit -> ServiceCollectionExtensions

    /// <summary>
    /// Adds default Giraffe services to the ASP.NET Core service container.
    ///
    /// The default services include features like <see cref="Json.ISerializer"/>, <see cref="Xml.ISerializer"/>, <see cref="INegotiationConfig"/> or more. Please check the official Giraffe documentation for an up to date list of configurable services.
    /// </summary>
    /// <returns>Returns an <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> builder object.</returns>
    [<Extension>]
    static member AddGiraffe: svc: IServiceCollection -> IServiceCollection
