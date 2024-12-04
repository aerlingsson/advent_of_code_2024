module Day4

open System.Collections.Generic

let inline (.+) (a: int * int) (b: int * int) = ((fst a + fst b), (snd a + snd b))

let private parse (s: string) =
  s.Split "\n"
  |> Array.indexed
  |> Array.collect (fun (rowIdx, line) ->
    line.ToCharArray()
    |> Array.indexed
    |> Array.filter (fun (_, char) -> Array.contains char [| 'X'; 'M'; 'A'; 'S' |])
    |> Array.map (fun (colIdx, char) -> (rowIdx, colIdx), char))
  |> dict

let isCharMatch (charPositions: IDictionary<int * int, char>) offset key char =
  match charPositions.TryGetValue(offset .+ key) with
  | true, c when c = char -> true
  | _ -> false

let part1 (input: string) =
  let validOffsets =
    [ (-1, 0), (-2, 0), (-3, 0) // up
      (1, 0), (2, 0), (3, 0) // down
      (0, -1), (0, -2), (0, -3) // left
      (0, 1), (0, 2), (0, 3) // right
      (-1, 1), (-2, 2), (-3, 3) // up + right
      (-1, -1), (-2, -2), (-3, -3) // up + left
      (1, -1), (2, -2), (3, -3) // down + left
      (1, 1), (2, 2), (3, 3) ] // down + right

  let charPositions = input |> parse
  let isCharMatch = isCharMatch charPositions

  charPositions
  |> Seq.filter (fun x -> x.Value = 'X')
  |> Seq.fold
    (fun (count: int) x ->
      let matches =
        validOffsets
        |> List.filter (fun (m, a, s) -> isCharMatch m x.Key 'M' && isCharMatch a x.Key 'A' && isCharMatch s x.Key 'S')

      count + Seq.length matches)
    0

let part2 (input: string) =
  let offsetPairs = [ ((-1, -1), (1, 1)), ((1, -1), (-1, 1)) ]

  let charPositions = input |> parse
  let isCharMatch = isCharMatch charPositions

  let isPairMatch key (a, b) =
    (isCharMatch a key 'M' && isCharMatch b key 'S')
    || (isCharMatch b key 'M' && isCharMatch a key 'S')

  charPositions
  |> Seq.filter (fun x -> x.Value = 'A')
  |> Seq.fold
    (fun (count: int) x ->
      if
        offsetPairs
        |> List.exists (fun (pair1, pair2) -> isPairMatch x.Key pair1 && isPairMatch x.Key pair2)
      then
        count + 1
      else
        count)
    0
