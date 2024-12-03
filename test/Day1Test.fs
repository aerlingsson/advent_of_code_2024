module Day1Test

open Xunit

[<Fact>]
let part1 () =
  let ex = Day1.part1 Day1Input.exampleInput
  Assert.Equal(11, ex)
  printfn "%A" (Day1.part1 Day1Input.realInput)

[<Fact>]
let part2 () =
  let ex = Day1.part2 Day1Input.exampleInput
  Assert.Equal(31, ex)
  printfn "%A" (Day1.part2 Day1Input.realInput)
