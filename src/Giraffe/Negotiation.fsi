[<AutoOpen>]
module Giraffe.Negotiation

open System.Collections.Generic
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http

/// <summary>
/// Interface defining the negotiation rules and the <see cref="HttpHandler" /> for unacceptable requests when doing content negotiation in Giraffe.
/// </summary>
type INegotiationConfig =
    /// <summary>
    /// A dictionary of mime types and response writing <see cref="HttpHandler" /> functions.
    ///
    /// Each mime type must be mapped to a function which accepts an obj and returns a <see cref="HttpHandler" /> which will send a response in the associated mime type.
    /// </summary>
    /// <example>
    /// <code>
    /// dict [ "application/json", json; "application/xml" , xml ]
    /// </code>
    /// </example>
    abstract member Rules: IDictionary<string, obj -> HttpHandler>

    /// <summary>
    /// A <see cref="HttpHandler" /> function which will be invoked if none of the accepted mime types can be satisfied. Generally this <see cref="HttpHandler" /> would send a response with a status code of 406 Unacceptable.
    /// </summary>
    /// <returns></returns>
    abstract member UnacceptableHandler: HttpHandler

/// <summary>
/// The default implementation of <see cref="INegotiationConfig."/>
///
/// Supported mime types:
///
/// */*: If a client accepts any content type then the server will return a JSON response.
///
/// application/json: Server will send a JSON response.
///
/// application/xml: Server will send an XML response.
///
/// text/xml: Server will send an XML response.
///
/// text/plain: Server will send a plain text response (by suing an object's ToString() method).
/// </summary>
type DefaultNegotiationConfig =
    new: unit -> DefaultNegotiationConfig
    interface INegotiationConfig

/// <summary>
/// An implementation of INegotiationConfig which allows returning JSON only.
///
/// Supported mime types:
///
/// */*: If a client accepts any content type then the server will return a JSON response.
/// application/json: Server will send a JSON response.
/// </summary>
type JsonOnlyNegotiationConfig =
    new: unit -> JsonOnlyNegotiationConfig
    interface INegotiationConfig

[<Extension>]
type NegotiationExtensions =
    new: unit -> NegotiationExtensions

    /// <summary>
    /// Sends a response back to the client based on the request's Accept header.
    ///
    /// If the Accept header cannot be matched with one of the supported mime types from the negotiationRules then the unacceptableHandler will be invoked.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="negotiationRules">A dictionary of mime types and response writing <see cref="HttpHandler" /> functions. Each mime type must be mapped to a function which accepts an obj and returns a <see cref="HttpHandler" /> which will send a response in the associated mime type (e.g.: dict [ "application/json", json; "application/xml" , xml ]).</param>
    /// <param name="unacceptableHandler"> A <see cref="HttpHandler" /> function which will be invoked if none of the accepted mime types can be satisfied. Generally this <see cref="HttpHandler" /> would send a response with a status code of 406 Unacceptable.</param>
    /// <param name="responseObj">The object to send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member NegotiateWithAsync:
        ctx: HttpContext *
        negotiationRules: IDictionary<string, (obj -> HttpFunc -> HttpContext -> HttpFuncResult)> *
        unacceptableHandler: HttpHandler *
        responseObj: obj ->
            HttpFuncResult

    /// <summary>
    /// Sends a response back to the client based on the request's Accept header.
    ///
    /// The negotiation rules as well as a <see cref="HttpHandler" /> for unacceptable requests can be configured in the ASP.NET Core startup code by registering a custom class of type <see cref="INegotiationConfig"/>.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="responseObj">The object to send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member NegotiateAsync: ctx: HttpContext * responseObj: obj -> HttpFuncResult

/// <summary>
/// Sends a response back to the client based on the request's Accept header.
///
/// If the Accept header cannot be matched with one of the supported mime types from the negotiationRules then the unacceptableHandler will be invoked.
/// </summary>
/// <param name="negotiationRules">A dictionary of mime types and response writing <see cref="HttpHandler" /> functions. Each mime type must be mapped to a function which accepts an obj and returns a <see cref="HttpHandler" /> which will send a response in the associated mime type (e.g.: dict [ "application/json", json; "application/xml" , xml ]).</param>
/// <param name="unacceptableHandler">A <see cref="HttpHandler" /> function which will be invoked if none of the accepted mime types can be satisfied. Generally this <see cref="HttpHandler" /> would send a response with a status code of 406 Unacceptable.</param>
/// <param name="responseObj">The object to send back to the client.</param>
/// <param name="ctx"></param>
/// <returns>A Giraffe <see cref="HttpHandler" /> function which can be composed into a bigger web application.</returns>
val negotiateWith:
    negotiationRules: IDictionary<string, (obj -> HttpFunc -> HttpContext -> HttpFuncResult)> ->
    unacceptableHandler: HttpHandler ->
    responseObj: obj ->
    HttpFunc ->
    ctx: HttpContext ->
        HttpFuncResult

/// <summary>
/// Sends a response back to the client based on the request's Accept header.
///
/// The negotiation rules as well as a <see cref="HttpHandler" /> for unacceptable requests can be configured in the ASP.NET Core startup code by registering a custom class of type <see cref="INegotiationConfig"/>.
/// </summary>
/// <param name="responseObj">The object to send back to the client.</param>
/// <param name="ctx"></param>
/// <returns>A Giraffe <see cref="HttpHandler" /> function which can be composed into a bigger web application.</returns>
val negotiate: responseObj: obj -> HttpFunc -> ctx: HttpContext -> HttpFuncResult
