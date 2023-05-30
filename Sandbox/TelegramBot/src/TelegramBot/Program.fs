module Program

open System
open Funogram.Api
open Funogram.Telegram
open Funogram.Telegram.Bot
open Recycling.Domain
open Recycling.Repository

let MaterialToString(material: Material) =
    let color =  match material.Class with
                | Recycling -> "🟢"
                | Burning -> "🟠"
                | ThrowingAway -> "🔴"
                | _ -> ""
    
    let notes = material.Notes |> String.concat ","
    match notes.Length with
        | 0 -> $"{color} {material.Name}"
        | n -> $"{color} {material.Name} ({notes})"

let BuildMaterialMessage(categories: Material list) =
    categories
    |> List.map MaterialToString
    |> String.concat Environment.NewLine

let CategoryToString(category: MaterialCategory) =
    let colors = 
        List.fold(fun acc c -> 
            let color = match c with
                | Recycling -> "🟢"
                | Burning -> "🟠"
                | ThrowingAway -> "🔴"
                | _ -> ""
            $"{acc}{color}") "" category.Classes

    let aliases = category.Aliases |> String.concat ","
    match aliases.Length with
        | 0 -> $"{colors} {category.Name}"
        | n -> $"{colors} {category.Name} ({aliases})"

let BuildCategoriesMessage(categories: MaterialCategory list) =
    categories
    |> List.map CategoryToString
    |> String.concat Environment.NewLine
    
let ShowCategories (ctx: UpdateContext) =

    let repo = new SimpleRepository()
    
    let categories = (repo :> IRecyclingRepository).getMaterialCategories()

    match ctx.Update.Message with
    | Some { MessageId = messageId; Chat = chat } ->
        Api.sendMessage chat.Id (BuildCategoriesMessage categories ) |> api ctx.Config
        |> Async.Ignore
        |> Async.Start
    | _ -> ()

let DescribeCategory command: string =

    let repo = new SimpleRepository()
    (repo :> IRecyclingRepository).getMaterialInfo(command)
    |> BuildMaterialMessage

// let DescribeCategory command =
//     match command with
//     | "PET-1" -> "🟢 Пляшки прозора з-під напоїв без відтінку
// 🟢 Пляшки прозора з-під напоїв з блакитним відтінком 
// 🟢 Пляшки прозора з-під напоїв з зеленим відтінком
// 🟢 Пляшки прозора з-під напоїв з коричневим відтінком
// 🟢 Пляшки прозора з-під напоїв з жовтим відтінком
// 🟢 Пляшки з-під напоїв чорна
// 🟢 Пляшки білі з-під молочки
// 🟢 Пляшки прозорі з-під молочки
// 🟢 Пляшки з-під олії прозорі
// 🟢 Пляшки з-під оцту та соєвого соусу, тільки якщо етикетка легко знімається (сортуються в один бак з пляшками з-під олії)
// 🟢 Прозорі й кольорові пляшки з-під засобів побутової хімії (кришечки, ковпачки, дозатори й наліпки можна лишати)
// 🟠 Непрозорі пляшки з-під молочних та інших виробів, темно-синього та світло-коричневого кольору
// 🟠 Усі інші вироби з маркуванням «PET-1»: (одноразовий посуд, блістери, кришки тощо)"
//     | _ -> "Unknown category"

let HandleCommand ctx command =
    match ctx.Update.Message with
    | Some { MessageId = messageId; Chat = chat } ->
        Api.sendMessage chat.Id (DescribeCategory command) |> api ctx.Config
        |> Async.Ignore
        |> Async.Start
    | _ -> ()

let updateArrived (ctx: UpdateContext) =

    processCommands ctx [|
        cmd "/start" ShowCategories
        cmdScan "/say %s" (fun text _ -> printfn "User invoked say command with text %s" text)
        cmdScan "%s" (fun cmd ctx -> HandleCommand ctx cmd)
    |] |> ignore

    // match ctx.Update.Message with
    // | Some { MessageId = messageId; Chat = chat } ->
    //     Api.sendMessageReply chat.Id "Hello, world!" messageId |> api ctx.Config
    //     |> Async.Ignore
    //     |> Async.Start
    // | _ -> ()


[<EntryPoint>]
let main _ =
    async {
        //let config = Config.defaultConfig |> Config.withReadTokenFromFile
        let config = { Config.defaultConfig with Token = "5853779019:AAFZd1_tm4bLr-adFhJJXihT0-fADIcWDP8" }
        let! _ = Api.deleteWebhookBase () |> api config
        return! startBot config updateArrived None
    } |> Async.RunSynchronously
    0