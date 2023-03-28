module Customer

open System

type UpdateFrequency =
    | Monthly of int
    | Weekly of DayOfWeek list
    | Daily
    | Never

type Customer =
    { name: string
      freq: UpdateFrequency
    }
    
    static member shouldUpdate (date: DateTime) (customer: Customer) =
        match customer.freq with
        | Monthly day -> date.Day = day
        | Weekly days -> List.contains date.DayOfWeek days
        | Daily -> true
        | Never -> false