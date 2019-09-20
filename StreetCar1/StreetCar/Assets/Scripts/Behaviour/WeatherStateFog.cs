using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BehaviourMachine;
using DigitalRuby.RainMaker;

public class WeatherStateFog : StateBehaviour
{
    public GameObject rainPrefab;

    public float duration = 8f;

    // Called when the state is enabled
    private float time;

    async void OnEnable()
    {

        RainScript rainScript = FindObjectOfType<RainScript>();
        if (rainScript)
        {
            ParticleSystem.EmissionModule a = rainScript.RainFallParticleSystem.emission;
            a.enabled = false;

            ParticleSystem.EmissionModule b = rainScript.RainExplosionParticleSystem.emission;
            b.enabled = false;

            ParticleSystem.EmissionModule c = rainScript.RainMistParticleSystem.emission;
            c.enabled = false;

            ParticleSystem.EmissionModule d = rainScript.CloudParticleSystem.emission;
            d.enabled = false;

            ParticleSystem.EmissionModule e = rainScript.FogParticleSystem.emission;
            e.enabled = true;
        }

        Debug.Log("Started *State*");
    }

    // Called when the state is disabled
    void OnDisable()
    {
        RainScript rainScript = FindObjectOfType<RainScript>();
        if (rainScript)
        {
            ParticleSystem.EmissionModule a = rainScript.RainFallParticleSystem.emission;
            a.enabled = false;

            ParticleSystem.EmissionModule b = rainScript.RainExplosionParticleSystem.emission;
            b.enabled = false;

            ParticleSystem.EmissionModule c = rainScript.RainMistParticleSystem.emission;
            c.enabled = false;

            ParticleSystem.EmissionModule d = rainScript.CloudParticleSystem.emission;
            d.enabled = false;

            ParticleSystem.EmissionModule e = rainScript.FogParticleSystem.emission;
            e.enabled = false;
        }

        Debug.Log("Stopped *State*");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > duration)
        {
            SendEvent("FogDissipating");
            time = 0f;
        }

    }
}


