module Day3Test

open Xunit

[<Fact>]
let part1 () =
  let ex = Day3.part1 Day3Input.exampleInputPart1
  Assert.Equal(161, ex)
  printfn "%A" (Day3.part1 Day3Input.realInput)

[<Fact>]
let part2 () =
  let ex = Day3.part2 Day3Input.exampleInputPart2
  Assert.Equal(48, ex)
  printfn "%A" (Day3.part2 Day3Input.realInput)
