
# Jolt Overdrive Prototype
## Project Overview
**Jolt Overdrive** is a proof of concept prototype developed in Unity as part of the game pitch assessment. 
The prototype demostrates the core gameplay of a **Vehicle-Arcade Combat Action** game, focusing on physics-drriven movement, modular vehicle components, and short, high-impact combat encounters.

This prototype is not a complete game and is intended to showcase key mechanics described in the accompanying pitch document.

## Play Instructions & Controls
### Controls
- **W/A/S/D:** Move vehicle forward, left, backward, right
- **Mouse Movement:** Rotate camera / aim direction
- **Left Mouse Button (LMB):** Jolt Attack (primary ability)

### How to Play
- Use **WASD** to navigate the arena and explore the environment.
- Use **mouse movement** to orient the vehicle and camera.
- Press **Left Mouse Button** to perform a **Jolt Attack**, jolting the vehicle forward toward a target or in the foward direction if no target is present.
- Enage enemy cannon unit within range.
- Avoid or interact with environmental hazards such as explosive barrels. 
- Vehicle components take damage through physical impact and may detach, affecting vechicle performace and additional hazards in the arena. 

### Prototype Scope & Limitations
- Single playable arena
- Physics-driven vehicle controller.
- Modular vehicle component system
- Enemy cannon AI basis targeting logic
- No menu, tutorial, or progression systems included

### Known Bugs & Limitations
- **Vehicle controller occasionally overrides physics jolt impulses:** workaround, Jolt movement is implemented using Rigidbody methods rather than simple force application. and reset vehicle parameters.
- **Wrong focus direction:** has part of the heuristic to select jolt target, it was not considered that if vechicle behind player is closer ignore. 

### Referencing
- Unity Technology - Unity Engine Documentation (https://docs.unity3d.com/Manual/index.html)
- Following games as references informed the design and development
  - Mad Max (Avalanche Studios, 2015)
  - BeamNG.drive (BeamNG GmgH, 2015-Present)
  - SpyHunter (Midway Games)
  - Crash Team Racing (Naughty Dog, 1999)
 
 ### Engine & Tools
 - **Game Engine:** Unity Engine (Unity 6)
 - **Scripting Language:** C#
 - **Physics:** Unity Rigidbody & Collider systems
 - **Assets:** Unity Asset Store vehicle controller (modified for prototype use)
### Notes
This prototype is intended to demonstrate:
- Core gameplay loop functionality
- Physics-based interaction systems
