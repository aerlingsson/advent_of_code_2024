module Day1

open System

let parse (s: string) =
  s.Split "\r\n"
  |> Array.filter ((<>) String.Empty)
  |> (Array.map (fun x -> x.Split "   " |> Array.map int))

let part1 (input: string) =
  input
  |> parse
  |> Array.fold (fun (xs, ys) row -> row[0] :: xs, row[1] :: ys) ([], [])
  |> fun (xs, ys) -> (List.sort xs, List.sort ys)
  ||> List.zip
  |> List.map (fun (x, y) -> abs(x - y))
  |> List.sum
