# Chamber of Elements

- re do affinity types class and object class becuase theres a discconect
  - make it easier to add affinities
- make code work with multile affinity types for elements (when your doing passives)
- re do how i do re roll elements and using the array
- re do "reroll specific" code
- create a stat resistance database or something for the takedamge function
- might want to fuck with the order for strength and weakness.
- "miss" ui element
- "immune" ui element
- drop down menu for dynamic passives to see what theyve got stacked on top and how many turns for each stack
- need to fix the order of all the scripts and their awake functions or your gonna conitune to have issues with error
- for elements that deal damage two seperate times you need to delay the hit markers with a queue and coroutines maybe
- screen shake settings

- ice elemental affinity type to cause freeze


- take art folder out of repository
- re do parts of onplayerclikc and onenemyclick because i fucked up the abstratcion between an element and an element object
- need to update hailstorm and frost when multiple enemies are added
- need to update radiance and light when multiple minions are added

- Elements That Need To Be Implemented
  - Arena - Done
  - Utility
    - Death - when adding minions
	- Enchant - when adding mana system
	- Hearth - when adding minions
	- Runestone - when adding mana system
	- Shield - ask joey about this one
  - Elemental - Done
	
- confusion element

- Minions
  - Golden Golem - On Death - Relics
  - Ceramic Golem - Don't Implement
  - Diamond Golem - Don't Implement
 
  - Rock Golem - Retaliation
  - Living Soul - Death Rattle
  - Lightning Elemental - Focus
  - Human - Death Rattle
  - Knight - Strength, Death Rattle
  - Shaman - Enlightenment, Death Rattle
  - Cleric - Focus, Death Rattle
  - Living Spirit - Fear
  - Clay Golem - Spell Power (Earth)
  - Obsidian Golem - Death Rattle
  - Skeleton - Death Rattle
  - Glass Golem - Retaliation
  - Dust Devil - Weather Affinity (Sandstorm)
 
 - Retaliation - On Hit Deal Damage
 - Enlightenment - Start of Turn
 - Spell Power (Earth) - Affinity Attack
 - Weather Affinity (Sandstorm) - Attack in weather
 
 - Death Rattle Feature


- minions
- relics
- visuals
- hover tooltip for elements
- create enemies that existed
- menus

- Mana System
- Ability for minion and element slots
- RogueLike

Sprites Needed
- Retaliation Passive
- Enlightenment Passive
- Spell Powers
- Weather Affinity

Behavior Every End Of Turn Passive Orders
1 - Poison
1000 - PassiveTurnCalcRemove

Behavior Start of Turn Passive Orders
1 - Enlightenment

Behavior On Hit Passive Orders
1 - Retaliation

Changed Or Added Things from Godot
- Weather Interact Object
- Minions behave like other characters (i.e. have passives and utitlity elements can be used on them
