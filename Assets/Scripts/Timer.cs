using System;
using UnityEngine;
using Zenject;

public interface ITimer
{
    public void RunTimer(float length, Action action);

    public void RunOnceTimer(float length, Action action);
}

public class Timer : ITimer
{
    public float TimeRemaining { get; set; } = 0;

    public void RunTimer(float length, Action action)
    {
        if (TimeRemaining == 0)
        {
            TimeRemaining = length;
        }

        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0)
        {
            action.Invoke();
            TimeRemaining = length;
        }
    }

    public void RunOnceTimer(float length, Action action)
    {
        if (TimeRemaining == 0)
        {
            TimeRemaining = length;
        }

        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0)
        {
            action.Invoke();
            return;
        }
    }
}