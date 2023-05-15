module Program

open System
open Funogram.Api
open Funogram.Telegram
open Funogram.Telegram.Bot

let GetCategoriesMessage () =
    [
        "PET-1 🟢"
        "HDPE"
        "PVC 🔴"
        "PP"
        "PS"
        "PC"
        "7 OTHER 🟠"
        "PAP"
        "C/PAP"
        "FE"
        "ALU"
        "GL"
    ]

let ShowCategories (ctx: UpdateContext) =
    match ctx.Update.Message with
    | Some { MessageId = messageId; Chat = chat } ->
        Api.sendMessage chat.Id (GetCategoriesMessage() |> String.concat Environment.NewLine) |> api ctx.Config
        |> Async.Ignore
        |> Async.Start
    | _ -> ()

let DescribeCategory command =
    match command with
    | "PET-1" -> "🟢 Пляшки прозора з-під напоїв без відтінку
🟢 Пляшки прозора з-під напоїв з блакитним відтінком 
🟢 Пляшки прозора з-під напоїв з зеленим відтінком
🟢 Пляшки прозора з-під напоїв з коричневим відтінком
🟢 Пляшки прозора з-під напоїв з жовтим відтінком
🟢 Пляшки з-під напоїв чорна
🟢 Пляшки білі з-під молочки
🟢 Пляшки прозорі з-під молочки
🟢 Пляшки з-під олії прозорі
🟢 Пляшки з-під оцту та соєвого соусу, тільки якщо етикетка легко знімається (сортуються в один бак з пляшками з-під олії)
🟢 Прозорі й кольорові пляшки з-під засобів побутової хімії (кришечки, ковпачки, дозатори й наліпки можна лишати)"
    | _ -> "Unknown category"

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