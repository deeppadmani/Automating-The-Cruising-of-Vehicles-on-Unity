using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskList : MonoBehaviour
{
    protected TaskInterface[] tasks = new TaskInterface[] 
    {
        new Debug_Task(),
        new InitialDirectionControl(),
        new ReachDestinationControlTask(),
        new TurnLeft(),
        new TurnRight(),
        new AlarmControl()
    };

    public TaskInterface[] GetTasks() {
        return this.tasks;
    }
}
