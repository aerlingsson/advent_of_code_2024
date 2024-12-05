module Day5Test

open Xunit

[<Fact>]
let part1 () =
  let ex = Day5.part1 Day5Input.exampleInput
  Assert.Equal(143, ex)
  printfn "%A" (Day5.part1 Day5Input.realInput)

//[<Fact>]
//let part2 () =
//  let ex = Day5.part2 Day5Input.exampleInput
//  Assert.Equal(9, ex)
//  printfn "%A" (Day5.part2 Day5Input.realInput)
