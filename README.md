# DanceFloorOsu
Uses an osu! file to create a disco floor

# Usage
- Create a `new DanceFloor` and give it the AudioSource that the music will play from
- When loading a beatmap, call `DanceFloor#Load`
- When updating:
  - Call `DanceFloor#Update`
  - Get `DanceFloor#ColorGrid` or `DanceFloor#ColorArray`
