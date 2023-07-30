using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeft : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {    
        if ((devices.compass[0] > Constants.COMPASS_NEG_91_DEGREE) && 
            (devices.compass[0] < Constants.COMPASS_NEG_89_DEGREE) && 
            (Constants.STATE_READY_FOR_LEFT_MOVE ==  devices.memory[0]))
        {
            devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.steeringControl[1] = Constants.STEERING_CONTROL_NORMAL;
            devices.memory[0] = Constants.STATE_LEFT_MOVE_DONE;
        }
        if (Constants.STATE_GRASS_DETACTED == devices.memory[0])
        {
            
            devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.steeringControl[1] = Constants.STEERING_CONTROL_FAST_LEFT;

            if((devices.compass[0] > Constants.COMPASS_NEG_91_DEGREE) && 
            (devices.compass[0] < Constants.COMPASS_NEG_89_DEGREE))
            {
                devices.memory[0] = Constants.STATE_READY_FOR_LEFT_MOVE;
            }
        }  
    }
}

