[<AutoOpen>]
module Giraffe.Preconditional

open System
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http
open Microsoft.Net.Http.Headers

type Precondition =
    | NoConditionsSpecified
    | ResourceNotModified
    | ConditionFailed
    | AllConditionsMet

type EntityTagHeaderValue with

    /// <summary>
    /// Creates an object of type <see cref="EntityTagHeaderValue"/>.
    /// </summary>
    /// <param name="isWeak">The difference between a regular (strong) ETag and a weak ETag is that a matching strong ETag guarantees the file is byte-for-byte identical, whereas a matching weak ETag indicates that the content is semantically the same. So if the content of the file changes, the weak ETag should change as well.</param>
    /// <param name="eTag">The entity tag value (without quotes or the W/ prefix).</param>
    /// <returns>Returns an object of <see cref="EntityTagHeaderValue"/>.</returns>
    static member FromString: isWeak: bool -> eTag: string -> EntityTagHeaderValue

[<Extension>]
type PreconditionExtensions =
    new: unit -> PreconditionExtensions

    /// <summary>
    /// Validates the following conditional HTTP headers of the HTTP request:
    ///
    /// If-Match
    ///
    /// If-None-Match
    ///
    /// If-Modified-Since
    ///
    /// If-Unmodified-Since
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="eTag">Optional ETag. You can use the static EntityTagHeaderValue.FromString helper method to generate a valid <see cref="EntityTagHeaderValue"/> object.</param>
    /// <param name="lastModified">Optional <see cref="System.DateTimeOffset"/> object denoting the last modified date.</param>
    /// <returns>
    /// Returns a Precondition union type, which can have one of the following cases:
    ///
    /// NoConditionsSpecified: No validation has taken place, because the client didn't send any conditional HTTP headers.
    ///
    /// ConditionFailed: At least one condition couldn't be satisfied. It is advised to return a 412 status code back to the client (you can use the HttpContext.PreconditionFailedResponse() method for that purpose).
    ///
    /// ResourceNotModified: The resource hasn't changed since the last visit. The server can skip processing this request and return a 304 status code back to the client (you can use the HttpContext.NotModifiedResponse() method for that purpose).
    ///
    /// AllConditionsMet: All pre-conditions can be satisfied. The server should continue processing the request as normal.
    /// </returns>
    [<Extension>]
    static member ValidatePreconditions:
        ctx: HttpContext * eTag: EntityTagHeaderValue option * lastModified: DateTimeOffset option -> Precondition

    /// <summary>
    /// Sends a default HTTP 304 Not Modified response to the client.
    /// </summary>
    /// <returns></returns>
    [<Extension>]
    static member NotModifiedResponse: ctx: HttpContext -> HttpContext option

    /// <summary>
    /// Sends a default HTTP 412 Precondition Failed response to the client.
    /// </summary>
    /// <returns></returns>
    [<Extension>]
    static member PreconditionFailedResponse: ctx: HttpContext -> HttpContext option

/// <summary>
/// Validates the following conditional HTTP headers of the request:
///
/// If-Match
///
/// If-None-Match
///
/// If-Modified-Since
///
/// If-Unmodified-Since
///
///
/// If the conditions are met (or non existent) then it will invoke the next http handler in the pipeline otherwise it will return a 304 Not Modified or 412 Precondition Failed response.
/// </summary>
/// <param name="eTag">Optional ETag. You can use the static EntityTagHeaderValue.FromString helper method to generate a valid <see cref="EntityTagHeaderValue"/> object.</param>
/// <param name="lastModified">Optional <see cref="System.DateTimeOffset"/> object denoting the last modified date.</param>
/// <param name="next"></param>
/// <param name="ctx"></param>
/// <returns>A Giraffe <see cref="HttpHandler" /> function which can be composed into a bigger web application.</returns>
val validatePreconditions:
    eTag: EntityTagHeaderValue option ->
    lastModified: DateTimeOffset option ->
    next: HttpFunc ->
    ctx: HttpContext ->
        HttpFuncResult
