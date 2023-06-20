namespace Giraffe

[<RequireQualifiedAccess>]
module Xml =
    /// <summary>
    /// Interface defining XML serialization methods.
    /// Use this interface to customize XML serialization in Giraffe.
    /// </summary>
    [<AllowNullLiteral>]
    type ISerializer =
        abstract member Serialize: obj -> byte array
        abstract member Deserialize<'T> : string -> 'T

[<RequireQualifiedAccess>]
module SystemXml =
    open Microsoft.IO
    open System.Xml

    /// <summary>
    /// Default XML serializer in Giraffe.
    /// Serializes objects to UTF8 encoded indented XML code.
    /// </summary>
    type Serializer =
        new: settings: XmlWriterSettings * rmsManager: RecyclableMemoryStreamManager -> Serializer
        new: settings: XmlWriterSettings -> Serializer
        static member DefaultSettings: XmlWriterSettings
        interface Xml.ISerializer
