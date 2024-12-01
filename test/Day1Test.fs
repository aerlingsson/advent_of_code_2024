module Day1Test

open Xunit

[<Fact>]
let part1 () =
  let example = Day1.part1 Day1Input.exampleInput
  Assert.Equal(11, example)
  let real = Day1.part1 Day1Input.realInput
  printfn "%A" real
