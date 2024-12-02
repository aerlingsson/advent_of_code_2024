module Day2

open System

let private parse (s: string) =
  s.Split "\r\n"
  |> Array.filter ((<>) String.Empty)
  |> Array.map (fun x -> x.Split " " |> Array.map int |> Array.toList)
  |> Array.toList

type Change =
  | Increase
  | Decrease

let validReport levels =
  let validPair (pair: int list) =
    let a, b = pair[0], pair[1]
    let validDiff = abs (a - b) >= 1 && abs (a - b) <= 3

    if a < b && validDiff then Some Increase
    else if a > b && validDiff then Some Decrease
    else None

  let allValid (prev: Change option) (curr: int list) : Change option =
    match validPair curr, prev with
    | Some Increase, Some Increase -> Some Increase
    | Some Decrease, Some Decrease -> Some Decrease
    | _ -> None

  match levels |> List.windowed 2 with
  | firstPair :: rest -> rest |> List.fold allValid (validPair firstPair)
  | _ -> failwith "Doesn't happen"

let part1 (input: string) =
  input |> parse |> List.choose validReport |> _.Length

let part2 (input: string) =
  let ok, errors = input |> parse |> List.partition (validReport >> Option.isSome)

  let fixedErrors =
    errors
    |> List.map (fun levels -> [0..levels.Length-1] |> List.map (fun idx -> levels |> List.removeAt idx |> validReport))
    |> List.filter (List.exists Option.isSome)

  ok.Length + fixedErrors.Length
