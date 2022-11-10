# Multi-platform-Game
ECS7003P Assessment Queen Mary

## How to play

The game is a tower defense game with a few twists. Firstly, your own placed towers will be attacked by enemies and can be destroyed. In your inventory in the bottom right of the screen, you will start the game with 1 of each tower type, and receive 1 of each type after each wave defeated. These tower types are (from left to right in the inventory):
- Healer (heals adjacent turrents 50 health per second)
- Archer (fast attacks at a medium range, and relatively low health)
- Cannon (slow attacks at a high range, medium health)

On top of this, you will have the option to open a skill tree. Here are 4 branches, each representing one of the four mages (physical, imaginary, magic and mechanic). For the prototype, only one of these is enabled, and does not yet do special kind of damage, but just normal damage. Levelling the first skill in this skill branch will unlock that mage tower, which has a default place on the map, and cannot be attacked by enemies. It also has a maximuim range, covering the entire map. Levelling the next skills increases that towers attack speed, but will, in the future, unlock actual skills for the mage. The mage is currently represented as a golden tower.

The currency to buy these skills is skill points. You start with 50 skill points, which is enough to unlock one mage at the start. You gain skill points by killing enemies (currently 20 per enemy) and lose skill points (currently 20 per enemy) when enemies reach your base (blue cube). When you lose skill points that would take you below 0, you lose the last skill you acquired (and gain half its cost as skill points). If you lose skill points that would take you below 0 and have no more skills unlocked, you lose the game. When you purchase the final skill (can only be purchased once all skill branches are bought), you win the game.

There are 2 types of enemies, blue and red:
- Blue enemies are slow and do not have a lot of Health
- Red enemies are faster with more Health

To play, click on one of the towers on the inventory, or unlock the mage by opening the skill tree and clicking on Skill 1 on the top left. Press the "Next Wave" button to spawn the next wave. Waves get increasingly more difficult (until wave 5, then they stay constant) and also start spawning on different, and later multiple paths. 

## External Sources

Some code used and adapted from https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0 , https://github.com/Brackeys/Tower-Defense-Tutorial

Code used for tags:
https://answers.unity.com/questions/1470694/multiple-tags-for-one-gameobject.html
