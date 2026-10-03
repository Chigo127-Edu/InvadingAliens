# InvadingAliens
######

## 0. Index
1. What is InvadingAliens
2. How to download InvadingAliens
3. How to play InvadingAliens
4. How does InvadingAliens work
5. Conclusion
######

## 1. What is InvadingAliens
InvadingAliens is an Open-source game inspired by well-known SpaceInvaders. Our version uses [RetroGameFramework](https://github.com/profGiovanniVolpintesta/RetroGameFramework), also Open-Source.<br>
Hence, the game relies on Windows Forms APIs (Which are not open source), and having .NET Framework v4.7.2 instaled is needed in order to play.
######

## 2. How to download InvadingAliens
Head to the [Releases](https://github.com/Chigo127-Edu/InvadingAliens/releases) to download the game.<br>
For both Windows and Wine environments, .NET Framework v4.7.2 is needed.
######

## 3. How to play InvadingAliens
When the game is launched, the player is welcomed by a console menu.<br>
To choose the desired option, W, A, S, D buttons are used, or the 4 arrows keys, and CARRIAGE RETURN to choose.
######
When a new game is started, the spaceship can be moved using W, A, S, D buttons, or the 4 arrows keys. It is possible to shoot enemies by generating bullets with SPACE, but it is not possible to move and shoot at he same time.<br>
The player's duty is to kill enemies and bosses, in order to lose as few lives as possible. Indeed, if the spaceship collides with an enemy or a boss, or goes out the game field (The application window), the player will lose as many lives as the remaining enemy's. For example, if a boss has 3 lives left, the player loses 3 lives.
######
To earn lost lives, it is necessary to kill bosses. Once killed, they give a number of lives equal to the next level. For example, once killed the level 3 boss, the player receives 4 lives, and their level becomes 4.
######
P is used to pause the game
ESC is used to invoke a game over. Pressing it a second time closes the game. 
######

## 4. How does InvadingAliens work
The game works on a pixel martrix, where various elements interact.<br>
There are three principal three kinds of elements: singular, multiple, and text.
######
The spaceship is a single element, and has its properties and static styles.<br>
Dynamic elements have all some static properties for the type (e.g. style), and dynamic properties (e.g. position and speed).<br>
Enemies are dynamic enemies, generated with a ```list``` of ```Element``` objects. They have a common style.
######
If an enemy is hit by a projectile, it loses a life. It disappears when its lives counts becomes zero.<br>
If an enemy hits the player, they lose as many lives as the remaining enemy's.<br>
As the level increases, monsters spawn rate increases as well.
######
Projectiles are elements with the very similar properties as enemies, except a different style.<br>
Projectiles do not harm the shooter, but only the target(s), which are the enemies.<br>
Game difficulty increases sligltly after completing more levels.
######
Bosses are special enemies, which spawn every 30 seconds, one at a time. Their speed increases when the level increases as well.
######

## 5. Conclusion
This Open-Source project is made by Chigo127-Edu and Ale-Cioffo, under the GNU General Public License, version 3.

