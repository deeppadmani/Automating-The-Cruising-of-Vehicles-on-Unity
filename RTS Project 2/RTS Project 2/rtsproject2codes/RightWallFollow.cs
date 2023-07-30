using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWallFollow : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {
        if (Jobs.STATE_MOVING_FORWARD_WITH_3F_SPEED == devices.memory[0])
        {  
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 3f;
            devices.memory[0] = Jobs.STATE_OBJECT_DETECTION;    //checking for object
        }
        if (Jobs.STATE_OBJECT_DETECTION == devices.memory[0])
        {      
           if(10 > devices.lidar[0] )
           {
                devices.speedControl[0] = 1f;
                devices.speedControl[1] = 0f;
                if((28f > devices.gps[1] && 25f < devices.gps[1]) ||      // lane-1
                  (7.5f > devices.gps[1] && 6.5f < devices.gps[1]) ||      // Lane-3 
                  (-12.5f > devices.gps[1] && -13.5f < devices.gps[1])    //Lane-5  
                )
                {
                    devices.memory[0] = Jobs.STATE_OBJECT_DETECTED;  //object detectd
                }
                else
                {
                    devices.memory[0] = Jobs.STATE_TURNING_LEFT;  //object detectd                
                }
           }
        }


        if (Jobs.STATE_MOVING_FORWARD_WITH_1F_SPEED == devices.memory[0])         //lane change 
        {   
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 1.5f;   
            devices.memory[0] = Jobs.STATE_LANE_DETECTION;
        }

        if (Jobs.STATE_LANE_DETECTION == devices.memory[0])
        {   

            if(14 > devices.gps[1] &&  13.5 < devices.gps[1]||    //Lane-2
              (-6 > devices.gps[1] && -6.5 < devices.gps[1]) ||    //Lane-4
              (-26 > devices.gps[1] && -26.5 < devices.gps[1])    //Lane-6  Rubina changed
              )
            {
                    devices.memory[0] = Jobs.STATE_TURNING_RIGHT;         
                    devices.speedControl[0] = 1f;
                    devices.speedControl[1] = 0f;
            }
        }

        if(Jobs.STATE_U_TURN_1 == devices.memory[0])         // U-Turn state
        {
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 1f; 


            if(8 > devices.gps[1] && 7.5 < devices.gps[1] || 
               (-12 > devices.gps[1] && -13 < devices.gps[1]))
            {
                devices.speedControl[0] = 1f;
                devices.speedControl[1] = 0f;
                devices.memory[0] = Jobs.STATE_U_TURN_2;
            }
        }
    }
}
