namespace Recycling.Repository

open Recycling.Domain

type SimpleRepository() =

    interface IRecyclingRepository with 
        member this.getMaterialCategories() =
            [
                MaterialCategory("PET-1", ["PETE"], Recycling)
                // "HDPE"
                // "PVC 🔴"
                // "PP"
                // "PS"
                // "PC"
                // "7 OTHER 🟠"
                // "PAP"
                // "C/PAP"
                // "FE"
                // "ALU"
                // "GL"
            ]