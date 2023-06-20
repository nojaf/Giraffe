[<AutoOpen>]
module Giraffe.HttpStatusCodeHandlers

open Microsoft.AspNetCore.Http

/// <summary>
/// A collection of <see cref="HttpHandler" /> functions to return HTTP status code 1xx responses.
/// </summary>
module Intermediate =
    val CONTINUE: HttpHandler
    val SWITCHING_PROTO: HttpHandler

/// <summary>
/// A collection of <see cref="HttpHandler" /> functions to return HTTP status code 2xx responses.
/// </summary>
module Successful =
    val ok: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val OK: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val created: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val CREATED: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val accepted: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val ACCEPTED: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val NO_CONTENT: HttpHandler

/// <summary>
/// A collection of <see cref="HttpHandler" /> functions to return HTTP status code 4xx responses.
/// </summary>
module RequestErrors =
    val badRequest: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val BAD_REQUEST: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)

    /// <summary>
    /// Sends a 401 Unauthorized HTTP status code response back to the client.
    ///
    /// Use the unauthorized status code handler when a user could not be authenticated by the server (either missing or wrong authentication data). By returning a 401 Unauthorized HTTP response the server tells the client that it must know who is making the request before it can return a successful response. As such the server must also include which authentication scheme the client must use in order to successfully authenticate.
    /// </summary>
    /// <remarks>
    /// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/WWW-Authenticate
    /// http://stackoverflow.com/questions/3297048/403-forbidden-vs-401-unauthorized-http-responses/12675357
    ///
    /// List of authentication schemes:
    ///
    /// https://developer.mozilla.org/en-US/docs/Web/HTTP/Authentication#Authentication_schemes
    /// </remarks>
    val unauthorized:
        scheme: string -> realm: string -> x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)

    val UNAUTHORIZED:
        scheme: string -> realm: string -> x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)

    val forbidden: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val FORBIDDEN: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val notFound: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val NOT_FOUND: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val methodNotAllowed: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val METHOD_NOT_ALLOWED: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val notAcceptable: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val NOT_ACCEPTABLE: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val conflict: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val CONFLICT: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val gone: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val GONE: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val unsupportedMediaType: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val UNSUPPORTED_MEDIA_TYPE: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val unprocessableEntity: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val UNPROCESSABLE_ENTITY: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val preconditionRequired: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val PRECONDITION_REQUIRED: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val tooManyRequests: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val TOO_MANY_REQUESTS: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)

/// <summary>
/// A collection of <see cref="HttpHandler" /> functions to return HTTP status code 5xx responses.
/// </summary>
module ServerErrors =
    val internalError: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val INTERNAL_ERROR: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val notImplemented: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val NOT_IMPLEMENTED: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val badGateway: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val BAD_GATEWAY: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val serviceUnavailable: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val SERVICE_UNAVAILABLE: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val gatewayTimeout: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val GATEWAY_TIMEOUT: x: 'a -> (HttpFunc -> HttpContext -> HttpFuncResult)
    val invalidHttpVersion: x: HttpHandler -> (HttpFunc -> HttpContext -> HttpFuncResult)
