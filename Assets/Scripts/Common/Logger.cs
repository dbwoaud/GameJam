using System.Diagnostics;

public static class Logger
{
    [Conditional("UNITY_EDITOR")]
    public static void Log(string msg)
    {
        UnityEngine.Debug.Log(msg);    
    }

    public static void LogError(string msg)
    {
        UnityEngine.Debug.LogError(msg);    
    }
}
