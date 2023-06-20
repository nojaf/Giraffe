namespace Giraffe.EndpointRouting

open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Routing
open FSharp.Core
open Giraffe

module private RouteTemplateBuilder =
    val convertToRouteTemplate: path: PrintfFormat<'a, 'b, 'c, 'd, 'T> -> string * (string * char) list

module private RequestDelegateBuilder =
    val createRequestDelegate: handler: HttpHandler -> RequestDelegate

    val createTokenizedRequestDelegate:
        mappings: (string * char) list ->
        tokenizedHandler: ('T -> HttpFunc -> HttpContext -> HttpFuncResult) ->
            RequestDelegate

[<AutoOpen>]
module Routers =
    type HttpVerb =
        | GET
        | POST
        | PUT
        | PATCH
        | DELETE
        | HEAD
        | OPTIONS
        | TRACE
        | CONNECT
        | NotSpecified

        override ToString: unit -> string

    type RouteTemplate = string
    type RouteTemplateMappings = list<string * char>
    type MetadataList = obj list

    type Endpoint =
        | SimpleEndpoint of HttpVerb * RouteTemplate * HttpHandler * MetadataList
        | TemplateEndpoint of HttpVerb * RouteTemplate * RouteTemplateMappings * (obj -> HttpHandler) * MetadataList
        | NestedEndpoint of RouteTemplate * Endpoint list * MetadataList
        | MultiEndpoint of Endpoint list

    val GET_HEAD: (Endpoint list -> Endpoint)
    val GET: (Endpoint list -> Endpoint)
    val POST: (Endpoint list -> Endpoint)
    val PUT: (Endpoint list -> Endpoint)
    val PATCH: (Endpoint list -> Endpoint)
    val DELETE: (Endpoint list -> Endpoint)
    val HEAD: (Endpoint list -> Endpoint)
    val OPTIONS: (Endpoint list -> Endpoint)
    val TRACE: (Endpoint list -> Endpoint)
    val CONNECT: (Endpoint list -> Endpoint)
    val route: path: string -> handler: HttpHandler -> Endpoint

    val routef:
        path: PrintfFormat<'a, 'b, 'c, 'd, 'T> ->
        routeHandler: ('T -> HttpFunc -> HttpContext -> HttpFuncResult) ->
            Endpoint

    val subRoute: path: string -> endpoints: Endpoint list -> Endpoint
    val applyBefore: httpHandler: HttpHandler -> endpoint: Endpoint -> Endpoint
    val applyAfter: httpHandler: HttpHandler -> endpoint: Endpoint -> Endpoint
    val addMetadata: metadata: obj -> endpoint: Endpoint -> Endpoint

[<Extension>]
type EndpointRouteBuilderExtensions =
    new: unit -> EndpointRouteBuilderExtensions

    [<Extension>]
    static member MapGiraffeEndpoints: builder: IEndpointRouteBuilder * endpoints: Endpoint list -> unit

[<Extension>]
type ApplicationBuilderExtensions =
    new: unit -> ApplicationBuilderExtensions

    /// <summary>
    /// Uses ASP.NET Core's Endpoint Routing middleware to register Giraffe endpoints.
    /// </summary>
    [<Extension>]
    static member UseGiraffe: builder: IApplicationBuilder * endpoints: Endpoint list -> IApplicationBuilder
