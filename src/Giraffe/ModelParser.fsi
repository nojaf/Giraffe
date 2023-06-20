namespace Giraffe

/// <summary>
/// Module for parsing models from a generic data set.
/// </summary>
module ModelParser =
    open System
    open System.Globalization
    open System.Reflection
    open System.Collections.Generic
    open Microsoft.Extensions.Primitives

    type private Type with

        member IsGeneric: unit -> bool
        member IsFSharpList: unit -> bool
        member IsFSharpOption: unit -> bool
        member GetGenericType: unit -> Type
        member MakeNoneCase: unit -> obj
        member MakeSomeCase: value: obj -> obj

    /// Returns a value (the None union case) if the type is `Option<'T>` otherwise `None`.
    val getValueForMissingProperty: t: Type -> obj option

    val getValueForComplexType:
        cultureInfo: CultureInfo option ->
        data: IDictionary<string, StringValues> ->
        strict: bool ->
        prop: PropertyInfo ->
            obj option

    val getValueForArrayOfGenericType:
        cultureInfo: CultureInfo option ->
        data: IDictionary<string, StringValues> ->
        strict: bool ->
        prop: PropertyInfo ->
            obj option

    /// <summary>
    /// Tries to create an instance of type 'T from a given set of data.
    /// It will try to match each property of 'T with a key from the data dictionary and parse the associated value to the value of 'T's property.
    /// </summary>
    /// <param name="culture">An optional <see cref="System.Globalization.CultureInfo"/> element to be used when parsing culture specific data such as float, DateTime or decimal values.</param>
    /// <param name="data">A key-value dictionary of values for each property of type 'T. Only optional properties can be omitted from the dictionary.</param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>If all properties were able to successfully parse then Some 'T will be returned, otherwise None.</returns>
    val tryParse: culture: CultureInfo option -> data: IDictionary<string, StringValues> -> Result<'T, string>
    /// <summary>
    /// Create an instance of type 'T from a given set of data.
    /// </summary>
    /// <param name="culture">An optional <see cref="System.Globalization.CultureInfo"/> element to be used when parsing culture specific data such as float, DateTime or decimal values.</param>
    /// <param name="data">A key-value dictionary of values for each property of type 'T. Only optional properties can be omitted from the dictionary.</param>
    /// <typeparam name="'T"></typeparam>
    /// <returns>An instance of type 'T. Not all properties might be set. Null checks are required for reference types.</returns>
    val parse: culture: CultureInfo option -> data: IDictionary<string, StringValues> -> 'T
