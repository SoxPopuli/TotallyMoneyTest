open Input
open System
open Extensions
open Customer

//Prints date in ISO 8601 format with day of week
let dateToString (dt: DateTime) =
    sprintf $"%9s{dt.DayOfWeek.ToString()} {dt.Year}-%02d{dt.Month}-%02d{dt.Day}"

[<EntryPoint>]
let main args =
    if args.Length = 0 then
        failwith "Error: no input."

    let input = args[0]
    let input: Customer list = loadInput input |> Result.expect "failed to load input"

    //Generate a sequence of the next 90 days
    let today = DateTime.Today
    let days = seq { for i in 1..90 -> today.AddDays i }

    for day in days do
        //Get list of customers who want to be updated
        //on a specific day
        let updates =
            List.filter (Customer.shouldUpdate day) input
            |> List.map (fun u -> u.name)
            |> List.reduce (fun acc u -> $"{acc}, {u}")

        printfn $"{dateToString day}: %s{updates}"

    0
