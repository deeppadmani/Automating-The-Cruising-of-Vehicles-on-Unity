using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {
        if ((devices.compass[0] > Constants.COMPASS_NEG_1_DEGREE) && 
            (devices.compass[0] < Constants.COMPASS_POS_1_DEGREE) && 
            Constants.STATE_READY_FOR_RIGHT_MOVE == devices.memory[0])
        {
                devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
                devices.steeringControl[1] = Constants.STEERING_CONTROL_NORMAL;
                devices.memory[0] = Constants.STATE_RIGHT_MOVE_DONE;
        }
        if (Constants.STATE_GRASS_DETACTED_NEAR_BRIDGE == devices.memory[0])
        { 
            
            devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.steeringControl[1] = Constants.STEERING_CONTROL_SLOW_RIGHT;

            if(devices.compass[0] > Constants.COMPASS_NEG_1_DEGREE && devices.compass[0] < Constants.COMPASS_POS_1_DEGREE)
            {
                devices.memory[0] = Constants.STATE_READY_FOR_RIGHT_MOVE;
            }
        }  
    }
}
