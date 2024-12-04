module Day4

let inline (.+) (a: int * int) (b: int * int) = ((fst a + fst b), (snd a + snd b))

let private validOffsets =
  [ (-1, 0), (-2, 0), (-3, 0) // up
    (1, 0), (2, 0), (3, 0) // down
    (0, -1), (0, -2), (0, -3) // left
    (0, 1), (0, 2), (0, 3) // right
    (-1, 1), (-2, 2), (-3, 3) // up + right
    (-1, -1), (-2, -2), (-3, -3) // up + left
    (1, -1), (2, -2), (3, -3) // down + left
    (1, 1), (2, 2), (3, 3) ] // down + right

let private parse (s: string) =
  s.Split "\n"
  |> Array.indexed
  |> Array.collect (fun (rowIdx, line) ->
    line.ToCharArray()
    |> Array.indexed
    |> Array.filter (fun (_, char) -> Array.contains char [| 'X'; 'M'; 'A'; 'S' |])
    |> Array.map (fun (colIdx, char) -> (rowIdx, colIdx), char))
  |> dict

let part1 (input: string) =
  let charPositions = input |> parse

  let isMatch offset key char =
    match charPositions.TryGetValue(offset .+ key) with
    | true, c when c = char -> true
    | _ -> false

  charPositions
  |> Seq.filter (fun x -> x.Value = 'X')
  |> Seq.fold
    (fun (count: int) x ->
      let matches =
        validOffsets
        |> Seq.filter (fun (m, a, s) -> isMatch m x.Key 'M' && isMatch a x.Key 'A' && isMatch s x.Key 'S')

      count + Seq.length matches)
    0
