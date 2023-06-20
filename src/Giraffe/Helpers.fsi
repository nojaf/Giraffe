namespace Giraffe

[<AutoOpen>]
module Helpers =
    open System
    open Microsoft.IO

    /// <summary>Default single RecyclableMemoryStreamManager.</summary>
    val recyclableMemoryStreamManager: Lazy<RecyclableMemoryStreamManager>
    /// <summary>
    /// Checks if an object is not null.
    /// </summary>
    /// <param name="x">The object to validate against `null`.</param>
    /// <returns>Returns true if the object is not null otherwise false.</returns>
    val inline isNotNull: x: 'a -> bool when 'a: null
    /// <summary>
    /// Converts a string into a string option where null or an empty string will be converted to None and everything else to Some string.
    /// </summary>
    /// <param name="str">The string value to be converted into an option of string.</param>
    /// <returns>Returns None if the string was null or empty otherwise Some string.</returns>
    val inline strOption: str: string -> string option
    /// <summary>
    /// Reads a file asynchronously from the file system.
    /// </summary>
    /// <param name="filePath">The absolute path of the file.</param>
    /// <returns>Returns the string contents of the file wrapped in a Task.</returns>
    val readFileAsStringAsync: filePath: string -> Threading.Tasks.Task<string>
