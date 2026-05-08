using System;
using UnityEngine;

[RequireComponent(typeof(Detector), typeof(ColorChanger), typeof(LifeTimer))]
public class Cube : MonoBehaviour
{
    private ColorChanger _colorChanger;
    private Detector _detector;
    private LifeTimer _lifeTimer;
    private float _lifetime;

    public event Action<Cube> Expired;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _detector = GetComponent<Detector>();
        _lifeTimer = GetComponent<LifeTimer>();
    }

    private void OnEnable()
    {
        _detector.Collided += ChangeColor;
        _lifeTimer.Expired += Disapear;
    }

    private void OnDisable()
    {
        _detector.Collided -= ChangeColor;
        _lifeTimer.Expired -= Disapear;
    }

    public void Init(float lifetime)
    {
        _lifetime = lifetime;
        _lifeTimer.StopTimer();
        _detector.Init();
        _colorChanger.SetWhiteColor();
    }

    private void ChangeColor()
    {
        _colorChanger.SetRandomColor();
        _lifeTimer.StartTimer(_lifetime);
    }

    private void Disapear()
    {
        Expired?.Invoke(this);
    }
}