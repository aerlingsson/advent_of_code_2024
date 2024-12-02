module Day2Test

open Xunit

[<Fact>]
let part1 () =
  let ex = Day2.part1 Day2Input.exampleInput
  Assert.Equal(2, ex)
  let real = Day2.part1 Day2Input.realInput
  printfn "%A" real

[<Fact>]
let part2 () =
  let ex = Day2.part2 Day2Input.exampleInput
  Assert.Equal(4, ex)
  printfn "%A" (Day2.part2 Day2Input.realInput)
