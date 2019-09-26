using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    [SerializeField]
    private Transform umbrella;

    // Update is called once per frame
    void Update()
    {
        bool isRaining = FindObjectOfType<RainScript>().RainFallParticleSystem.emission.enabled;
        umbrella.gameObject.SetActive(isRaining);
    }
}
