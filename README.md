# Ocean and Sky

Video Game developed for the Montreal Ubisoft Gamelab 2016.

### Concept
Ocean and Sky is a cooperative, 3D rendered, side-scrolling platformer. 
The screen is split horizontally across the middle, allowing Player 1 (P1) movement in the top half regularly, and Player 2 (P2) movement in the bottom half regularly. The camera moves eastwards throughout the game, with the two PlayerCharacters confined to the camera view. Neither Player is able to go farther than camera space in any direction.

Levels are Time-based, with a Timer that is constantly counting down. There are collectible *Elements* that spawn and help keep the Timer from reaching 0. 


### Premise
Two characters, Spark and Echo, were punished millenia ago for unsaid crimes. Their penalty was to race across the world forever or until the end of time. Whichever comes first. The felons, mad with energy and motion, were quick to wonder if this was a curse. Or a blessing.

Spark is controlled by P1, and thus regularly moves in the Sky/Top Half of the screen.
Echo is controlled by P2, and thus regularly moves in the Ocean/Bottom Half of the screen.
 
### **Key Mechanics**

*Character Interactions*
* **PlayerCharacter Collisions:** Spark and Echo can bounce off of each other to alter each other's trajectory rapidly. This is useful for dodging obstacles and for reaching higher/lower than usual areas on the screen.
* **Camera Zoom:** As Spark and Echo come closer together, the camera zooms in to focus on the two PlayerCharacters. The longer and closer they stay together, the slower Time passes in the Timer. Actual in-game motion and pacing is unaffected. Camera zoom and proximity of the characters is also useful for going through narrow areas simultaneously.
* **Powers:** Spark and Echo each have access to a circular burst of light or sound that emanates from their respective body. And can be used to immediately clear obstacles or difficult blockades that would be impossible to go over/under. Using a Power costs a large amount of Time to be stripped from the Timer.

*Diving/Jumping*
* Spark (P1) can dive into the Ocean/Bottom Screen Space for a very short period of time. In doing so, Spark gains a temporary movement speed boost and a steep trajectory downwards. Echo (P2), can jump into the Sky/Top Screen Space for a very short period of time. In doing so, Echo gains a temporary movement speed boost and a steep trajectory upwards. Sharp dives and leaps can be used to reach higher areas onscreen, as well as dodge larger obstacles.

*Tandem*
* PlayerCharacters are required to use each other to help dodge obstacles, collect Elements to gain more time, and guide one another through each level.
* The closer in time each Player's input is with the other, the more in Tandem they are. As Tandem increases, so does the likelihood of Elements dropping to affect the Timer. Tandem increasing also causes the total game speed to increase - furthering the pace and difficulty of the game. **Tandem Zones** are vertical bars of colour that swipe across the screen from time to time, and will prompt a button press from both P1 and P2. The closer in sync the button presses are, the more in Tandem the players will be.

### **Systems**
*Day/Night Cycle*
* Counter intuitively, the manipulation of the Timer allows for controlling how quickly the Day and Night Cycle switches. Fast changes into Daytime cause more Tandem Zones to appear during the Daytime. And fast changes into Nighttime cause more Elements to appear at night. Also, Daytime has Low Tide, and with it - more room for Spark to fly. While Nighttime has High Tide giving Echo far more to swim through. Swapping rapidly between the times of day can cause Sunrise or Sunset to appear - when the two Players are matched in perfect Tandem.

*Timer*
* The Timer counts down from a total value of 100, until the end value of 0. Collecting Elements, increasing Tandem, and keeping Spark and Echo close together all effect the Timer's countdown in some way. There are three Elements that spawn randomly throughout a stage. **Element A**, stops the Timer from counting down for a short period. **Element B**, slows the Timer's countdown for a brief moment. **Element C**, returns some lost time back into the Timer. **Increasing Tandem** both increases the game pace and Player Character movement speed, and it also slightly reduces the rate at which the Timer counts down. **Keeping Spark and Echo close** will also reduce the rate at which the Timer counts down. Using **Powers** will, however, expend some of the Time left. Also, colliding with obstacles will deduct some Time from the Timer.
* The Timer can be *Sweet Spotted*, which means, if the Timer is kept at a certain period of time for some moments, it will incite immediate change of day. Either switching to Day or Night, and bringing extra rewards.

### **System Loop**
* The Loop contains the motion of P1 and P2 as they dodge obstacles, stay close, and build up Tandem with standard controls, *Diving/Jumping* and the *Character Interactions*. The Players then use Time as either a currency to clear more difficult paths with their *Powers*, or *Sweet Spot* the Timer to help incite changes in the day. Manipulating the Day and Night Cycle will create more opportunities to build Tandem, and in turn, to increase the game speed. As the game grows more rapid, more challenges ensue, causing more opportunities to use and gain Time and repeat the cycle.

### **Checkpoint System**
* Rather than an orthodox checkpoint system, the means of saving progress is through the Timer. Every so often, **Save Elements** appear that can be picked up - saving the current amount of time left in the Timer. If ever Players need extra time, they can cash in the Save Element, before it expires, to get a portion of Time added back to their timer. This is to promote the constant motion of the game, so there is no halt in movement from the beginning to the end.

### **A.I.**
* *Weather:* Different weather types appear based on the Player styles. The weather frequencies and natures are based on the nature of the playstyles of the two Players, creating a unique experience for different pairs of players. Different patterns include ferocity of the winds, cluttering of the screen and flashes of lightning to try and hinder Spark and Echo.

