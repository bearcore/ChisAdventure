using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Lerp
{
    public static Coroutine To(float duration, UnityAction<float> t,
        float target = 1f, bool smooth = false)
    {
        return CoroutineHelper.StartGlobalCoroutine(ToEnumerate(duration, t, target, smooth));
    }

    public static IEnumerator ToEnumerate(float duration, UnityAction<float> t,
        float target = 1f, bool smooth = false)
    {
        yield return FromToEnumerate(duration, t, 0f, target, smooth);
    }

    public static Coroutine FromTo(float duration, UnityAction<float> t,
        float start = 0f, float target = 1f, bool smooth = false)
    {
        return CoroutineHelper.StartGlobalCoroutine(FromToEnumerate(duration, t, start, target, smooth));
    }

    public static IEnumerator FromToEnumerate(float duration, UnityAction<float> t,
        float start = 0f, float target = 1f, bool smooth = false)
    {
        var elapsedTime = 0f;
        t(start);
        while (elapsedTime < duration)
        {
            // Yield here
            yield return null;

            elapsedTime += Time.deltaTime;
            if (smooth)
                t(Mathf.SmoothStep(start, target, elapsedTime / duration));
            else
                t(Mathf.Lerp(start, target, elapsedTime / duration));
        }
        t(target);
    }

    public static Coroutine Delay(float duration, UnityAction onDone)
    {
        return CoroutineHelper.StartGlobalCoroutine(DelayEnumerate(duration, onDone));
    }

    public static IEnumerator DelayEnumerate(float duration, UnityAction onDone)
    {
        yield return new WaitForSeconds(duration);
        onDone();
    }
}


public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper _instance;

    private static void EnsureExists()
    {
        if (_instance == null)
        {
            var go = new GameObject("CoroutineHelper");
            _instance = go.AddComponent<CoroutineHelper>();
            DontDestroyOnLoad(go);
        }
    }

    public static Coroutine StartGlobalCoroutine(IEnumerator _toRun)
    {
        EnsureExists();
        return _instance.StartCoroutine(_toRun);
    }

    public static void StopGlobalCoroutine(Coroutine coroutine)
    {
        _instance.StopCoroutine(coroutine);
    }
}