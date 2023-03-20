<p>The combat system will be split into turns during wich, the player or the AI can make different choices<br>
The turn will begin giving the highest speed character priority</p>

---

#### The System

- During it's turn a characher can chose to make different actions:
	- Move -> [[Movement]]
	- Action
	- Wait
	- Status

Each action holds a different output.

---

#### Possible Risks

- *Each state of combat needs to be separeted from the others*
- *Movement has to take in account the character statistics in order to calculate the move area*
- *Substates are required*

---

#### Possible Solutions

- **State Machine**
	- *Will make sure the state of combat is managed and separeted from the rest of the logic*
- **Command Pattern**
	- *Will make sure there will be no conflict in utilizing the same input for different actions*
	- *Undo function, perfect for restoring the privous state of a character in case the movement has to be re-done*
