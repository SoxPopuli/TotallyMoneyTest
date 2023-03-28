module Extensions

open System

type DayOfWeek with
    static member fromInt i =
        let values: DayOfWeek array =
            [| DayOfWeek.Monday
               DayOfWeek.Tuesday
               DayOfWeek.Wednesday
               DayOfWeek.Thursday
               DayOfWeek.Friday
               DayOfWeek.Saturday
               DayOfWeek.Sunday |]

        if i < values.Length then Some <| values[i] else None


type Option<'T> with
    static member expect (msg: string) (opt: Option<'T>) =
        match opt with
        | Some s -> s
        | None -> failwith msg
        
type Result<'O, 'E> with
    static member expect (msg: string) (res: Result<'O, 'E>) =
        match res with
        | Ok o -> o
        | Error _ -> failwith msg