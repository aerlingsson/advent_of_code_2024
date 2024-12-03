module Day3

open System
open System.Text.RegularExpressions

type Command =
  | Mul of int * int
  | Do
  | Dont

// Matches "do" or "don't" or "mul(1, 2)" and captures them as ["do"], ["don't"] and ["1"; "2"]
let private regex = Regex $"(do\(\))|(don't\(\))|mul\((\d+),(\d+)\)"

let private parse (s: string) =
  regex.Matches s
  |> Seq.toList
  |> List.map (fun x ->
    let groups =
      x.Groups
      |> Seq.tail
      |> Seq.filter (fun g -> g.Value <> String.Empty)
      |> Seq.toList

    match groups with
    | [ a; b ] -> Mul(int a.Value, int b.Value)
    | [ a ] when a.Value = "do()" -> Do
    | [ a ] when a.Value = "don't()" -> Dont
    | _ -> failwith "Expected only one or two captured groups")

let part1 (input: string) =
  input
  |> parse
  |> List.sumBy (fun x ->
    match x with
    | Mul(a, b) -> a * b
    | _ -> 0)

let part2 (input: string) =
  input
  |> parse
  |> List.fold
    (fun (active, sum) command ->
      match command with
      | Mul(a, b) -> if active then (active, (a * b) + sum) else (active, sum)
      | Do -> (true, sum)
      | Dont -> (false, sum))
    (true, 0)
  |> snd
