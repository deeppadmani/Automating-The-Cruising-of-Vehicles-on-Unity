using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
        public const float STEERING_CONTROL_IDX0 = 1f;
        public const float STEERING_CONTROL_OFFSET_LEFT = -0.2f;
        public const float STEERING_CONTROL_OFFSET_RIGHT = 0.2f;
        public const float STEERING_CONTROL_NORMAL = 0f;
        public const float STEERING_CONTROL_FAST_LEFT = -1f;
        public const float STEERING_CONTROL_SLOW_RIGHT = 1f;

        public const float TX_CONTROL_NORMAL = 0f;
        public const float TX_CONTROL_WHILE_CROSSING_BRIDGE = 55.6f;

        public const float STEERING_CONTROL_FAST_LEFT_2F = -2f;
        public const float STEERING_CONTROL_SLOW_RIGHT_2F = 2f;
        public const float COMPASS_NEG_OFFSET_NEAR_NORTH = -0.5f;
        public const float COMPASS_POS_OFFSET_NEAR_NORTH = 0.5f;

        public const int GRASS = 255;
        public const int BRIDGE = 33;
        public const float STATE_INIT_DIRECTION_CONTROL = 0;
        public const float STATE_VEHICAL_DIRECTION_DONE = 1;
        public const float STATE_READY_FOR_STRAIGHT_MOVE = 2;
        public const float STATE_CHECK_FOR_GRASS = 3;
        public const float STATE_GRASS_DETACTED = 4;
        public const float STATE_READY_FOR_LEFT_MOVE = 5;
        public const float STATE_LEFT_MOVE_DONE = 6;
        public const float STATE_READY_TO_MOVE_TOWARDS_BRIDGE = 7;
        public const float STATE_GRASS_DETACTED_NEAR_BRIDGE = 8;
        public const float STATE_READY_FOR_RIGHT_MOVE = 9;
        public const float STATE_RIGHT_MOVE_DONE = 10;
        public const float STATE_BOAT_ALARM_DETACTED = 11;
        public const float STATE_TX_CONTROL_AND_READY_MOVE_TOWARDS_DEST = 12;
        public const float STATE_MOVE_TOWARDS_DEST = 13;
        public const float STATE_DESTINATION_STOP = 14;

        public const float COMPASS_NEG_91_DEGREE = -91f;
        public const float COMPASS_NEG_89_DEGREE = -89f;
        public const float COMPASS_NEG_1_DEGREE = -1f;
        public const float COMPASS_POS_1_DEGREE = 1f;

        public const float BOAT_ALARM_DETACTED = 41f;
        public const float NO_ALARM_DETACTED = 0;

        public const float SPEED_10F = 10f;
        public const float SPEED_2F = 2f;
}



public class InitialDirectionControl : TaskInterface
{
    public void Execute(DeviceRegistry devices) 
    {
        if (Constants.STATE_INIT_DIRECTION_CONTROL == devices.memory[0])
        {
            if (devices.compass[0] > Constants.COMPASS_POS_OFFSET_NEAR_NORTH)
            {
                // Left
                devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
                devices.steeringControl[1] = Constants.COMPASS_NEG_OFFSET_NEAR_NORTH;
            }
            else if (devices.compass[0] < Constants.COMPASS_NEG_OFFSET_NEAR_NORTH)
            {
                // Right
                Debug.Log($"neg Compass["+ devices.compass[0]+ "]");
                devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
                devices.steeringControl[1] = Constants.COMPASS_POS_OFFSET_NEAR_NORTH;
            }
            else
            {
                devices.steeringControl[0] = Constants.STEERING_CONTROL_NORMAL;
            }

            if((devices.compass[0] > Constants.COMPASS_NEG_OFFSET_NEAR_NORTH) && 
                (devices.compass[0] < Constants.COMPASS_POS_OFFSET_NEAR_NORTH))
            {
                devices.memory[0] = Constants.STATE_VEHICAL_DIRECTION_DONE;
            }
        }

        if((devices.compass[0] > Constants.COMPASS_NEG_OFFSET_NEAR_NORTH) && 
            (devices.compass[0] < Constants.COMPASS_POS_OFFSET_NEAR_NORTH) && 
            (Constants.STATE_VEHICAL_DIRECTION_DONE == devices.memory[0]))
        {
            devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.steeringControl[1] = Constants.STEERING_CONTROL_NORMAL;
            devices.memory[0] = Constants.STATE_READY_FOR_STRAIGHT_MOVE;
        }

        if(devices.memory[0] > Constants.STATE_VEHICAL_DIRECTION_DONE && 
            devices.memory[0] < Constants.STATE_GRASS_DETACTED)
        {
            devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;
            devices.steeringControl[1] = Constants.STEERING_CONTROL_NORMAL;
            if(Constants.GRASS == devices.pixels[6,14,1])
            {
                // grass on right
                devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;;
                devices.steeringControl[1] = Constants.STEERING_CONTROL_FAST_LEFT;
            }
            if(Constants.GRASS == devices.pixels[6,1,1])
            {
                // grass on left
                devices.steeringControl[0] = Constants.STEERING_CONTROL_IDX0;;
                devices.steeringControl[1] = Constants.STEERING_CONTROL_SLOW_RIGHT;
            }
        }
    }
}
