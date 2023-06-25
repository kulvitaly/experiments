﻿module Program

open System
open Funogram.Api
open Funogram.Telegram
open Funogram.Telegram.Bot
open Recycling.Domain
open Recycling.Repository

let RecyclingClassToColor(recyclingClass) =
    match recyclingClass with
    | Recycling -> "🟢"
    | Burning -> "🟠"
    | ThrowingAway -> "🔴"
    | _ -> ""

let MaterialToString(material: Material) =
    let color =  RecyclingClassToColor material.Class
    
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
            let color = RecyclingClassToColor c
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
    // TODO: we should use DI
    let repo = new SimpleRepository()
    
    let categories = (repo :> IRecyclingRepository).getMaterialCategories()

    match ctx.Update.Message with
    | Some { MessageId = messageId; Chat = chat } ->
        Api.sendMessage chat.Id (BuildCategoriesMessage categories ) |> api ctx.Config
        |> Async.Ignore
        |> Async.Start
    | _ -> ()

let DescribeCategory command: string =
    // TODO: we should use DI
    let repo = new SimpleRepository()
    (repo :> IRecyclingRepository).getMaterialInfo(command)
    |> BuildMaterialMessage

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

[<EntryPoint>]
let main _ =
    System.IO.File.Copy(Environment.GetEnvironmentVariable("BOT_TOKEN_FILE"), $"{Environment.CurrentDirectory}/token", true)
    async {
        let config = Config.defaultConfig |> Config.withReadTokenFromFile
        let! _ = Api.deleteWebhookBase () |> api config
        return! startBot config updateArrived None
    } |> Async.RunSynchronously
    0