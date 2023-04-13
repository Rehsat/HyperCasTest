using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BounceScaleTweener : MonoBehaviour
{
    [SerializeField] private bool _playOnEnable;
    [SerializeField] private bool _destroyObComplete;
    [SerializeField] private float _startScale;
    [SerializeField] private TweenScale _bounceScale;
    [SerializeField] private TweenScale _endScale;
    private Sequence _sequence;

    private void OnEnable()
    {
        if(_playOnEnable)
            PlayBounceAnimation();
    }

    public void PlayBounceAnimation()
    {
        _sequence?.Kill();

        _sequence = DOTween.Sequence();
        var startScale = Vector3.one * _startScale;
        var bounceScale = Vector3.one * _bounceScale.Scale;
        var endScale = Vector3.one * _endScale.Scale;

        transform.localScale = startScale;
        var firstTween = transform.DOScale(bounceScale, _bounceScale.TimeToScale);
        var secondTween = transform.DOScale(endScale, _endScale.TimeToScale);
        _sequence.Append(firstTween).Append(secondTween).OnComplete(() =>
        {
            if (_destroyObComplete)
            {
                _sequence.Kill();
                Destroy(gameObject);
            }
        });

        _sequence.Play();

    }
}
[Serializable]
public class TweenScale
{
    [SerializeField] private float _scale;
    [SerializeField] private float _timeToScale;

    public float Scale => _scale;

    public float TimeToScale => _timeToScale;
}
