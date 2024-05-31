# Realm Hop!
## Overview
- Game is split up into sectors called 'rooms'
- 4 difficulties of 'rooms': Green, yellow, red, black
- 'Rooms' are prefabs that appear (animation) when the last 'room' is completed
- At the start, there is a higher chance of an easier room appearing, but as the game progresses the chance for harder rooms to appear grows
- Objective is to get through the most rooms possible
- Character model is a 'blob' called Blobert
- Skins are variations on Blobert

## Mechanics
- Movement is 'bhop' like - moving camera side to side increases speed
- Constant forward and jumping movement
- Last press takes priority, however if the last press is released and the other direction is still pressed, the camera will still rotate in that direction

## Game modes
- Three game modes - Classic, Rush & Rooms
- Classic: Speed resets when player hits the ground after jumping
- Rush: Speed doesn't decrease unless player collides with an object AND ground at the same time. Rate of gaining speed is inversely proportional to speed.
- Rooms: Classic mechanics, however in this gamemode the player selects the room they want to play, amd try to get the quickest time possible in that room

## UI
**Main Menu**:
- Select game mode
- Settings
- Help: Controls, room difficulties, objective
- Skins

**Rooms**:
- 4 tabs on the side with each room difficulty

**In-game**:
- 'Tap to play': Camera rotates to game position when pressed, and game begins
- Settings button (with quit)

**Settings**:
- Camera Sensitivity
- Sound on/off
- Music on/off
- Show Extra stats: Speed, height, map difficulty

## TBD
- First/third person...


