using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingTask : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
   {
        if(Jobs.STATE_READY_FOR_PARKING == devices.memory[0])
        {
            devices.steeringControl[0] = 1f;
            devices.steeringControl[1] = 0.5f;    //Right for parking
            
            if((28f > devices.gps[1] && 25f < devices.gps[1]) ||      // lane-1
               (7.5f > devices.gps[1] && 6.5f < devices.gps[1]) ||      // Lane-3 
               (-12.5f > devices.gps[1] && -13.5f < devices.gps[1]))    //Lane-5 
            {
                if(devices.compass[0] > 179f)
                {
                     devices.steeringControl[0] = 1f;
                     devices.steeringControl[1] = 0f;
                     devices.memory[0] = Jobs.STATE_PARKING_EGO_VEHICLE;   //Right done for parking
                }
            }
            else
            {
                if (devices.compass[0] > 0)
                {
                     devices.steeringControl[0] = 1f;
                     devices.steeringControl[1] = 0f;
                     devices.memory[0] = Jobs.STATE_PARKING_EGO_VEHICLE;   //Right done for parking
                }
            }
        }
        if(17 == devices.memory[0])
        {
            devices.speedControl[0] = 1f;
            devices.speedControl[1] = 1f;

            if(3 > devices.lidar[0])
            {
                devices.speedControl[0] = 1f;
                devices.speedControl[1] = 0f;
                devices.memory[0] = Jobs.STATE_PARKING_DONE;
            }
        }
   }
}
