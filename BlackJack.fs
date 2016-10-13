module BlackJack
 
open System.Windows.Forms
 
open System.Data
 
open System.Drawing


// creating playing field



let height = 400
 
let width = 400

let mutable win = false

let mutable lose = false

let mutable draw = false

let mutable userUsedAce = false

let mutable dealerUsedAce = false

let mutable secondCard = true

let mutable userAce = 0

let mutable dealerAce = 0

let mutable cardAmount = 52

let mutable userCount = 0;

let mutable dealerCount = 0;
 
let ffont=new Font("Arial", 9.75F,FontStyle.Regular, GraphicsUnit.Point) //choose font
 
let imageform = new Form(Text="Blackjack") //title of window
 
imageform.Height<-height //height of window
imageform.Width<-width //width of window
 
let dealerText=new Label(Top=20,Width=50) //location and size of the label
dealerText.Text<-"Dealer" //text of label

// Label for keeping users card count
let playerValue=new Label(Top=170,Left=100) //location and size of the label
playerValue.Text<-string(userCount) //text of label

// Label for keeping dealers card count
let dealerValue=new Label(Top=20,Left=100) //location and size of the label
dealerValue.Text<-string(dealerCount) //text of label

//create card image boxes
let mutable userImageBoxes = Array.create 10 (new PictureBox())
let mutable dealerImageBoxes = Array.create 10 (new PictureBox())

for x = 0 to 9 do 
    userImageBoxes.[x] <- new PictureBox()
    userImageBoxes.[x].SizeMode <- PictureBoxSizeMode.AutoSize
    userImageBoxes.[x].Top <- 200
    userImageBoxes.[x].Left <- 0 + (20 * x)
    userImageBoxes.[x].ImageLocation <-("CardBack.tif") //image for imagebox

    dealerImageBoxes.[x] <- new PictureBox()
    dealerImageBoxes.[x].SizeMode <- PictureBoxSizeMode.AutoSize
    dealerImageBoxes.[x].Top <- 50
    dealerImageBoxes.[x].Left <- 0 + (20 * x)
    dealerImageBoxes.[x].ImageLocation <-("CardBack.tif") //image for imagebox

let userText=new Label(Top=170,Width=50) //location and size of the label
userText.Text<-"User" //text of label
 
// the button
let btn = new Button(Top=330,Left=10,Text = "Draw")
let btn_stand = new Button(Top=330, Left=100, Text= "Stand")
let btn_new = new Button(Top=330, Left=190, Text= "New Game")
 
imageform.Controls.Add(dealerImageBoxes.[0]) //adding image
imageform.Controls.Add(dealerImageBoxes.[1]) //adding image
imageform.Controls.Add(dealerText) //adding label
imageform.Controls.Add(userImageBoxes.[0]) //adding image
imageform.Controls.Add(userImageBoxes.[1]) //adding image

imageform.Controls.Add(userText) //adding label
 
imageform.Controls.Add(btn) //adding button
imageform.Controls.Add(btn_stand)
imageform.Controls.Add(btn_new)

/////////////////////////////////////////////
//////////////////GAME LOGIC/////////////////
/////////////////////////////////////////////

let ranks = [|"A";"2"; "3"; "4"; "5"; "6"; "7"; "8";"9"; "10";"J";"Q";"K"|]
let suits = [|"S";"H";"C";"D"|]
let mutable cards = Array.create 52 "" 
let mutable values = Array.zeroCreate 52

for s = 0 to 3 do
    for r = 0 to 12 do
         Array.set cards ((13 * s) + r) (ranks.[r] + suits.[s])
         if (r+1 > 10) then Array.set values ((13 * s) + r) (10)
         else Array.set values ((13 * s) + r) (r + 1)

let mutable initialdraw = false;
let mutable cardno = 2;
let mutable dealercardno = 1;
let rnd = System.Random()
let mutable number = rnd.Next(cardAmount) // holds the value of the random number that is generated

// Two temporary arrays are made to hold the cards that are not being used
let mutable temp1 : string [] = Array.create 52 ""
let mutable temp2 : string [] = Array.create 52 ""

// Two temporary arrays that hold the values of the cards that haven't been removed
let mutable tempV1 : int [] = Array.zeroCreate 52
let mutable tempV2 : int [] = Array.zeroCreate 52

//button click function
let ButtonClick evt = 
    if (initialdraw = false) 
    then //draw initial hand for both user and dealer
         let card1 = cards.[number] + ".tif"

         // Check for Ace's
         if values.[number] = 1 && 11 + userCount <= 21 then userAce <- userAce + 1
         if values.[number] = 1 then if 11 + userCount <= 21 then userCount <- userCount + 11 
         
         else
            if userAce > 0  && userCount + values.[number] > 21 then userUsedAce <- true

            if userUsedAce then userCount <- userCount - 10 + values.[number]

            if userUsedAce then userAce <- userAce - 1

            if userUsedAce then userUsedAce <- false

            else
             // Increases the user's card count
             userCount <- userCount + values.[number]

         // Updates the players card count
         playerValue.Text<-string(userCount)

         // removes the card from the array and creates a shorter array
         temp1 <- cards.[..number - 1]
         temp2 <- cards.[number + 1..cardAmount-1]
         cards <- Array.append temp1 temp2

         // removes the value of card from the array and shortens the array
         tempV1 <- values.[..number - 1]
         tempV2 <- values.[number + 1..cardAmount-1]
         values <- Array.append tempV1 tempV2

         // reduces the card count and generates a random number 
         cardAmount <- cardAmount - 1
         number <- rnd.Next(cardAmount)

         let card2 = cards.[number] + ".tif"
         
        // Check for Ace's
         if values.[number] = 1 && 11 + userCount <= 21 then userAce <- userAce + 1
         if values.[number] = 1 && 11 + userCount <= 21 then userCount <- userCount + 11 
         
         else
            if userAce > 0  && userCount + values.[number] > 21 then userUsedAce <- true

            if userUsedAce then userCount <- userCount - 10 + values.[number]

            if userUsedAce then userAce <- userAce - 1

            if userUsedAce then userUsedAce <- false

            else
             // Increases the user's card count
             userCount <- userCount + values.[number]

         // Updates the players card count
         playerValue.Text<-string(userCount)

         // removes the card from the array and creates a shorter array
         temp1 <- cards.[..number - 1]
         temp2 <- cards.[number + 1..cardAmount-1]
         cards <- Array.append temp1 temp2

         // removes the value of card from the array and shortens the array
         tempV1 <- values.[..number - 1]
         tempV2 <- values.[number + 1..cardAmount-1]
         values <- Array.append tempV1 tempV2

         // reduces the card count and generates a random number
         cardAmount <- cardAmount - 1
         number <- rnd.Next(cardAmount)

         let dealercard1 = cards.[number] + ".tif"

         // Check for Ace's
         if values.[number] = 1  && 11 + dealerCount <= 21 then dealerAce <- dealerAce + 1
         if values.[number] = 1 && 11 + dealerCount <= 21 then dealerCount <- dealerCount + 11 
         
         else
            if dealerAce > 0  && dealerCount + values.[number] > 21 then dealerUsedAce <- true

            if dealerUsedAce then dealerCount <- dealerCount - 10 + values.[number]

            if dealerUsedAce then dealerAce <- dealerAce - 1

            if dealerUsedAce then dealerUsedAce <- false

            else
             // Increases the user's card count
             dealerCount <- dealerCount + values.[number]

         // Updates the dealer's card count label
         dealerValue.Text<-string(dealerCount)

         // removes the card from the array and creates a shorter array
         temp1 <- cards.[..number - 1]
         temp2 <- cards.[number + 1..cardAmount-1]
         cards <- Array.append temp1 temp2

         // removes the value of card from the array and shortens the array
         tempV1 <- values.[..number - 1]
         tempV2 <- values.[number + 1..cardAmount-1]
         values <- Array.append tempV1 tempV2

         // reduces the card count and generates a random number
         cardAmount <- cardAmount - 1
         number <- rnd.Next(cardAmount)

(*         let dealercard2 = cards.[number] + ".tif"

          // Check for Ace's
         if values.[number] = 1  then dealerAce <- dealerAce + 1
         if values.[number] = 1 && 11 + dealerCount <= 21 then dealerCount <- dealerCount + 11
         
         else
            if dealerAce > 0  && dealerCount + values.[number] > 21 then dealerUsedAce <- true

            if dealerUsedAce then dealerCount <- dealerCount - 10 + values.[number]

            if dealerUsedAce then dealerAce <- dealerAce - 1

            if dealerUsedAce then dealerUsedAce <- false

            else
             // Increases the user's card count
             dealerCount <- dealerCount + values.[number]

         // Updates the dealer's card count label
         dealerValue.Text<-string(dealerCount)

         // removes the card from the array and creates a shorter array
         temp1 <- cards.[..number - 1]
         temp2 <- cards.[number + 1..cardAmount-1]
         cards <- Array.append temp1 temp2

         // removes the value of card from the array and shortens the array
         tempV1 <- values.[..number - 1]
         tempV2 <- values.[number + 1..cardAmount-1]
         values <- Array.append tempV1 tempV2

         // reduces the card count and generates a random number
         cardAmount <- cardAmount - 1
         number <- rnd.Next(cardAmount)
*)
         userImageBoxes.[0].ImageLocation<-(card1) 
         userImageBoxes.[1].ImageLocation<-(card2)
         dealerImageBoxes.[0].ImageLocation<-(dealercard1) 
//         dealerImageBoxes.[1].ImageLocation<-(dealercard2)
         initialdraw <- true 
         btn.Text <- "Hit"

         // updates the players card count label
         imageform.Controls.Add(playerValue)

         // updates the dealers card count label
         imageform.Controls.Add(dealerValue)

    else //draw a new card
        let newcard = cards.[number] + ".tif"

        // Check for Ace's
        if values.[number] = 1 && 11 + userCount <= 21 then userAce <- userAce + 1
        if values.[number] = 1 && 11 + userCount <= 21 then userCount <- userCount + 11
         
         else
            if userAce > 0  && userCount + values.[number] > 21 then userUsedAce <- true

            if userUsedAce then userCount <- userCount - 10 + values.[number]

            if userUsedAce then userAce <- userAce - 1

            if userUsedAce then userUsedAce <- false

            else
             // Increases the user's card count
             userCount <- userCount + values.[number]

        // Updates the players card count
        playerValue.Text<-string(userCount)

        // updates the players card count label
        imageform.Controls.Add(playerValue)

        // removes the card from the array and creates a shorter array
        temp1 <- cards.[..number - 1]
        temp2 <- cards.[number + 1..cardAmount-1]
        cards <- Array.append temp1 temp2

        // removes the value of card from the array and shortens the array
        tempV1 <- values.[..number - 1]
        tempV2 <- values.[number + 1..cardAmount-1]
        values <- Array.append tempV1 tempV2

        // reduces the card count and generates a random number
        cardAmount <- cardAmount - 1
        number <- rnd.Next(cardAmount)

        userImageBoxes.[cardno].ImageLocation<-(newcard)
        imageform.Controls.Add(userImageBoxes.[cardno]) //adding image
        cardno <- cardno + 1

        //Check if user goes over 21 in "hit button
        if userCount > 21 then lose <- true //dealer under limit. player over


let ButtonStand evt =
    if(true)//Dealer <= 16
    then 
        let newcard = cards.[number] + ".tif"

       // Check for Ace's
        if values.[number] = 1 && 11 + dealerCount <= 21 then dealerAce <- dealerAce + 1
        if values.[number] = 1 && 11 + dealerCount <= 21 then dealerCount <- dealerCount + 11
         
         else
            if dealerAce > 0  && dealerCount + values.[number] > 21 then dealerUsedAce <- true

            if dealerUsedAce then dealerCount <- dealerCount - 10 + values.[number]

            if dealerUsedAce then dealerAce <- dealerAce - 1

            if dealerUsedAce then dealerUsedAce <- false

            else
             // Increases the user's card count
             dealerCount <- dealerCount + values.[number]

        // Updates the dealer's card count label
        dealerValue.Text<-string(dealerCount)

        // updates the dealers card count label
        imageform.Controls.Add(dealerValue)

        // removes the card from the array and creates a shorter array
        temp1 <- cards.[..number - 1]
        temp2 <- cards.[number + 1..cardAmount-1]
        cards <- Array.append temp1 temp2

        // removes the value of card from the array and shortens the array
        tempV1 <- values.[..number - 1]
        tempV2 <- values.[number + 1..cardAmount-1]
        values <- Array.append tempV1 tempV2

        // reduces the card count and generates a random number
        cardAmount <- cardAmount - 1
        number <- rnd.Next(cardAmount)

        dealerImageBoxes.[dealercardno].ImageLocation<-(newcard)
        imageform.Controls.Add(dealerImageBoxes.[dealercardno])
        dealercardno <- dealercardno + 1

        // win or lose check in the "stand button"
        if userCount = 21 && dealerCount = 21 then draw <- true //both 21
        elif userCount = 20 && dealerCount = 20 then draw <- true //both 20, dealer wont risk going over
        elif dealerCount > 21 then win <- true //player under limit. dealer over
        elif userCount < dealerCount && dealerCount <= 21 then lose <- true //dealer wins without going over



let ButtonplayAgain evt =
    // Rest the card counts and labels
    userCount <- 0
    dealerCount <- 0
    playerValue.Text <- userCount.ToString()
    dealerValue.Text <- dealerCount.ToString()
    cards <- Array.create 52 ""
    values <- Array.zeroCreate 52
    cardAmount <- 52
    userAce <- 0
    dealerAce <- 0
    dealerUsedAce <- false
    userUsedAce <- false
    win <- false
    lose <- false
    draw <-false

    // Reset the deck for new game
    for s = 0 to 3 do
    for r = 0 to 12 do
         Array.set cards ((13 * s) + r) (ranks.[r] + suits.[s])
         if (r+1 > 10) then Array.set values ((13 * s) + r) (10)
         else Array.set values ((13 * s) + r) (r + 1) 

    for x = 0 to 9 do 
        imageform.Controls.Remove(userImageBoxes.[x])
        imageform.Controls.Remove(dealerImageBoxes.[x])
    for x = 0 to 9 do 
        userImageBoxes.[x] <- new PictureBox()
        userImageBoxes.[x].SizeMode <- PictureBoxSizeMode.AutoSize
        userImageBoxes.[x].Top <- 200
        userImageBoxes.[x].Left <- 0 + (20 * x)
        userImageBoxes.[x].ImageLocation <-("CardBack.tif") 

        dealerImageBoxes.[x] <- new PictureBox()
        dealerImageBoxes.[x].SizeMode <- PictureBoxSizeMode.AutoSize
        dealerImageBoxes.[x].Top <- 50
        dealerImageBoxes.[x].Left <- 0 + (20 * x)
        dealerImageBoxes.[x].ImageLocation <-("CardBack.tif") 
    imageform.Controls.Add(dealerImageBoxes.[0]) 
    imageform.Controls.Add(dealerImageBoxes.[1]) 
    cardno <- 2
    dealercardno <- 1
    initialdraw <- false
    btn.Text <- "Draw"
    imageform.Controls.Add(userImageBoxes.[0]) 
    imageform.Controls.Add(userImageBoxes.[1]) 
   


btn.Click.Add(ButtonClick)
btn_stand.Click.Add(ButtonStand)
btn_new.Click.Add(ButtonplayAgain)

imageform.Show()

Application.Run(imageform) |> ignore
