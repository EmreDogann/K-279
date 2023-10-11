using System.Collections.Generic;
using Lights;
using MyBox;
using ScriptableObjects;
using UnityEngine;

// Modified by Emre Dogan 2023 - For K-279
// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
///     Component which will flicker a linked light while active by changing its
///     intensity between the min and max values given. The flickering can be
///     sharp or smoothed depending on the value of the smoothing parameter.
///     Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class LightFlickerEffect : MonoBehaviour, ILightEffect
{
    public bool Enabled = true;
    [Tooltip("External light to flicker; you can leave this null if you attach script to a light")]
    public Light lightObj;
    [DisplayInspector]
    [SerializeField] private LightFlickerSO normalFlicker;
    [DisplayInspector]
    [SerializeField] private LightFlickerSO explosionFlicker;

    private LightFlickerSO _currentFlicker;

    // Continuous average calculation via FIFO queue
    // Saves us iterating every time we update, we just change by the delta
    private Queue<float> _smoothQueue = new Queue<float>();
    private float _lastSum;
    private float _currentTimeDelay;
    private float _currentTimeFlick;
    private float _currentDuration;
    private float _startingIntensity;

    private float _gain = 1.0f;

    /// <summary>
    ///     Reset the randomness and start again. You usually don't need to call
    ///     this, deactivating/reactivating is usually fine but if you want a strict
    ///     restart you can do.
    /// </summary>
    public void Reset()
    {
        _smoothQueue.Clear();
        _lastSum = 0;
    }

    private void OnEnable()
    {
        if (explosionFlicker != null)
        {
            SubmarineSoundScape.ExplosionTriggered += OnExplosionTriggered;
        }
    }

    private void OnDisable()
    {
        if (explosionFlicker != null)
        {
            SubmarineSoundScape.ExplosionTriggered -= OnExplosionTriggered;
        }
    }

    private void Start()
    {
        _gain = 1.0f;
        _currentFlicker = normalFlicker;
        _smoothQueue = new Queue<float>();

        // External or internal light?
        if (lightObj == null)
        {
            lightObj = GetComponent<Light>();
            _startingIntensity = lightObj.intensity;
        }
    }

    public void EnableEffect()
    {
        Enabled = true;
    }

    public void DisableEffect()
    {
        Enabled = false;
        if (_currentFlicker != null)
        {
            foreach (LightFlickerAudio flickerAudio in _currentFlicker.flickerAudios)
            {
                flickerAudio.audio.StopAll();
            }
        }
    }

    private void Update()
    {
        if (lightObj == null || _currentFlicker == null || !Enabled)
        {
            return;
        }

        _currentTimeDelay += Time.deltaTime;
        if (_currentTimeDelay < _currentFlicker.delay)
        {
            return;
        }

        if (_currentDuration <= 0.0f)
        {
            _currentDuration = Random.Range(_currentFlicker.duration.Min, _currentFlicker.duration.Max);
        }

        // Check if current iteration is over.
        if (_currentTimeDelay > _currentFlicker.delay + _currentDuration)
        {
            lightObj.intensity = _startingIntensity;
            _currentTimeDelay = 0.0f;
            _currentDuration = 0.0f;
            _gain = 1.0f;
            _currentFlicker = normalFlicker;
            return;
        }

        // Pop off an item if too big
        while (_smoothQueue.Count >= _currentFlicker.smoothing)
        {
            _lastSum -= _smoothQueue.Dequeue();
        }

        _currentTimeFlick += Time.deltaTime;
        if (_currentTimeFlick < _currentFlicker.frequency)
        {
            return;
        }

        _currentTimeFlick = 0.0f;

        // Generate random new item, calculate new average
        float newVal = Random.Range(_currentFlicker.minIntensity, _currentFlicker.maxIntensity * _gain);
        _smoothQueue.Enqueue(newVal);
        _lastSum += newVal;

        float prevIntensity = lightObj.intensity;
        // Calculate new smoothed average
        lightObj.intensity = _lastSum / _smoothQueue.Count;

        float diff = Mathf.Abs(lightObj.intensity - prevIntensity);
        foreach (LightFlickerAudio flickerAudio in _currentFlicker.flickerAudios)
        {
            if (diff >= flickerAudio.threshold)
            {
                // Play zap audio
                flickerAudio.audio.Play(transform.position);
            }
        }
    }

    private void OnExplosionTriggered(float explosionStrength)
    {
        // _gain = explosionStrength;
        _currentFlicker = explosionFlicker;

        _smoothQueue.Clear();
        _lastSum = 0;
    }
}