namespace Recycling.Domain

type RecyclingClass =
    | Recycling
    | Burning
    | ThrowingAway

type MaterialCategory(name: string, aliases: string list, recyclingClasses : RecyclingClass list) =
    member this.Name = name
    member this.Aliases = aliases
    member this.Classes = recyclingClasses

type Material(name: string, recyclingClass: RecyclingClass, notes: string list) =
    member this.Name = name
    member this.Class = recyclingClass
    member this.Notes = notes

type IRecyclingRepository =
     abstract member getMaterialCategories : unit -> MaterialCategory list
     abstract member getMaterialInfo : string -> Material list
   
