using System;
using UnityEngine;

[RequireComponent(typeof(LifeTimer), typeof(Exploder), typeof(ColorChanger))]
[RequireComponent(typeof(TransparencyChanger))]
public class Bomb : MonoBehaviour
{
    private LifeTimer _lifeTimer;
    private Exploder _exploder;
    private ColorChanger _colorChanger;
    private TransparencyChanger _transparencyChanger;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _lifeTimer = GetComponent<LifeTimer>();
        _exploder = GetComponent<Exploder>();
        _colorChanger = GetComponent<ColorChanger>();
        _transparencyChanger = GetComponent<TransparencyChanger>();
    }

    private void OnEnable()
    {
        _lifeTimer.Expired += Explode;
    }

    private void OnDisable()
    {
        _lifeTimer.Expired -= Explode;
    }

    public void Init(Vector3 position, float lifeTime, float explosionRadius, float explosionForce)
    {

        transform.position = position;
        _colorChanger.SetBlackColor();

        _exploder.Init(explosionRadius, explosionForce);
        _lifeTimer.StartTimer(lifeTime);

        _transparencyChanger.ResetAlpha();
        _transparencyChanger.MakeInvisible(lifeTime);
    }

    private void Explode()
    {
        _exploder.Explode();
        Exploded?.Invoke(this);
    }
}