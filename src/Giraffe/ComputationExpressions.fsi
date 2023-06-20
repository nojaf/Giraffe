/// <summary>
/// A collection of F# computation expressions:
///
/// `opt {}`: Enables control flow and binding of Option{T} objects
/// `res {}`: Enables control flow and binding of Result{T, TError} objects
/// </summary>
module Giraffe.ComputationExpressions

/// <summary>
/// Enables control flow and binding of `Option{T}` objects
/// </summary>
type OptionBuilder =
    new: unit -> OptionBuilder
    member Bind: v: 'd option * f: ('d -> 'e option) -> 'e option
    member Return: v: 'c -> 'c option
    member ReturnFrom: v: 'b -> 'b
    member Zero: unit -> 'a option

val opt: OptionBuilder

/// <summary>
/// Enables control flow and binding of <see cref="Microsoft.FSharp.Core.Result{T, TError}" /> objects
/// </summary>
type ResultBuilder =
    new: unit -> ResultBuilder
    member Bind: v: Result<'c, 'd> * f: ('c -> Result<'e, 'd>) -> Result<'e, 'd>
    member Return: v: 'a -> Result<'a, 'b>

val res: ResultBuilder
