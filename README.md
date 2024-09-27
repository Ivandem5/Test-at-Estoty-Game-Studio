[Resources]
I didn't push many dependencies through Zenject, just the main ones. Also, for the spawning of enemies, I decided to abandon the use of the factory. I pass the injected properties to the object upon creation.
For convenience, I have prepared a prefab where you can later place all instantiated resources.

[Enemies]
Since the game involves different enemies, I prepared an abstract class to make it convenient to fill the project with new content.
Since no details were given about the features of enemy spawning and their limitations, I decided to make the usual endless spawning of enemies, with a given frequency. Without taking into account the types of enemies and their possible characteristics that affect the frequency of creation.
For ease of use, I decided to store enemy settings in a ScriptableObject. 
In the future, this practice can be expanded to the configuration of the player and spawners.

I decided to make the player's collisions with enemies without physical collision. So that the player can escape through some of the enemies by sacrificing HP.

[Animation]
Since there are not many animations at the moment, I decided to do without using Animancer.

[UI]
Since only 2 logics had to be implemented for the UI, I did not change it in any way, I just used the ready-made layout.

[Player]
I implemented the player through the Single Responsibility Principle (SRP), where the subsystem requests data from the main system. Since the player logic is not very massive, I did not make abstractions here to save time. At the same time, this concept can be easily expanded in the future.

[Map borders]
The assignment did not specify the boundaries of the map, and I did not add boundaries to the game location or add any logic there. Just checking that enemies are not created outside the map.# Test-at-Estoty-Game-Studio
