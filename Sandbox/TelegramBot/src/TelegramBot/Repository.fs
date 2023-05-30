namespace Recycling.Repository

open Recycling.Domain

type SimpleRepository() =

    interface IRecyclingRepository with 
        member this.getMaterialCategories() =
            [
                MaterialCategory("PET-1", ["1"; "ПЕТ"; "ПЕТФ"; "PET"; "PETE"], [Recycling; Burning])
                MaterialCategory("HDPE", ["2"; "PE-HD"], [Recycling])
                MaterialCategory("PVC", ["3"; "ПВХ"], [ThrowingAway])
                MaterialCategory("LDPE", ["4"; "PE-LD"], [Recycling])
                MaterialCategory("C/LDPE", [], [Burning])
                MaterialCategory("PP", ["5"], [Recycling; Burning])
                MaterialCategory("PS", ["6"; "ПС"], [Recycling; Burning])
                MaterialCategory("PC", ["7"; "ПС"], [Recycling; Burning])
                MaterialCategory("PAP", ["20-27"], [Recycling; Burning])
                MaterialCategory("C/PAP", ["81-84"; "Tetra Pak"; "Pure Pack"; "Elo Pak"], [Recycling])
                MaterialCategory("FE", ["40"], [Recycling])
                MaterialCategory("ALU", ["41"], [Recycling])
                MaterialCategory("GL", ["70-79"], [Recycling; ThrowingAway]) 
            ]
        member this.getMaterialInfo(name: string): Material list = 
            match name with
            | "PET-1" -> 
            [
                Material("Пляшки прозора з-під напоїв без відтінку", Recycling, [])
                Material("Пляшки прозора з-під напоїв з блакитним відтінком", Recycling, [])
                Material("Пляшки прозора з-під напоїв з зеленим відтінком", Recycling, [])
                Material("Пляшки прозора з-під напоїв з коричневим відтінком", Recycling, [])
                Material("Пляшки прозора з-під напоїв з жовтим відтінком", Recycling, [])
                Material("Пляшки з-під напоїв чорна", Recycling, [])
                Material("Пляшки білі з-під молочки", Recycling, [])
                Material("Пляшки з-під олії прозорі", Recycling, [])
                Material("Пляшки з-під оцту та соєвого соусу, тільки якщо етикетка легко знімається (сортуються в один бак з пляшками з-під олії)", Recycling, [])
                Material("Прозорі й кольорові пляшки з-під засобів побутової хімії (кришечки, ковпачки, дозатори й наліпки можна лишати)", Recycling, [])
                Material("Непрозорі пляшки з-під молочних та інших виробів, темно-синього та світло-коричневого кольору", Burning, [])
                Material("Усі інші вироби з маркуванням 'PET-1': (одноразовий посуд, блістери, кришки тощо)", Burning, [])
            ]
            | _ -> [Material("Unknown category", ThrowingAway, [])]
             