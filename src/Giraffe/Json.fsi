namespace Giraffe

[<RequireQualifiedAccess>]
module Json =
    open System.IO
    open System.Threading.Tasks

    /// <summary>
    /// Interface defining JSON serialization methods.
    /// Use this interface to customize JSON serialization in Giraffe.
    /// </summary>
    [<AllowNullLiteral>]
    type ISerializer =
        abstract member SerializeToString<'T> : 'T -> string
        abstract member SerializeToBytes<'T> : 'T -> byte array
        abstract member SerializeToStreamAsync<'T> : 'T -> Stream -> Task

        abstract member Deserialize<'T> : string -> 'T
        abstract member Deserialize<'T> : byte[] -> 'T
        abstract member DeserializeAsync<'T> : Stream -> Task<'T>

[<RequireQualifiedAccess>]
module NewtonsoftJson =
    open System.Text
    open Microsoft.IO
    open Newtonsoft.Json

    /// <summary>
    /// Default JSON serializer in Giraffe.
    /// Serializes objects to camel cased JSON code.
    /// </summary>
    type Serializer =
        new: settings: JsonSerializerSettings * rmsManager: RecyclableMemoryStreamManager -> Serializer
        new: settings: JsonSerializerSettings -> Serializer
        static member DefaultSettings: JsonSerializerSettings
        interface Json.ISerializer

[<RequireQualifiedAccess>]
module Utf8Json =
    open System.Text
    open Utf8Json

    /// <summary>
    /// <see cref="Utf8Json.Serializer" /> is an alternative serializer with
    /// great performance and supports true chunked transfer encoding.
    ///
    /// It uses Utf8Json as the underlying JSON serializer to (de-)serialize
    /// JSON content. Utf8Json is currently
    /// the fastest JSON serializer for .NET.
    /// </summary>
    /// <remarks>https://github.com/neuecc/Utf8Json</remarks>
    type Serializer =
        new: resolver: IJsonFormatterResolver -> Serializer
        static member DefaultResolver: IJsonFormatterResolver
        interface Json.ISerializer

[<RequireQualifiedAccess>]
module SystemTextJson =
    open System.Text
    open System.Text.Json

    /// <summary>
    /// <see cref="SystemTextJson.Serializer" /> is an alternaive <see cref="Json.ISerializer"/> in Giraffe.
    ///
    /// It uses <see cref="System.Text.Json"/> as the underlying JSON serializer to (de-)serialize
    /// JSON content.
    /// For support of F# unions and records, look at https://github.com/Tarmil/FSharp.SystemTextJson
    /// which plugs into this serializer.
    /// </summary>
    type Serializer =
        new: options: JsonSerializerOptions -> Serializer
        static member DefaultOptions: JsonSerializerOptions
        interface Json.ISerializer
