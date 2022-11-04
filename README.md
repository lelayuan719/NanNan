# NanNan

## Quick Fixes

* If the main menu screen or dialog is hidden, change the game's aspect ratio
  1. Go to the game tab in Unity
  2. Change "Free Aspect" to "Full HD (1920x1080)"

## Git best practices

* Always pull before editing
* Pull often
* Commit often
  * It's better to have lots of small commits that each change one feature than one big commit that does many things
  * If something goes wrong, we can isolate it to one bad commit
* Use reasonably descriptive commit messages

  ❌ "Commit"
  
  * Doesn't say anything about what is in the commit

  ✅ "Fixed bugs"
  
  * Adequate, but could be more descriptive

  😄 "Fixed objects disappearing in Chapter 4"

  * Concisely describes what the commit does

* I recommend committing to main since branches can easily have merge conflicts
  * If you do make a branch, try not to let it get too out of date since that causes merge conflicts
* Don't leave the project in a broken state after a series of commits
* Push to origin when you have a working feature

## Project structure and organization

* Assets folder contains everything
  * Images
  * Scenes
  * Prefabs
  * Scripts
  * Etc.
* Game manager object is always loaded and globally addressable
  * Use it to get the inventory
  * Use it to store global variables
  * In code, use `GameManager.GM.<variable>` to access the game manager
* Use prefabs for things used more than once

## Style Guide

* Classes and functions are in PascalCase (every word capitalized)
* Variables are in camelCase (every word except the first one capitalized)
* Put scripts in the right folders
