module Day3Test

open Xunit

[<Fact>]
let part1 () =
  let ex = Day3.part1 Day3Input.exampleInput
  Assert.Equal(161, ex)
  printfn "%A" (Day3.part1 Day3Input.realInput)

//[<Fact>]
//let part2 () =
//  let ex = Day3.part2 Day3Input.exampleInput
//  Assert.Equal(4, ex)
//  printfn "%A" (Day3.part2 Day3Input.realInput)
