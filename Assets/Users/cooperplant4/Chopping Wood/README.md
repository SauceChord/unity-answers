# How can I make it so that inputs only function once per second?

*[Original URL](https://answers.unity.com/questions/1839821/how-can-i-make-it-so-that-inputs-only-function-onc.html)*

I am trying to get a collision method to work where when the player is holding the chopping key (space) and is colliding with a tree it will increase the hit counter for the tree every second and after the tree is chopped will add score. However my problem is that when I old the space bar there are far too many inputs. So what I've been racking my brain for is some way to only allow the if statement in the collision method to run once per second. I tried a timestamp but it would only increase the chopping counter once, then you would have to recollide with the object. I am using GetKey for the input, and OnCollisionStay2D for the collision.
