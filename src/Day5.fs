module Day5

open System
open System.Collections.Generic

let private parse (s: string) =
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

let private pageCorrectlyOrdered (depsPerPage: IDictionary<int, int array>) update printed page =
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

let private updateCorrectlyOrdered depsPerPage update =
  update
  |> Array.fold
    (fun (printed, isUpdateOk) page ->
      if isUpdateOk then
        (page :: printed, pageCorrectlyOrdered depsPerPage update printed page)
      else
        (printed, isUpdateOk))
    ([], true)

let part1 (input: string) =
  let depsPerPage, updates = input |> parse

  updates
  |> Array.choose (fun update ->
    let printed, isOk = updateCorrectlyOrdered depsPerPage update
    if isOk then Some printed[Seq.length printed / 2] else None)
  |> Array.sum

let private dfsTopologicalSort (graph: IDictionary<int, int array>) =
  let rec visit (visited, stack) node =
    match Set.contains node visited with
    | true -> (visited, stack)
    | false ->
      let visited = Set.add node visited
      let deps = graph[node] |> Array.toList
      let visited, stack = deps |> List.fold visit (visited, stack)

      (visited, node :: stack)

  graph.Keys |> Seq.toList |> List.fold visit (Set.empty, []) |> snd |> List.rev

let part2 (input: string) =
  let depsPerPage, updates = input |> parse

  let (ordered: int array array) =
    updates
    |> Array.filter (fun update -> update |> updateCorrectlyOrdered depsPerPage |> snd |> not)
    |> Array.map (fun update ->
      update
      |> Array.map (fun page ->
        let deps =
          match depsPerPage.TryGetValue page with
          | true, deps -> deps
          | _ -> [||]

        page, deps |> Array.filter (fun x -> Array.contains x update))
      |> dict
      |> dfsTopologicalSort
      |> List.toArray)

  ordered |> Array.sumBy (fun update -> update[Seq.length update / 2])
