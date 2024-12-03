module Day3

open System
open System.Text.RegularExpressions

let private regex = Regex "mul\((\d+),(\d+)\)"

let private parse (s: string) =
  regex.Matches s
  |> Seq.toList
  |> List.map (fun x -> int x.Groups[1].Value, int x.Groups[2].Value)

let part1 (input: string) =
  input |> parse |> List.sumBy (fun (a, b) -> a * b)
