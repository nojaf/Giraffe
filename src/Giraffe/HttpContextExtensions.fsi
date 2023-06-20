namespace Giraffe

open System
open System.Text
open System.Globalization
open System.Runtime.CompilerServices
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Giraffe.ViewEngine

type MissingDependencyException =
    new: dependencyName: string -> MissingDependencyException
    inherit Exception

[<Extension>]
type HttpContextExtensions =
    new: unit -> HttpContextExtensions

    /// <summary>
    /// Returns the entire request URL in a fully escaped form, which is suitable for use in HTTP headers and other operations.
    /// </summary>
    /// <returns>Returns a <see cref="System.String"/> URL.</returns>
    [<Extension>]
    static member GetRequestUrl: ctx: HttpContext -> string

    /// <summary>
    /// Gets an instance of `'T` from the request's service container.
    /// </summary>
    /// <returns>Returns an instance of `'T`.</returns>
    [<Extension>]
    static member GetService: ctx: HttpContext -> 'T

    /// <summary>
    /// Gets an instance of <see cref="Microsoft.Extensions.Logging.ILogger{T}" /> from the request's service container.
    ///
    /// The type `'T` should represent the class or module from where the logger gets instantiated.
    /// </summary>
    /// <returns> Returns an instance of <see cref="Microsoft.Extensions.Logging.ILogger{T}" />.</returns>
    [<Extension>]
    static member GetLogger: ctx: HttpContext -> ILogger<'T>

    /// <summary>
    /// Gets an instance of <see cref="Microsoft.Extensions.Logging.ILogger"/> from the request's service container.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="categoryName">The category name for messages produced by this logger.</param>
    /// <returns>Returns an instance of <see cref="Microsoft.Extensions.Logging.ILogger"/>.</returns>
    [<Extension>]
    static member GetLogger: ctx: HttpContext * categoryName: string -> ILogger

    /// <summary>
    /// Gets an instance of <see cref="Microsoft.Extensions.Hosting.IHostingEnvironment"/> from the request's service container.
    /// </summary>
    /// <returns>Returns an instance of <see cref="Microsoft.Extensions.Hosting.IHostingEnvironment"/>.</returns>
    [<Extension>]
    static member GetHostingEnvironment: ctx: HttpContext -> IHostingEnvironment

    /// <summary>
    /// Gets an instance of <see cref="Giraffe.Serialization.Json.ISerializer"/> from the request's service container.
    /// </summary>
    /// <returns>Returns an instance of <see cref="Giraffe.Serialization.Json.ISerializer"/>.</returns>
    [<Extension>]
    static member GetJsonSerializer: ctx: HttpContext -> Json.ISerializer

    /// <summary>
    /// Gets an instance of <see cref="Giraffe.Serialization.Xml.Xml.ISerializer"/> from the request's service container.
    /// </summary>
    /// <returns>Returns an instance of <see cref="Giraffe.Serialization.Xml.Xml.ISerializer"/>.</returns>
    [<Extension>]
    static member GetXmlSerializer: ctx: HttpContext -> Xml.ISerializer

    /// <summary>
    /// Sets the HTTP status code of the response.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="httpStatusCode">The status code to be set in the response. For convenience you can use the static <see cref="Microsoft.AspNetCore.Http.StatusCodes"/> class for passing in named status codes instead of using pure int values.</param>
    [<Extension>]
    static member SetStatusCode: ctx: HttpContext * httpStatusCode: int -> unit

    /// <summary>
    /// Adds or sets a HTTP header in the response.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="key">The HTTP header name. For convenience you can use the static <see cref="Microsoft.Net.Http.Headers.HeaderNames"/> class for passing in strongly typed header names instead of using pure `string` values.</param>
    /// <param name="value">The value to be set. Non string values will be converted to a string using the object's ToString() method.</param>
    [<Extension>]
    static member SetHttpHeader: ctx: HttpContext * key: string * value: obj -> unit

    /// <summary>
    /// Sets the Content-Type HTTP header in the response.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="contentType">The mime type of the response (e.g.: application/json or text/html).</param>
    [<Extension>]
    static member SetContentType: ctx: HttpContext * contentType: string -> unit

    /// <summary>
    /// Tries to get the <see cref="System.String"/> value of a HTTP header from the request.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="key">The name of the HTTP header.</param>
    /// <returns> Returns Some string if the HTTP header was present in the request, otherwise returns None.</returns>
    [<Extension>]
    static member TryGetRequestHeader: ctx: HttpContext * key: string -> string option

    /// <summary>
    /// Retrieves the <see cref="System.String"/> value of a HTTP header from the request.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="key">The name of the HTTP header.</param>
    /// <returns>Returns Ok string if the HTTP header was present in the request, otherwise returns Error string.</returns>
    [<Extension>]
    static member GetRequestHeader: ctx: HttpContext * key: string -> Result<string, string>

    /// <summary>
    ///  Tries to get the <see cref="System.String"/> value of a query string parameter from the request.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="key">The name of the query string parameter.</param>
    /// <returns>Returns Some string if the parameter was present in the request's query string, otherwise returns None.</returns>
    [<Extension>]
    static member TryGetQueryStringValue: ctx: HttpContext * key: string -> string option

    /// <summary>
    /// Retrieves the <see cref="System.String"/> value of a query string parameter from the request.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="key">The name of the query string parameter.</param>
    /// <returns>Returns Ok string if the parameter was present in the request's query string, otherwise returns Error string.</returns>
    [<Extension>]
    static member GetQueryStringValue: ctx: HttpContext * key: string -> Result<string, string>

    /// <summary>
    /// Retrieves the <see cref="System.String"/> value of a cookie from the request.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="key">The name of the cookie.</param>
    /// <returns>Returns Some string if the cookie was set, otherwise returns None.</returns>
    [<Extension>]
    static member GetCookieValue: ctx: HttpContext * key: string -> string option

    /// <summary>
    /// Retrieves the <see cref="System.String"/> value of a form parameter from the request.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="key">The name of the form parameter.</param>
    /// <returns>Returns Some string if the form parameter was set, otherwise returns None.</returns>
    [<Extension>]
    static member GetFormValue: ctx: HttpContext * key: string -> string option

    /// <summary>
    /// Reads the entire body of the <see cref="Microsoft.AspNetCore.Http.HttpRequest"/> asynchronously and returns it as a <see cref="System.String"/> value.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <returns>Returns the contents of the request body as a <see cref="System.Threading.Tasks.Task{System.String}"/>.</returns>
    [<Extension>]
    static member ReadBodyFromRequestAsync: ctx: HttpContext -> Threading.Tasks.Task<string>

    /// <summary>
    /// Reads the entire body of the <see cref="Microsoft.AspNetCore.Http.HttpRequest"/> asynchronously and returns it as a <see cref="System.String"/> value.
    /// This method buffers the response and makes subsequent reads possible.
    /// </summary>
    /// <returns>Returns the contents of the request body as a <see cref="System.Threading.Tasks.Task{System.String}"/>.</returns>
    [<Extension>]
    static member ReadBodyBufferedFromRequestAsync: ctx: HttpContext -> Threading.Tasks.Task<string>

    /// <summary>
    /// Uses the <see cref="Json.ISerializer"/> to deserializes the entire body of the <see cref="Microsoft.AspNetCore.Http.HttpRequest"/> asynchronously into an object of type 'T.
    /// </summary>
    /// <typeparam name="'T"></typeparam>
    /// <returns>Retruns a <see cref="System.Threading.Tasks.Task{T}"/></returns>
    [<Extension>]
    static member BindJsonAsync: ctx: HttpContext -> Threading.Tasks.Task<'T>

    /// <summary>
    /// Uses the <see cref="Xml.ISerializer"/> to deserializes the entire body of the <see cref="Microsoft.AspNetCore.Http.HttpRequest"/> asynchronously into an object of type 'T.
    /// </summary>
    /// <typeparam name="'T"></typeparam>
    /// <returns>Retruns a <see cref="System.Threading.Tasks.Task{T}"/></returns>
    [<Extension>]
    static member BindXmlAsync: ctx: HttpContext -> Threading.Tasks.Task<'T>

    /// <summary>
    /// Parses all input elements from an HTML form into an object of type 'T.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="cultureInfo">An optional <see cref="System.Globalization.CultureInfo"/> element to be used when parsing culture specific data such as float, DateTime or decimal values.</param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>Returns a <see cref="System.Threading.Tasks.Task{T}"/></returns>
    [<Extension>]
    static member BindFormAsync: ctx: HttpContext * ?cultureInfo: CultureInfo -> Threading.Tasks.Task<'T>

    /// <summary>
    /// Tries to parse all input elements from an HTML form into an object of type 'T.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="cultureInfo">An optional <see cref="System.Globalization.CultureInfo"/> element to be used when parsing culture specific data such as float, DateTime or decimal values.</param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>Returns an object 'T if model binding succeeded, otherwise a <see cref="System.String"/> message containing the specific model parsing error.</returns>
    [<Extension>]
    static member TryBindFormAsync:
        ctx: HttpContext * ?cultureInfo: CultureInfo -> Threading.Tasks.Task<Result<'T, string>>

    /// <summary>
    /// Parses all parameters of a request's query string into an object of type 'T.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="cultureInfo">An optional <see cref="System.Globalization.CultureInfo"/> element to be used when parsing culture specific data such as float, DateTime or decimal values.</param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>Returns an instance of type 'T</returns>
    [<Extension>]
    static member BindQueryString: ctx: HttpContext * ?cultureInfo: CultureInfo -> 'T

    /// <summary>
    /// Tries to parse all parameters of a request's query string into an object of type 'T.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="cultureInfo">An optional <see cref="System.Globalization.CultureInfo"/> element to be used when parsing culture specific data such as float, DateTime or decimal values.</param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>Returns an object 'T if model binding succeeded, otherwise a <see cref="System.String"/> message containing the specific model parsing error.</returns>
    [<Extension>]
    static member TryBindQueryString: ctx: HttpContext * ?cultureInfo: CultureInfo -> Result<'T, string>

    /// <summary>
    /// Parses the request body into an object of type 'T based on the request's Content-Type header.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="cultureInfo">An optional <see cref="System.Globalization.CultureInfo"/> element to be used when parsing culture specific data such as float, DateTime or decimal values.</param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>Returns a <see cref="System.Threading.Tasks.Task{T}"/></returns>
    [<Extension>]
    static member BindModelAsync: ctx: HttpContext * ?cultureInfo: CultureInfo -> Threading.Tasks.Task<'T>

    /// <summary>
    /// Writes a byte array to the body of the HTTP response and sets the HTTP Content-Length header accordingly.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="bytes">The byte array to be send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteBytesAsync: ctx: HttpContext * bytes: byte array -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Writes an UTF-8 encoded string to the body of the HTTP response and sets the HTTP Content-Length header accordingly.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="str">The string value to be send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteStringAsync: ctx: HttpContext * str: string -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Writes an UTF-8 encoded string to the body of the HTTP response and sets the HTTP `Content-Length` header accordingly, as well as the `Content-Type` header to `text/plain`.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="str">The string value to be send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteTextAsync: ctx: HttpContext * str: string -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Serializes an object to JSON and writes the output to the body of the HTTP response.
    /// It also sets the HTTP Content-Type header to application/json and sets the Content-Length header accordingly.
    /// The JSON serializer can be configured in the ASP.NET Core startup code by registering a custom class of type <see cref="Json.ISerializer"/>
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="dataObj">The object to be send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteJsonAsync: ctx: HttpContext * dataObj: 'T -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Serializes an object to JSON and writes the output to the body of the HTTP response using chunked transfer encoding.
    /// It also sets the HTTP Content-Type header to application/json and sets the Transfer-Encoding header to chunked.
    /// The JSON serializer can be configured in the ASP.NET Core startup code by registering a custom class of type <see cref="Json.ISerializer"/>.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="dataObj">The object to be send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteJsonChunkedAsync: ctx: HttpContext * dataObj: 'T -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Serializes an object to XML and writes the output to the body of the HTTP response.
    /// It also sets the HTTP Content-Type header to application/xml and sets the Content-Length header accordingly.
    /// The JSON serializer can be configured in the ASP.NET Core startup code by registering a custom class of type <see cref="Xml.ISerializer"/>.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="dataObj">The object to be send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteXmlAsync: ctx: HttpContext * dataObj: obj -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Reads a HTML file from disk and writes its contents to the body of the HTTP response.
    /// It also sets the HTTP header Content-Type to text/html and sets the Content-Length header accordingly.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="filePath">A relative or absolute file path to the HTML file.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteHtmlFileAsync: ctx: HttpContext * filePath: string -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// Writes a HTML string to the body of the HTTP response.
    /// It also sets the HTTP header Content-Type to text/html and sets the Content-Length header accordingly.
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="html">The HTML string to be send back to the client.</param>
    /// <returns>Task of Some HttpContext after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteHtmlStringAsync: ctx: HttpContext * html: string -> Threading.Tasks.Task<HttpContext option>

    /// <summary>
    /// <para>Compiles a `Giraffe.GiraffeViewEngine.XmlNode` object to a HTML view and writes the output to the body of the HTTP response.</para>
    /// <para>It also sets the HTTP header `Content-Type` to `text/html` and sets the `Content-Length` header accordingly.</para>
    /// </summary>
    /// <param name="ctx">The current http context object.</param>
    /// <param name="htmlView">An `XmlNode` object to be send back to the client and which represents a valid HTML view.</param>
    /// <returns>Task of `Some HttpContext` after writing to the body of the response.</returns>
    [<Extension>]
    static member WriteHtmlViewAsync: ctx: HttpContext * htmlView: XmlNode -> Threading.Tasks.Task<HttpContext option>
