module Day4Test

open Xunit

[<Fact>]
let part1 () =
  let ex = Day4.part1 Day4Input.exampleInput
  Assert.Equal(18, ex)
  printfn "%A" (Day4.part1 Day4Input.realInput)

[<Fact>]
let part2 () =
  let ex = Day4.part2 Day4Input.exampleInput
  Assert.Equal(9, ex)
  printfn "%A" (Day4.part2 Day4Input.realInput)
