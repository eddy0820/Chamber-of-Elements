# Chamber of Elements

- re do affinity types class and object class becuase theres a discconect
  - make it easier to add affinities
- make code work with multile affinity types for elements (when your doing passives)
- re do how i do re roll elements and using the array
- re do "reroll specific" code
- create a stat resistance database or something for the takedamge function
- might want to fuck with the order for strength and weakness.
- IFocusable for minions and enemies maybe
- "miss" ui element
- "immune" ui element
- drop down menu for dynamic passives to see what theyve got stacked on top and how many turns for each stack
- need to fix the order of all the scripts and their awake functions or your gonna conitune to have issues with error
- for elements that deal damage two seperate times you need to delay the hit markers with a queue and coroutines maybe
- screen shake settings
- need to update hailstorm and frost when multiple enemies are added


- take art folder out of repository
- re do parts of onplayerclikc and onenemyclick because i fucked up the abstratcion between an element and an element object
- do i really need different stats class for the different character types

- Clean up using stuff in every script

- Elements That Need To Be Implemented
  - Arena - Done
  - Utility
    - Death - when adding minions
	- Enchant - when adding mana system
	- Hearth - when adding minions
	- Runestone - when adding mana system
	- Shield - ask joey about this one
  - Elemental - Done
  
- radianc and light need to deal with minions
	
- confusion element

- Minions
 - Shaman
 - Cleric
 - Knight
 - Earth Elemental
 - Air Elemental
 - Fire Elemental
 - Water Elemental
 - Ice Elemental
 - Magma Elemental
 - Metal Golem
 - Crystal Golem
 - Rock Golem
 - Living Soul
 - Lightning Elemental
 - Human
 - Living Spirit
 - Golden Golem
 - Clay Golem
 - Ceramic Golem
 - Diamond Golem
 - Obsidian Golem
 - Skeleton
 - Glass Golem
 - Dust Devil


- minions
- relics
- visuals
- hover tooltip for elements
- create enemies that existed
- menus

- Ability for minion and element slots

Behavior Every Turn Passive Orders
1 - Poison
1000 - PassiveTurnCalcRemove

Changed Or Added Things from Godot
- Weather Interact Object
- Minions behave like other characters (i.e. have passives and utitlity elements can be used on them
