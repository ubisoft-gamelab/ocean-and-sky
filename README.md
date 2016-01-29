# Ocean and Sky

Video Game developed for the Montreal Ubisoft Gamelab 2016.

### Concept
Ocean and Sky is a cooperative, 3D exploration game.

The two characters (one is in the sky, one is in the ocean) must stay in close contact in order to stay alive. Their movements influence the zoom level of the camera (which is a follow camera), but the max zoom is restricted.

The two characters must make their way to a landmark on the horizon. As the game progresses, the landmark gets closer and closer but the environment gets more and more hazardous.

The environment consists of dangerous weather events and enemies. There is a game cycle (day/night) that influences which weather events are likely to be triggered and also influences the likelihood of certain enemy spawns (and also enemy behavior).

### Premise
Two characters, Spark and Echo, were punished millenia ago for unsaid crimes. Their penalty was to race across the world forever or until the end of time. Whichever comes first. The felons, mad with energy and motion, were quick to wonder if this was a curse. Or a blessing.

Spark is controlled by P1, and thus regularly moves in the Sky/Top Half of the screen.
Echo is controlled by P2, and thus regularly moves in the Ocean/Bottom Half of the screen.
 
### **Key Mechanics**

*Camera:* As the characters move closer together and further apart, the camera will keep them in the view by zooming in and out (up to a maximum
zoom amount). The camera will also be affected by some weather indirectly; the storm can jostle the players, affecting the camera.

*Diving/Jumping* The characters can leap/dive in order to briefly avoid enemies and obstacles in their domain.

*Affect Hazardous Weather* The characters can perform an action that will help relieve themselves (or the other player!) of a tall wave, or a whirlpool for example. Similar to a "wind/water cutter" attack.

### **Systems**
*Day/Night Cycle*
* Day and night will change quite often. As the time changes, weather patterns will change and different enemies will spawn.

*Weather*
* Weather will positively or negatively affect the movement of the characters, making it more difficult for the characters to stay close and make it more difficult for avoid enemies.

### **System Loop**
* Loop consists of day and night cycles, and natural weather transitions.
* Evolutionary progression -> Game becomes more and more difficult, and there will be more violent weather patterns/enemies as the game progresses.

### **Checkpoint System**
* After leaving a storm, or making it past a series of obstacles/enemies, a checkpoint will be reached.

### **A.I.**
* Different enemies with different behavior will spawn and attempt to attack the players.
