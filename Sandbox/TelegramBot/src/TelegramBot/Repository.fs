namespace Recycling.Repository

open Recycling.Domain

type SimpleRepository() =
    
    interface IRecyclingRepository with 
        member this.getMaterialCategories() =
            [
                // TODO: we should calculate recycling classes based on material rules
                MaterialCategory("PET-1", ["♻️1"; "Поліетилентерефталат"; "ПЕТ"; "ПЕТФ"; "PET"; "PETE"], [Recycling; Burning])
                MaterialCategory("HDPE", ["♻️2"; "Поліетилен високої щільності"; "Поліетилен низького тиску"; "PE-HD"], [Recycling])
                MaterialCategory("PVC", ["♻️3"; "Полівінілхлорид"; "ПВХ"], [ThrowingAway])
                MaterialCategory("LDPE", ["♻️4"; "Поліетилен низької щільності"; "Поліетилен високого тиску"; "PE-LD"], [Recycling])
                MaterialCategory("C/LDPE", [], [Burning])
                MaterialCategory("PP", ["♻️5"; "Поліпропілен"; "ПП"], [Recycling; Burning])
                MaterialCategory("PS", ["♻️6"; "Полістирол"; "ПС"], [Recycling; Burning])
                MaterialCategory("PC", ["♻️7"; "Полікарбонат"], [Recycling; Burning])
                MaterialCategory("PAP", ["Папір"; "♻️20-27"], [Recycling; Burning])
                MaterialCategory("C/PAP", ["Комбіноване паковання"; "81-84"; "Tetra Pak"; "Pure Pack"; "Elo Pak"], [Recycling])
                MaterialCategory("FE", ["♻️40"], [Recycling])
                MaterialCategory("ALU", ["♻️41"], [Recycling])
                MaterialCategory("GL", ["♻️70-79"; "Скло"], [Recycling; ThrowingAway]) 
                MaterialCategory("Other", [], [Recycling; Burning; ThrowingAway])
            ]
        
        member this.getMaterialInfo(name: string): Material list = 
            match name.ToUpper() with
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
            | "HDPE" -> 
                [
                    Material("Пляшки та флакони з-під засобів побутової хімії та молочних напоїв", Recycling, ["Обов’язково знімайте етикетки та наліпки"])
                    Material("Кришечки з-під пляшок із напоями", Recycling, [])
                ]
            | "PVC" -> 
                [
                    Material("Будь-які вироби з PVC-3 (ПВХ). Цей матеріал містить хлор. При нагріванні він виділяє високотоксичні сполуки, тому його переробка та спалювання небезпечні для довкілля. Будь ласка, за можливості уникайте використання такого паковання. (Приклади: труби, підвіконня, плінтуси, пластикові картки, віконні рами, клейонка, лінолеум, вінілові платівки, банери)", ThrowingAway, [])
                ]
            | "LDPE" -> 
                [
                    Material("Комерційні плівки (стретч-плівка, «пупирка», zip-пакети без замочка-бігунка, паковання з-під засобів особистої гігієни: туалетного паперу, підгузків тощо)", Recycling, ["Знімайте наліпки, скотч, застібки"])
                    Material("Поліетиленові пакети («кульочки») всіх розмірів і кольорів", Recycling, [])
                    Material("Пакети з-під молочних продуктів, поштових відправлень і ґрунту (чорні всередині)", Recycling, [])
                    Material("Кришечки з-під бутлів для води (без спіненого кільця з поліетилену та наліпки)", Recycling, ["Знімайте спінене кільце поліетилену та наліпку"])
                    Material("Білий спінений поліетилен без друку або з друком", Recycling, [])
                    Material("Не білий спінений поліетилен без друку або з друком", Recycling, [])
                ]
            | "PP" -> 
                [
                    Material("Прозорі та кольорові (окрім чорних) неплівкові вироби з поліпропілену (контейнери, кришечки, ємності з-під молочних виробів, відра тощо)", Recycling, ["Знімайте всі етикетки й наліпки"])
                    Material("Чорні неплівкові вироби з поліпропілену (контейнери, кришечки, ємності з-під молочних виробів, відра тощо)", Recycling, [])
                    Material("Поліпропіленові мішки усіх кольорів (для будівельного сміття, поштових паковань, сухої харчової продукції тощо)", Recycling, [])
                    Material("Ящики для овочів, фруктів або скляних пляшок", Recycling, [])
                    Material("Плівки-«шуршики» (марковані/немарковані, будь-якого кольору)", Burning, [])
                ]
            | "PS" -> 
                [
                    Material("Білий пінопласт (той, що кришиться на кульки), кольоровий або з кольоровими вкрапленнями", Recycling, [])
                    Material("Паковання біле та кольорове зі спіненого полістиролу", Recycling, [])
                    Material("Дитячі іграшки зі спіненого полістиролу", Recycling, [])
                    Material("Коробочки від CD-дисків та касет", Recycling, [])
                    Material("Одноразові виделки, ножі та ложки (чисті та без наліпок, прийом цих пластиків здійснює консультант на станції)", Recycling, [])
                    Material("Інше паковання з маркуванням PS6", Burning, [])
                ]
            | "PC" -> 
                [
                    Material("Бутлі з-під води", Recycling, [])
                    Material("CD", Recycling, [])
                    Material("Інші вироби з полікарбонату", Recycling, [])
                    Material("Вироби із маркуванням «7 OTHER»", Burning, [])
                ]
            | "PAP" -> 
                [
                    Material("Гофрокартон", Recycling, ["Знімайте наліпки і скотч"])
                    Material("Пап’є-маше (лотки з-під яєць, паковання для напоїв на виніс)", Recycling, [])
                    Material("Паперовий мікс (газети, книжки, офісний білий папір і всі інші паперові вироби)", Recycling, [])
                    Material("Наклейки, шпалери, фотопапір, кальку, серветки, термопапір (квитки на літак чи концерт, паркувальні талони), пергамент", Burning, [])
                    Material("Чеки", Burning, [])
                ]
            | "C/PAP" -> 
                [
                    Material("Паковання з-під соків, молочних виробів тощо з маркуванням «Tetra Pak», «Pure Pack», «Elo Pak»", Recycling, ["Кришечки можете не знімати"])
                    Material("«Паперові» стаканчики та контейнери з-під доставки їжі", Recycling, [])
                    Material("Кришки для харчових контейнерів, туби з-під чипсів та інші багатошарові вироби, що складаються з картону, фольги і пластикової плівки", Recycling, [])
                ]
            | "FE" -> 
                [
                    Material("Жерстяні бляшанки з-під продуктів харчування та лакофарбових виробів, а також кришечки", Recycling, [])
                    Material("Частка пластику на побутовому металі не має перевищувати 10%.", Recycling, [])
                    Material("Побутовий метал (каструлі, сковорідки, цвяхи тощо)", Recycling, ["Частка пластику на побутовому металі не має перевищувати 10%."])
                ]
            | "ALU" -> 
                [
                    Material("Алюмінієві бляшанки з-під-консервів і напоїв", Recycling, [])
                    Material("Алюмінієві туби та кришечки", Recycling, [])
                    Material("Фольга й паковання з фольги", Recycling, [])
                ]
            | "GL" -> 
                [
                    Material("Кольорове скло та кольоровий склобій", Recycling, ["Знімайте кришечки з усього скла, крім пляшок з-під олії"])
                    Material("Прозоре скло без відтінків і прозорий склобій", Recycling, ["Знімайте кришечки з усього скла, крім пляшок з-під олії"])
                    Material("Листове скло", Recycling, [])
                    Material("Лампочки", ThrowingAway, [])
                    Material("Термометри", ThrowingAway, [])
                    Material("Фарбоване скло", ThrowingAway, [])
                    Material("Кераміку", ThrowingAway, [])
                    Material("Дзеркала", ThrowingAway, [])
                    Material("Термоскло, гартоване скло", ThrowingAway, [])
                    Material("Захисне скло для ґаджетів", ThrowingAway, [])
                    Material("Кришталь", ThrowingAway, [])
                    Material("Лінзи", ThrowingAway, [])
                    Material("Ампули", ThrowingAway, [])
                ]
            | "Other" -> 
                [
                    Material("Пульверизатори, дозатори", Recycling, [])
                    Material("Немарковані кришечки", Recycling, [])
                    Material("Натуральний корок і коркові пробки", Recycling, [])
                    Material("Ґаджети, кабелі та дрібну побутову техніку розміром 1х1 м", Recycling, [])
                    Material("Дрібна деревина", Recycling, [])
                    Material("Книжки для буккросингу", Recycling, [])
                    Material("Системи нагрівання тютюну, одноразові електронні цигарки, вейпи (для заряджання систем скидання дронів)", Recycling, [])
                    Material("Віск, парафін в будь-якому вигляді (для виготовлення окопних свічок)", Recycling, [])
                    Material("Усі інші пластикові вироби без маркування", Burning, [])
                    Material("Текстильні відходи: зношений одяг, взуття, м’які іграшки й сумки (великі металеві деталі треба відокремити, дрібний металевий декор можна залишити, якщо він є частиною одягу — наприклад, замки, ґудзики тощо; з іграшок обов’язково відділити всі металеві частини та батарейки)", Burning, [])
                    Material("Гнучкі пластикові туби з-під косметики та зубної пасти", Burning, [])
                    Material("Порожні ємності з-під косметики", Burning, [])
                    Material("Фільтри для води зворотного осмосу (без металу та внутрішнього наповнювача)", Burning, [])
                    Material("Картриджі з-під фільтру-глечика", Burning, [])
                    Material("Одноразові пластикові соломинки", Burning, [])
                    Material("Пластикові зубні щітки", Burning, [])
                    Material("Губки для миття посуду", Burning, [])
                    Material("Гумові вироби (рукавиці, шланги, взуттєві підошви)", Burning, [])
                    Material("Касети, дискети", Burning, [])
                    Material("Рентгенівські знімки", Burning, [])
                    Material("Корки синтетичні", Burning, [])
                    Material("Пластикові туби з-під косметики", Burning, [])
                    Material("Пластикові оправи з-під окулярів", Burning, [])
                    Material("Оргскло", Burning, [])
                    Material("Контейнери, стаканчики, коробочки PS", Burning, [])
                    Material("Пластикові кришечки з-під води Borjomi", Burning, [])
                    Material("Бахіли", Burning, [])
                    Material("Фарм- і медвідходи (використані маски, рукавички, ліки, ампули, шприци)", ThrowingAway, [])
                    Material("Батарейки, акумулятори, ртутьвмісні (люмінісцентні) лампи й термометри", ThrowingAway, [])
                    Material("Відходи, що контактували з рідинами тіла (кров’ю, сечею, слиною, виділеннями)", ThrowingAway, [])
                    Material("Використані засоби особистої гігієни (підгузки, прокладки, тампони тощо)", ThrowingAway, [])
                    Material("Картриджі принтера", ThrowingAway, [])
                    Material("Газові балони", ThrowingAway, [])
                    Material("Кераміку", ThrowingAway, [])
                    Material("Штучні ялинки (ПВХ+метал)", ThrowingAway, [])
                    Material("Гострі й ріжучі предмети (голки, леза)", ThrowingAway, [])
                    Material("Будівельні відходи, лаки, фарби, цегла", ThrowingAway, [])
                    Material("Пластилін", ThrowingAway, [])
                    Material("Недопалки і стіки для куріння", ThrowingAway, [])
                    Material("Запальнички", ThrowingAway, [])
                    Material("Вибухонебезпечні предмети (гільзи, снаряди тощо)", ThrowingAway, [])
                    Material("ДСП/фанери/великі шматки меблів", ThrowingAway, [])
                    Material("Склопакети", ThrowingAway, [])
                ]
            | _ -> this.SearchByAlias name
    
    member private this.SearchByAlias(alias: string) =
        let category = 
            (this :> IRecyclingRepository).getMaterialCategories()
            |> List.tryFind (fun category -> this.IsMatch alias category.Aliases)

        match category with
        | Some(category) -> (this :> IRecyclingRepository).getMaterialInfo(category.Name)
        | None -> [Material("Unknown category", ThrowingAway, [])]

    member private this.SplitNumberRange(range: string) =
        let array = range.Split('-')

        match array.Length with
        | 1 -> [range]
        | 2 -> this.GetRange array.[0] array.[1]
        | _ -> [range]

    member private this.ExpandAliasRange(alias: string) =
        let (|Prefix|_|) (p:string) (s:string) =
            if s.StartsWith(p) then
                Some(s.Substring(p.Length))
            else
                None

        match alias with
        | Prefix "♻️" numberRange -> this.SplitNumberRange numberRange
        | _ -> [alias]


    member private this.GetRange startRange endRange = 
        let startNumber = startRange |> int
        let endNumber = endRange |> int
        
        let range = [startNumber..endNumber]

        let strRange = range |> List.map (fun number -> number.ToString()) 
        strRange

    member private this.IsMatch name aliases =
        aliases
        |> List.map (fun alias -> this.ExpandAliasRange alias)
        |> List.exists (fun expanded -> List.exists (fun a -> a = name) expanded)
             