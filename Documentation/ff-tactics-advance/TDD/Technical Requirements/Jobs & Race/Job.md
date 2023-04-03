
#### **Description**

- ~ Jobs define the specialization of a character in the game.
- ~ It also limits the types of weapons it can equip and thus the abilities it can learn.
- ~ In order to change jobs a character need to satisfy some requirements, this being having N number of A-Abilities equipped.

#### **Implementation**

- ^ **Scriptable Objects** → As the structure is shared among characters without changing it can remain static.

---

| **Variables**                                  | **Type**           |
| ---------------------------------------------- | ------------------ |
| + <span style="color:#443fde">**Name**         | `string`           |
| + <span style="color:#443fde">**Branches**     | `List<JobRequirement>` |
| + <span style="color:#443fde">**StatGrow**     | `Stats`            |
| + <span style="color:#443fde">**Descriptiong** | `string`           |
