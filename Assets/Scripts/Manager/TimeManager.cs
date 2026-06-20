public class TimeManager : Singleton<TimeManager>
{
    public float timeScale {get; private set;}

    public void SetTimeScale(float value)
    {
        timeScale = value;
    }
}
