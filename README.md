# 1st_Personal_Project_Game
# 2023/02/13 Start
# 2023/02/14 Make UI v0.0.1
# 2023/02/17 Week 1 develop End v0.0.2             make controller and bullet pooling system and scrolling BackGround Complete
# 2023/05/26 Fixed an error FpsCounter was created repeatedly when returned to the Loading Scene 
# 2023/05/26 Fixed an error Fixed an error where the UI would be cropped if it was not 4:3
# 2023/05/26 If you keep calling WaitForSeconds as new when running a coroutine, you can call GC and reduce game performance in the long run, so use variable WaitForSeconds.
# 2023/05/26 I recognized that the enemy generation logic was very bad, and to improve it, we modified the enemy generation logic as a whole.
# 2023/05/26 Since it is very inefficient to update the state in the ui from the update, it is changed to a coroutine method.
# 2023/05/26 Fixed item not having exception handling for pooling
# next I need make enemy pattern
