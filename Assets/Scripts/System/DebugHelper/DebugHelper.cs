#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public static class DebugHelper
{
    #region CriticalNullReferenceLogger
    public static void CriticalNullReferenceLogger(Component scriptName, Type requiredComponentName, string location, string codeSnippet, GameObject gameObject, bool condition)
    {
        if (!condition)
        {
            string requiredComponent = requiredComponentName != null ? requiredComponentName.Name : "Unknown Component";
            UnityEngine.Debug.LogError($"This should not activate. If it does, please refer to the Debug.Assert listed below. Script name: {scriptName.GetType().Name}");
            UnityEngine.Debug.Assert(condition, $"Script: {scriptName.GetType().Name}. <color=red>Required Component: {requiredComponent}.</color> <color=blue>Location: {location},</color> <color=purple>Code Snippet: {codeSnippet}.</color> <color=green>Game Object Name: {gameObject.name}</color>", gameObject);
        }
    }
    #endregion

    #region CriticalExceptionLogger
    public static void CriticalExceptionLogger(Exception exception, Component scriptName, string location, GameObject gameObject)
    {
        UnityEngine.Debug.LogError($"Critical Error in <color=green>{gameObject.name}</color>: <color=red>{exception.Message}. This is in the script: <color=purple>{scriptName.GetType().Name}</color>, with this part of the code: <color=blue>{location}</color>", gameObject);
        UnityEngine.Debug.LogException(exception, gameObject);
    }
    #endregion

    #region CriticalOutOfRangeLogger
    public static void CriticalOutOfRangeLogger(int index, int length, Component scriptName, string location, GameObject gameObject)
    {
        UnityEngine.Debug.LogError($"Critical Error in <color=green>{gameObject.name}</color>: Index <color=red>{index}</color> is out of range for length <color=red>{length}</color>. This is in the script: <color=purple>{scriptName.GetType().Name}</color>, with this part of the code: <color=blue>{location}</color>", gameObject);
    }
    #endregion

    #region CriticalArgumentNullLogger
    public static void CriticalArgumentNullLogger(string argumentName, Component scriptName, string location, GameObject gameObject)
    {
        UnityEngine.Debug.LogError($"Critical Error in <color=green>{gameObject.name}</color>: Argument <color=red>{argumentName}</color> cannot be null. This is in the script: <color=purple>{scriptName.GetType().Name}</color>, with this part of the code: <color=blue>{location}</color>", gameObject);
    }
    #endregion

    #region CriticalLogger
    public static void CriticalLogger(string message, Component scriptName, string location, GameObject gameObject)
    {
        UnityEngine.Debug.LogError($"Critical Error in <color=green>{gameObject.name}</color>: {message}. This is in the script: <color=purple>{scriptName.GetType().Name}</color>, with this part of the code: <color=blue>{location}</color>", gameObject);
    }
    #endregion

    #region WarningLogger
    public static void WarningLogger(string message, Component scriptName, string location, GameObject gameObject)
    {
        UnityEngine.Debug.LogWarning($"Warning, on the <color=green>{gameObject.name}</color>, {message}. This is in the script: <color=purple>{scriptName.GetType().Name}</color>, with this part of the code: <color=blue>{location}</color>", gameObject);
    }
    #endregion

    #region InfoLogger
    public static void InfoLogger(string message, Component scriptName, string location, GameObject gameObject, bool showInConsole)
    {
        if (showInConsole)
        {
            UnityEngine.Debug.Log($"Info, on the {gameObject.name}, {message}. This is in the script: {scriptName.GetType().Name}, with this part of the code: {location}", gameObject);
        }
    }
    #endregion
}
#endif