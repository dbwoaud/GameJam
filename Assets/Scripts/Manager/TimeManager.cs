using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public float timeScale {get; private set;} = 1f;
    public float deltaTime => timeScale * Time.deltaTime;

    public void SetTimeScale(float value)
    {
        timeScale = value;
    }
}
