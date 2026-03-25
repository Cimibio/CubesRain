using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Detector), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;
    private Detector _detector;
    private float _lifetime;

    private Coroutine _lifetimerCountdownCoroutine;

    public event Action<Cube> Expired;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _detector = GetComponent<Detector>();
    }

    private void OnEnable()
    {
        _detector.Collided += ActivateLifetimer;
    }

    private void OnDisable()
    {
        _detector.Collided -= ActivateLifetimer;
    }

    public void Init(float lifetime)
    {
        if (_lifetimerCountdownCoroutine != null)
        {
            StopCoroutine(_lifetimerCountdownCoroutine);
            _lifetimerCountdownCoroutine = null;
        }

        _lifetime = lifetime;
        _detector.Init();
        _colorChanger.SetWhiteColor();
    }

    private void ActivateLifetimer()
    {
        _colorChanger.SetRandomColor();
        _lifetimerCountdownCoroutine = StartCoroutine(CountdownLifetime(_lifetime));
    }

    private IEnumerator CountdownLifetime(float delay)
    {
        yield return new WaitForSeconds(delay);
        Expired?.Invoke(this);
    }
}