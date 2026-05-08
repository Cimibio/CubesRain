using System;
using System.Collections;
using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    private Coroutine _countdownCoroutine;

    public event Action Expired;

    private void OnDisable()
    {
        StopTimer();
    }

    public void StartTimer(float lifetime)
    {
        StopTimer();

        if (lifetime > 0)
            _countdownCoroutine = StartCoroutine(Countdown(lifetime));
    }

    public void StopTimer()
    {
        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
            _countdownCoroutine = null;
        }
    }

    private IEnumerator Countdown(float delay)
    {
        yield return new WaitForSeconds(delay);
        Expired?.Invoke();
    }

}