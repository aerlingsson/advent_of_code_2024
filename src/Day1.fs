module Day1

open System

let private parse (s: string) =
  s.Split "\r\n"
  |> Array.filter ((<>) String.Empty)
  |> (Array.map (fun x -> x.Split "   " |> Array.map int))

let part1 (input: string) =
  input
  |> parse
  |> Array.fold (fun (xs, ys) row -> row[0] :: xs, row[1] :: ys) ([], [])
  |> fun (xs, ys) -> (List.sort xs, List.sort ys)
  ||> List.zip
  |> List.map (fun (x, y) -> abs (x - y))
  |> List.sum

let part2 (input: string) =
  let parsed = input |> parse

  let freqs idxFn xs =
    xs
    |> Array.map idxFn
    |> Array.groupBy id
    |> Array.map (fun (x, xs) -> x, xs.Length)
    |> dict

  let xFreqs = freqs Array.head parsed
  let yFreqs = freqs (Array.item 1) parsed

  xFreqs.Keys
  |> Seq.toArray
  |> Array.map (fun x ->
    match yFreqs.TryGetValue x with
    | true, yFreq -> x * xFreqs[x] * yFreq
    | _ -> 0)
  |> Array.sum