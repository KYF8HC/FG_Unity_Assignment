# FG_Unity_Assignment
Péter Gulyás  

GitHub link: https://github.com/KYF8HC/FG_Unity_Assignment 

Unity Version: 2022.3.8f1  

What I wanted to achive was a car controller similar to the one on the third video. 

You must open the Game Scene to be able to run the game. I think the only packeges that I use are the new InputSystem, TextMeshPro and the AI Navigation. But I think these should be installed automatically.

When you start the game, you will see the main menu. Whenever you press "Play," it will open the map selection screen (it only has 2 maps), where you can choose which map you would like to play on. There is an "Options" option as well, but I did not manage to implement an Options menu. The plan was to be able to rebind keys.  

You can control the car using WASD, and the space bar is used for braking. Additionally, the "R" key allows you to reset the car's rotation and position it at the most recent checkpoint you have passed. 

Using the "Escape" key, you can open the Pause menu, and the game will pause as well. From there, you have the options to return to the main menu, restart the game, or quit. Upon completing all the laps, the game will exit to the main menu (though I had initially planned to implement an end-game scene with leaderboards, I ran out of time).  

Having previously developed games similar to the assignment's requirements in the past, I aimed to explore something new and something that I am not familiar with. Although it was acknowledged that we did not need to implement anything overly complex and were encouraged not to overextend, I still wanted to create a bit more complex car controller. Nonetheless, I really enjoyed the process, and while there's still room for improvement, I believe it ultimately evolved into an entertaining and fun arcade-style car controller. With the assistance of the following sources, the implementation process was relatively straightforward.  

I also incorporated a basic AI system inspired by the things we covered in the Pathfinding class. The checkpoint system which I used to the AI's navigation as well, was influenced by Code Monkey's checkpoint system video, handles the lap tracking in the game.   

I used Synty Studios Simple Racer assets.  

Resources:  

https://www.youtube.com/watch?v=ZwMa9g7lvT8 - This is a series of 3 episodes, but I mainly used this part  

https://www.youtube.com/watch?v=CdPYlj5uZeI - I used this video for the car physics  

https://www.youtube.com/watch?v=CBgtU9FCEh8 - This was the video where I got the idea from  

https://www.youtube.com/watch?v=IOYNg6v9sfc&t=837s - Checkpoint system  
