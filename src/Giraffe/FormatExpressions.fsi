module Giraffe.FormatExpressions

open FSharp.Core

type MatchMode =
    | Exact // Will try to match entire string from start to end.
    | StartsWith // Will try to match a substring. Subject string should start with test case.
    | EndsWith // Will try to match a substring. Subject string should end with test case.
    | Contains // Will try to match a substring. Subject string should contain test case.

type MatchOptions =
    { IgnoreCase: bool
      MatchMode: MatchMode }

    static member Exact: MatchOptions
    static member IgnoreCaseExact: MatchOptions

/// <summary>
/// Tries to parse an input string based on a given format string and return a tuple of all parsed arguments.
/// </summary>
/// <param name="format">The format string which shall be used for parsing.</param>
/// <param name="options">The options record with specifications on how the matching should behave.</param>
/// <param name="input">The input string from which the parsed arguments shall be extracted.</param>
/// <returns>Matched value as an option of 'T</returns>
val tryMatchInput: format: PrintfFormat<'a, 'b, 'c, 'd, 'T> -> options: MatchOptions -> input: string -> 'T option
/// <summary>
/// Tries to parse an input string based on a given format string and return a tuple of all parsed arguments.
/// </summary>
/// <param name="format">The format string which shall be used for parsing.</param>
/// <param name="ignoreCase">The flag to make matching case insensitive.</param>
/// <param name="input">The input string from which the parsed arguments shall be extracted.</param>
/// <returns>Matched value as an option of 'T</returns>
val tryMatchInputExact: format: PrintfFormat<'a, 'b, 'c, 'd, 'T> -> ignoreCase: bool -> input: string -> 'T option
/// **Description**
///
/// Validates if a given format string can be matched with a given tuple.
///
/// **Parameters**
///
/// `format`: The format string which shall be used for parsing.
///
/// **Output**
///
/// Returns `unit` if validation was successful otherwise will throw an `Exception`.
/// <summary>
/// Validates if a given format string can be matched with a given tuple.
/// </summary>
/// <param name="format">The format string which shall be used for parsing.</param>
/// <returns>Returns <see cref="Microsoft.FSharp.Core.Unit"/> if validation was successful otherwise will throw an `Exception`.</returns>
val validateFormat: format: PrintfFormat<'a, 'b, 'c, 'd, 'T> -> unit
