namespace Recycling.Domain

type RecyclingClass =
    | Recycling
    | Burning
    | ThrowingAway

type MaterialCategory(name: string, aliases: string list, recyclingClass : RecyclingClass) =
    member this.Name = name
    member this.Aliases = aliases
    member this.Class = recyclingClass

type IRecyclingRepository =
     abstract member getMaterialCategories : unit -> MaterialCategory list
