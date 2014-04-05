terrier-game
============
Copyright (C) 2014 Phillip Bradbury

This is the primary repository for a game-engine model to be used with a separate interface.
This project is written in C#.

The goal of this project is to create a working skill-based rpg engine that is system agnostic and can be used with any visualization engine.

Currently this project is in alpha stages with the following working features:
  Combat - Attack and Block are the only supported combat actions
  Skills - Only combat skills are supported - Hit, Parry, Dodge, Multiple Attacks, asstd. weapon skills
  
Organization:
  Every object within the game is considered a GameObject.  GameObjects have names and can be examined or duplicated.
  There are 3 main types of GameObjects - Rooms, Players, Items.
  Rooms:
    Rooms are essentially containers which can hold Items and Players (and possibly other Rooms)
    Players are the primary class which represent both NPCs and PCs
    Items are either equipment or consumables
    
Currently this project is in alpha development and is considered as-is
