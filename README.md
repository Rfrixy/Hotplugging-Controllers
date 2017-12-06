# Hotplugging-Controllers


WHY?
While running and working with multiple joysticks, Unity can have upto 16 joysticks in use.
The problem here is that sometimes when re-plugging joysticks during gameplay, joystick number of the joysticks can sometimes be erratically assigned.
E.g. 4 joysticks connected, joystick1 is removed and re-plugged and is assigned the number 5.
Your code which uses joystick 1 as the input won't work anymore.



HOW?
This code will keep track of which number is assigned to which joystick in the array joys.
So for using this number, instead of writing 
"joystick 1 button 0"
get a reference to the input manager and use
"joystick "+InputManager.joys[0]+" button 0" 
for the first joystick. ( joys[3] for the fourht joystick ).

numPlayers is the number of joysticks that need to be mapped. If this number is higher than the number of joysticks connected, there will be a minor performance cost as the code will run once every second to try and detect new joysticks connected.
