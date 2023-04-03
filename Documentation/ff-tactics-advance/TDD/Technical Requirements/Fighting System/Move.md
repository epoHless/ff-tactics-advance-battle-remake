The Movement will be grid based on square tiles, each tile will have a travel cost.<br>
Climbing a hill or traversing a river for example will cost more energy.<br>
The movement will be in 4 directions, each entity in the current combat will be able to move in the map, Enemies too.

- !! **Each character can move once per turn. when the turn ends, the player will have to chose a [[Facing Direction]].**

----

#### **The tiles will**

- **Be Traversable**
	- *By the player*
	- *By the AI*

<span>

- **Be Selectable by the player input**
	- *During the move state the player will be able to move the cursor to each tile, giving the tooltip of a character if selected;*
	- *A highlight of the traversable tiles will appear showing the player the tiles it can move to;*	
	- *A highlight of the range of the attacks will appear when selecting a target for an attack*

---

> [!info] Movement Highlight
> ![](ff-move.png)
> <p style="text-align: center; "><i>Movement highlight</i></p>
> 

---

#### **Possible Risks**

1. The AI needs to be able to use the system and travel seamlessly around the map.
2. Travel cost needs to be taken in account when planning the grid system

---

#### **Grids**

- **WIll have a travel cost**
	- *Based on height or tile type*
- **Each tile can be occupied by only a character at the time** 
	- *When travelling through a tile occupied the character in the occupied tile will move aside to let the moving character pass*

---

#### **Possible Solution**

- **Unity Tile System**
	- $ *Natively implements the neighbour rules, making it easyier for each tile to communicate to one another*
	- ! *A study on the API is necessary in order to use it efficently*
- **Custom Tile Component**
	- $ *Custom API, easier to understand*
	- ! *Lack of online support and documentation*

---

#### **Final Choice**

The **Unity Tile System** will be used to place some **custom tile components** in order to facilitate level building.