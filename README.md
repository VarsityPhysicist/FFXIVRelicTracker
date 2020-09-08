# FFXIVRelicTracker
This is a project for tracking Relic weapon progression in FFXIV. Each expansion of FXIV adds a new relic wepon with different tasks required to unlock them.

Release is here: https://github.com/VarsityPhysicist/FFXIVRelicTracker/releases/tag/v1.0

This is my first programming project and I didn't look into basing it on any specific version of Microsoft's .NET Core runtime, so unfortunately the latest version is required - you can download it here:https://dotnet.microsoft.com/download

Currently the following expansions are included in this: ARR

To use the application, download the files and run FFXIVRelicTracker.exe
  The main window can be dragged around from the top bar and can be expanded or shrunk by clicking and dragging from the bottom right corner.
  Create a character via the add character button. Click the save button on the main menu at any time to save the current progression of all characters that have been added.
  Once a character is selected via the dropdown, the options on the right of the window will allow viewing of the character's progression through different expansion's relics
  
  ARR Progression:
    Summary Tab:
      The summary tab is the default tab selected in ARR, it list the different stages for each weapon and each job.
      Each stage and job in the table has a button that is used to indicate progress, clicking the buttons completes or incompletes other options in the table.
        If a stage is completed by clicking a button, preceeding stages are completed
        If a stage is incompleted by clicking a button, following stages are incompleted
        If a stage is put into "Initiated" other jobs at that level that had "Initiated" as their status are set to N/A, since this stage can 
          only be worked on by a single job at a time
    
    Relic Tab:
      This tab is used to display when the stages are for the first step, called the relic stage
      Select a job via the dropdown box and the steps required for completion will display on the left and an 
        image will display showing the appropriate step's location to go to, since some point of the progression requires slaying of some creatures
      Clicking the mark completed button updates the job to have completed the Relic stage and this is updated in the Summary Tab
      
    Atma Tab:
      This tab is used to show where to get specific atma and how many are required for completing all remaining jobs
    
    Animus Tab:
      This tab is used for displaying the progress on completing books.
      Each book has creatures required to be slain, Leves to complete, FATEs to complete, and dungeons to complete.
      On the top of this tab, the books can be completed via checkboxes
      On the bottom of this tab there are several items:
        The current book being worked on is selected via a dropdown, as books are checked off from the top they are removed or added as necessary
        The creatures, Leves, FATEs, and dungeons to be completed are shown with checkboxes to indicate if they're completed or not
        Several radio buttons for available maps are located to the right of this section; as items are completed/incompleted, 
          the listing of maps that content is to be completed in updated as necessary
        Selecting a radio button displays a map image on the right
        Each task in a map will be shown by a colored circle on the map. The taks types are color-coded and match the outlining in the sections on the left of the window
        Completing a task can be done by clicking on the circle on the map or the checkboxes
        
    Novus Tab:
      This tab is used for displaying the progress on adding materia to the scrolls for completion
      A total of 75 materia is required to be added, for PLD and DRG classes I have the information on the max materia for each type that are to be affixed
        Other jobs, I do not have the max materia for each item, so their interface only allows adding of subtracting from the total
    
    Nexus Tab:
      This tab is used for displaying the progress on "collecting light" for the target weapon
      Collecting light is done by completing tasks with the weapon equipped; varying tasks give different levels of light, 
        but its most time effecient to run Tam-Tara Deepcroft since it only takes a few minutes to complete and is easiliy solo-able at level cap
      Click buttons to add the amount of "light" to the total or type in the current total if it is known
       -2000 light is needed, this was mistakenly not included
     
     Zodiac Braves Tab:
      This tab is used for displaying the progress on completing the required quests for this stage
      At the top right, the total currencies required for all remaining tasks is listed
      In the table, there is information on the quests to be completed - where to go and what to gather
        The items that are to be purchased or crafted may be done in advance
      
      Zodiac Zeta Tab:
        This tab is used for displaying the progress on "collecting light" for the target weapon like the Nexus stage
        This stage is similar to the Nexus stage, except instead of collecting 2000 light at onces, you collect
          (I think) 80 light 12 times
          
Don't forget to save progress before closing the application!
