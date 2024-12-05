module Day5

open System

let parse (s: string) =
  let parts = s.Split "\r\n\r\n"

  let depsPerPage =
    parts[0].Split "\r\n"
    |> Array.filter ((<>) String.Empty)
    |> Array.map (fun x -> x.Split "|" |> Array.map int |> (fun pages -> (pages[0], pages[1])))
    |> Array.groupBy snd
    |> Array.map (fun (page, rules) -> page, rules |> Array.map fst)
    |> dict

  let updates =
    parts[1].Split "\r\n"
    |> Array.filter ((<>) String.Empty)
    |> Array.map (fun x -> x.Split "," |> Array.map int)

  depsPerPage, updates

let part1 (input: string) =
  let depsPerPage, updates = input |> parse

  let correctlyOrdered update printed page =
    //printf "printed: %A, page: %A" printed page
    match depsPerPage.TryGetValue page with
    | false, _ -> true
    | true, reqs ->
      reqs
      |> Array.forall (fun x ->
        if update |> Array.contains x then
          List.contains x printed
        else
          true)

  updates
  |> Array.choose (fun update ->
    let printed, isOk =
      update
      |> Array.fold
        (fun (printed, isUpdateOk) page ->
          if isUpdateOk then
            (page :: printed, correctlyOrdered update printed page)
          else
            (printed, isUpdateOk))
        ([], true)

    if isOk then Some printed[Seq.length printed / 2] else None)
  |> Array.sum
