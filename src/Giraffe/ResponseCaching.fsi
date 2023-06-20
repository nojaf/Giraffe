namespace Giraffe

[<AutoOpen>]
module ResponseCaching =
    open System
    open Microsoft.AspNetCore.Http

    /// <summary>
    /// Specifies the directive for the `Cache-Control` HTTP header:
    ///
    /// NoCache: The resource should not be cached under any circumstances.
    /// Public: Any client and proxy may cache the resource for the given amount of time.
    /// Private: Only the end client may cache the resource for the given amount of time.
    /// </summary>
    type CacheDirective =
        | NoCache
        | Public of TimeSpan
        | Private of TimeSpan

    /// <summary>
    /// Enables (or suppresses) response caching by clients and proxy servers.
    /// This http handler integrates with ASP.NET Core's response caching middleware.
    ///
    /// The responseCaching http handler will set the relevant HTTP response headers in order to enable response caching on the client, by proxies (if public) and by the ASP.NET Core middleware (if enabled).
    /// </summary>
    /// <param name="directive">Specifies the cache directive to be set in the response's HTTP headers. Use NoCache to suppress caching altogether or use Public`/`Private to enable caching for everyone or clients only.</param>
    /// <param name="vary">Optionally specify which HTTP headers have to match in order to return a cached response (e.g. Accept and/or Accept-Encoding).</param>
    /// <param name="varyByQueryKeys">An optional list of query keys which will be used by the ASP.NET Core response caching middleware to vary (potentially) cached responses. If this parameter is used then the ASP.NET Core response caching middleware has to be enabled. For more information check the official [VaryByQueryKeys](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/middleware?view=aspnetcore-2.1#varybyquerykeys) documentation.</param>
    /// <param name="next"></param>
    /// <param name="ctx"></param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    val responseCaching:
        directive: CacheDirective ->
        vary: string option ->
        varyByQueryKeys: string array option ->
        next: HttpFunc ->
        ctx: HttpContext ->
            HttpFuncResult

    /// <summary>
    /// Disables response caching by clients and proxy servers.
    /// </summary>
    /// <returns>A Giraffe `HttpHandler` function which can be composed into a bigger web application.</returns>
    val noResponseCaching: HttpHandler
    /// <summary>
    /// Enables response caching for clients only.
    ///
    /// The <see cref="responseCaching"/> http handler will set the relevant HTTP response headers in order to enable response caching on the client only.
    /// </summary>
    /// <param name="seconds">Specifies the duration (in seconds) for which the response may be cached.</param>
    /// <param name="vary">Optionally specify which HTTP headers have to match in order to return a cached response (e.g. Accept and/or Accept-Encoding).</param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    val privateResponseCaching: seconds: int -> vary: string option -> HttpHandler
    /// <summary>
    /// Enables response caching for clients and proxy servers.
    /// This http handler integrates with ASP.NET Core's response caching middleware.
    ///
    /// The <see cref="responseCaching"/> http handler will set the relevant HTTP response headers in order to enable response caching on the client, by proxies and by the ASP.NET Core middleware (if enabled).
    ///
    /// </summary>
    /// <param name="seconds">Specifies the duration (in seconds) for which the response may be cached.</param>
    /// <param name="vary">Optionally specify which HTTP headers have to match in order to return a cached response (e.g. `Accept` and/or `Accept-Encoding`).</param>
    /// <returns>A Giraffe <see cref="HttpHandler"/> function which can be composed into a bigger web application.</returns>
    val publicResponseCaching: seconds: int -> vary: string option -> HttpHandler
