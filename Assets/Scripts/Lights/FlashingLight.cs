using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


// Flashing Light Script using Tween to Sequence. Works with any light as it simply Tweens between intensity.
[RequireComponent(typeof(Light))]
public class FlashingLight : MonoBehaviour {
    [SerializeField]
    [Tooltip("Minimum intensity between which to animate the light.")]
    [Min(0.01f)]
    private float minIntensity = 7.0f;

    [SerializeField]
    [Tooltip("Maximum intensity between which to animate the light.")]
    private float maxIntensity = 10.0f;

    [SerializeField]
    [Tooltip("Duration of one cycle")]
    [Min(0.001f)]
    private float oneCycleDuration = 1.0f;

    [SerializeField]
    [Tooltip("The min intensities will randomly add onto the set minIntensity to achieve a variation")]
    private float minVariation = 0.0f;

    [SerializeField]
    [Tooltip("The max intensities will randomly add onto the set maxIntensity to achieve a variation")]
    private float maxVariation = 0.0f;

    [SerializeField]
    [Tooltip("Easing of the light")]
    private Ease easeType = Ease.OutFlash; // Outflash gives a heart beat type feel

    private Light _light;

    private Sequence _flashingLightLoop;

    private void Awake() {
        _light = GetComponent<Light>();

        _flashingLightLoop = DOTween.Sequence();
        // Set to minimum
        _light.intensity = minIntensity;
        // Set sequence to go max and then min, with random range in between, set infinite looping, set ease type

        _flashingLightLoop
            .AppendCallback(LightToMax)
            .AppendInterval(oneCycleDuration / 2.0f)
            .AppendCallback(LightToMin)
            .AppendInterval(oneCycleDuration / 2.0f)
            .SetLoops(-1)
            .SetEase(easeType);
    }

    // Start is called before the first frame update
    void Start() {
        _flashingLightLoop.Play();
    }

    void LightToMax() {
        _light.DOIntensity(Random.Range(maxIntensity, maxIntensity + maxVariation), oneCycleDuration / 2);
    }

    void LightToMin() {
        _light.DOIntensity(Random.Range(minIntensity, minIntensity + minVariation), oneCycleDuration / 2);
    }
}