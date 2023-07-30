using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmControl : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {    
        if (Constants.STATE_RIGHT_MOVE_DONE == devices.memory[0])
        {     
            if (Constants.BOAT_ALARM_DETACTED == devices.microphone[0])
            {
                devices.memory[0] = Constants.STATE_BOAT_ALARM_DETACTED;
            }
        }

        if (Constants.STATE_BOAT_ALARM_DETACTED == devices.memory[0])
        {    
            if (Constants.NO_ALARM_DETACTED == devices.microphone[0])
            {
                devices.memory[0] = Constants.STATE_TX_CONTROL_AND_READY_MOVE_TOWARDS_DEST;
            }
        }
    }
}
