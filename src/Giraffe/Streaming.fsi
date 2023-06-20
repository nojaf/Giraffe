[<AutoOpen>]
module Giraffe.Streaming

open System
open System.IO
open System.Runtime.CompilerServices
open System.Collections.Generic
open Microsoft.AspNetCore.Http
open Microsoft.Net.Http.Headers

type internal RangeBoundary =
    { Start: int64
      End: int64 }

    member internal Length: int64

/// <summary>
/// A collection of helper functions to parse and validate the Range and If-Range HTTP headers of a request.
/// </summary>
module internal RangeHelper =
    /// **Description**
    ///
    /// Parses the Range HTTP header of a request.
    ///
    /// Original code taken from ASP.NET Core:
    ///
    /// https://github.com/aspnet/StaticFiles/blob/dev/shared/Microsoft.AspNetCore.RangeHelper.Sources/RangeHelper.cs
    ///
    val parseRange: request: HttpRequest -> ICollection<RangeItemHeaderValue> option

    /// <summary>
    /// Validates if the provided set of ranges can be satisfied with the given contentLength.
    /// </summary>
    /// <param name="ranges"></param>
    /// <param name="contentLength"></param>
    /// <returns></returns>
    val validateRanges:
        ranges: ICollection<RangeItemHeaderValue> -> contentLength: int64 -> Result<RangeBoundary, string>

    /// <summary>
    /// Parses and validates the If-Range HTTP header
    /// </summary>
    /// <param name="request"></param>
    /// <param name="eTag"></param>
    /// <param name="lastModified"></param>
    /// <returns></returns>
    val isIfRangeValid:
        request: HttpRequest -> eTag: EntityTagHeaderValue option -> lastModified: DateTimeOffset option -> bool

[<Extension>]
type StreamingExtensions =
    new: unit -> StreamingExtensions

    [<Extension>]
    static member internal RangeUnit: HttpContext -> string

    [<Extension>]
    static member internal WriteStreamToBodyAsync:
        ctx: HttpContext * stream: Stream * rangeBoundary: RangeBoundary option ->
            Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Streams data to the client.
    ///
    /// The handler will respect any valid HTTP pre-conditions (e.g. If-Match, If-Modified-Since, etc.) and return the most appropriate response. If the optional parameters eTag and/or lastModified have been set, then it will also set the ETag and/or Last-Modified HTTP headers in the response.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="enableRangeProcessing">If enabled then the handler will respect the Range and If-Range HTTP headers of the request as well as set all necessary HTTP headers in the response to enable HTTP range processing.</param>
    /// <param name="stream">The stream to be send to the client.</param>
    /// <param name="eTag">An optional entity tag which identifies the exact version of the data.</param>
    /// <param name="lastModified">An optional parameter denoting the last modified date time of the data.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteStreamAsync:
        ctx: HttpContext *
        enableRangeProcessing: bool *
        stream: Stream *
        eTag: EntityTagHeaderValue option *
        lastModified: DateTimeOffset option ->
            Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Streams a file to the client.
    ///
    /// The handler will respect any valid HTTP pre-conditions (e.g. If-Match, If-Modified-Since, etc.) and return the most appropriate response. If the optional parameters eTag and/or lastModified have been set, then it will also set the ETag and/or Last-Modified HTTP headers in the response.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="enableRangeProcessing">If enabled then the handler will respect the Range and If-Range HTTP headers of the request as well as set all necessary HTTP headers in the response to enable HTTP range processing.</param>
    /// <param name="filePath">The absolute or relative path (to ContentRoot) of the file.</param>
    /// <param name="eTag">An optional entity tag which identifies the exact version of the file.</param>
    /// <param name="lastModified">An optional parameter denoting the last modified date time of the file.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteFileStreamAsync:
        ctx: HttpContext *
        enableRangeProcessing: bool *
        filePath: string *
        eTag: EntityTagHeaderValue option *
        lastModified: DateTimeOffset option ->
            Threading.Tasks.Task<HttpContext option>

/// <summary>
/// Streams data to the client.
///
/// The handler will respect any valid HTTP pre-conditions (e.g. If-Match, If-Modified-Since, etc.) and return the most appropriate response. If the optional parameters eTag and/or lastModified have been set, then it will also set the ETag and/or Last-Modified HTTP headers in the response.
/// </summary>
/// <param name="enableRangeProcessing">enableRangeProcessing: If enabled then the handler will respect the Range and If-Range HTTP headers of the request as well as set all necessary HTTP headers in the response to enable HTTP range processing.</param>
/// <param name="stream">The stream to be send to the client.</param>
/// <param name="eTag">An optional entity tag which identifies the exact version of the data.</param>
/// <param name="lastModified">An optional parameter denoting the last modified date time of the file.</param>
/// <param name="ctx"></param>
/// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
val streamData:
    enableRangeProcessing: bool ->
    stream: Stream ->
    eTag: EntityTagHeaderValue option ->
    lastModified: DateTimeOffset option ->
    HttpFunc ->
    ctx: HttpContext ->
        HttpFuncResult

/// <summary>
/// Streams a file to the client.
///
/// The handler will respect any valid HTTP pre-conditions (e.g. If-Match, If-Modified-Since, etc.) and return the most appropriate response. If the optional parameters eTag and/or lastModified have been set, then it will also set the ETag and/or Last-Modified HTTP headers in the response.
/// </summary>
/// <param name="enableRangeProcessing">If enabled then the handler will respect the Range and If-Range HTTP headers of the request as well as set all necessary HTTP headers in the response to enable HTTP range processing.</param>
/// <param name="filePath">The absolute or relative path (to ContentRoot) of the file.</param>
/// <param name="eTag">An optional entity tag which identifies the exact version of the file.</param>
/// <param name="lastModified">An optional parameter denoting the last modified date time of the file.</param>
/// <param name="ctx"></param>
/// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
val streamFile:
    enableRangeProcessing: bool ->
    filePath: string ->
    eTag: EntityTagHeaderValue option ->
    lastModified: DateTimeOffset option ->
    HttpFunc ->
    ctx: HttpContext ->
        HttpFuncResult
