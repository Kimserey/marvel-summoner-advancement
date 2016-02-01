namespace Marvel

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Html

[<JavaScript>]
module Client =    
    
    type Title =
    | Text of string
    | TextWithImportant of string * string
    
    let Main = 
        let result = 
            Var.Create 0

        let values =
            [ (Title.Text "Quest or versus crystal", 5, Var.Create 0)
              (Title.Text "Free crystal", 10, Var.Create 0)
              (Title.Text "Arena 2 star hero or crystal shard crystal", 20, Var.Create 0)
              (Title.Text "Daily, alliance quest, or golden crystal", 30, Var.Create 0)
              (Title.TextWithImportant ("Alliance crystal", "4th feb - 11th feb"), 300, Var.Create 0)
              (Title.Text "Lesser ascendant crystal", 40, Var.Create 0)
              (Title.Text "Premium hero shard crystal", 60, Var.Create 0)
              (Title.Text "Hero crystal", 100, Var.Create 0)
              (Title.Text "Premium hero crystal", 200, Var.Create 0)
              (Title.Text "3-star hero shard crystal", 250, Var.Create 0)
              (Title.Text "Featured hero crystal", 300, Var.Create 0)
              (Title.Text "4-star hero shard crystal", 400, Var.Create 0)
              (Title.Text "3-star hero crytal", 800, Var.Create 0)
              (Title.Text "4-star hero crystal", 5000, Var.Create 0)]
        
        let form =
            let formGroups =
                values
                |> List.map(fun (title, mult, value) ->
                    let style = Var.Create ""
                    match title with 
                    | Text title -> 
                          divAttr [ attr.``class`` "form-group"
                                    attr.styleDyn style.View]
                                  [ labelAttr [ attr.``for`` title
                                                attr.``class`` "col-xs-6 control-label" ]
                                              [ text title ]
                                    labelAttr [ attr.``for`` title
                                                attr.``class`` "col-xs-4 control-label" ]
                                              [ text (string mult + "points x") ]
                                    divAttr [ attr.``class`` "col-xs-2" ]
                                            [ Doc.IntInputUnchecked [ attr.``class`` "form-control"
                                                                      attr.id title
                                                                      on.focus (fun _ _ -> Var.Set style "border:1px solid blue")
                                                                      on.blur (fun _ _ -> Var.Set style "") ] value ] ] :> Doc
                    | TextWithImportant (title, important) ->  
                          divAttr [ attr.``class`` "form-group"
                                    attr.styleDyn style.View]
                                  [ labelAttr [ attr.``for`` title
                                                attr.``class`` "col-xs-6 control-label" ]
                                              [ text (title + " - "); spanAttr [ attr.style "color:red;" ] [ text important ] ]
                                    labelAttr [ attr.``for`` title
                                                attr.``class`` "col-xs-4 control-label" ]
                                              [ text (string mult + "points x") ]
                                    divAttr [ attr.``class`` "col-xs-2" ]
                                            [ Doc.IntInputUnchecked [ attr.``class`` "form-control"
                                                                      attr.id title
                                                                      on.focus (fun _ _ -> Var.Set style "border:1px solid blue")
                                                                      on.blur (fun _ _ -> Var.Set style "") ] value ] ] :> Doc)
                                                                      
            let calculate() =
                values |> List.sumBy (fun (_, mult, value) -> mult * value.Value)

            formAttr [ attr.``class`` "form-horizontal" ] 
                     [ yield! formGroups
                       yield  Doc.Button "Calculate" [ attr.``type`` "submit"
                                                       attr.style "margin:auto; width: 300px;"
                                                       attr.``class`` "btn btn-default btn-block" ] (calculate >> Var.Set result) :> Doc ]

        let resultDiv =
            divAttr [ attr.``class`` "well"
                      attr.style "font-size: xx-large;text-align: center;margin: 30px;"] [ text "Total: "; Doc.TextView (result.View |> View.Map string) ]

        [ divAttr [ attr.``class`` "jumbotron" ] [ divAttr [ attr.``class`` "container" ] [ h1 [ text "Summoner advancement" ] ] ]
          divAttr [ attr.``class`` "container" ]
                  [ form; resultDiv] ]
        |> Seq.cast
        |> Doc.Concat
        |> Doc.RunById "main"
