module Input

open System
open System.IO
open FSharp.Data
open Customer
open Extensions

type InputProvider = JsonProvider<"./ExampleInput.json">

let private readFile (path: string) : Result<string, string> =
    if File.Exists path then
        try
            use reader = File.OpenText path
            Ok <| reader.ReadToEnd()
        with ex ->
            Error ex.Message
    else
        Error $"file ${path} does not exist"

let loadInput (path: string) : Result<Customer list, string> =
    let toCustomer (json: InputProvider.Root) =
        let freq =
            match json.Frequency.Type with
            | "monthly" ->
                Option.bind
                    (fun day ->
                        if day <= 28 then
                            Some <| UpdateFrequency.Monthly day
                        else
                            None)
                    json.Frequency.Update.Number
            | "weekly" ->
                Option.map
                    (fun arr ->
                        arr
                        |> Array.choose DayOfWeek.fromInt
                        |> Array.toList
                        |> UpdateFrequency.Weekly)
                    json.Frequency.Update.Array
            | "daily" ->  Some UpdateFrequency.Daily 
            | "never" -> Some UpdateFrequency.Never
            | _ -> None
            
        Option.map
            (fun f -> { name = json.Name; freq = f })
            freq
    
    readFile path
    |> Result.map
        (  InputProvider.Parse
        >> Array.choose toCustomer
        >> Array.toList
        )
