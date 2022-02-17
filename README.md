# ConnectFour
connect four game with winform UI
* player vs player
* player vs Bot

## Game Setting Form
* start the game by setting the names of the players and the game board size.
### Player vs Bot
<div align="center">
  <img src="https://github.com/Matan231/ConnectFour/blob/master/examples/game_setting_form_with_name.PNG" width="300" height="350"><br><br>
</div>

### Player vs Player
* by marking the box to the left of player2 name, the game is in PvP mode
* press start to bagin a new game.
<div align="center">
  <img src="https://github.com/Matan231/ConnectFour/blob/master/examples/Game_set_form_player_vs_playerPNG.PNG" width="300" height="350"><br><br>
</div>



## Game Board
* press on the numbers at the top of the board to insert new token 
* the tokens are falling from the top to the lowest place they can go

<div align="center">
  <img src="https://github.com/Matan231/ConnectFour/blob/master/examples/Game_board.PNG" width="400" height="450"><br><br>
</div>

### winning the game
* The first player that achives four neighboring tokens of his shape (horizontal diagonal or vertical) wins the game!
* the score of the wining player is incremented by 1 
* press "yes" for another match or "no" to exit the game

<div align="center">
  <img src="https://github.com/Matan231/ConnectFour/blob/master/examples/Wining.PNG" width="400" height="450"><br><br>
</div>

## Bot AI 
the Bot is using minmax strategy with a search tree
