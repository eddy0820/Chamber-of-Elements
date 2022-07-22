# Chamber of Elements

- re do how i do re roll elements and using the array
- re do "reroll specific" code
- "miss" ui element
- "immune" ui element
- drop down menu for dynamic passives to see what theyve got stacked on top and how many turns for each stack
- for elements that deal damage two seperate times you need to delay the hit markers with a queue and coroutines maybe
- screen shake settings
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


- relics
- visuals
- hover tooltip for elements
- create enemies that existed
- menus

- Mana System
- Ability for minion and element slots
- RogueLike
- book

Sprites Needed
- Retaliation Passive
- Enlightenment Passive
- Spell Powers
- Potencies
- Weather Affinites
- Weather Potencies
- Weathers

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
