
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    // To use this script properly, ensure that the directional light is always infront of the pivot point! 
    // This script will rotate the pivot point, and use a point along the circle of rotation to change the orientation of the directional light
    [Header("Settings")]
    [SerializeField]
    private float _pivotRadius;

    [SerializeField]
    private Transform _pivotPoint;

    [SerializeField]
    private float _pivotRotationSpeed;

    [SerializeField]
    private Gradient _colorGradient;

    [SerializeField]
    private Gradient _skyboxGradient;

    [Header("Components")]
    [SerializeField]
    private Light _directionalDayLight;

    [SerializeField]
    private Light _directionalNightLight;

    private bool _cycleEnabled = true;

    public float cycleProgress;

    private void Awake() 
    {
        _directionalDayLight.transform.LookAt(_pivotPoint.position);
        _directionalNightLight.transform.LookAt(_pivotPoint.position);
    }

    public void StopDayNightCycle() 
    {
        _cycleEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cycleEnabled)
        {
            // Rotate the pivot
            _pivotPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, _pivotPoint.rotation.eulerAngles.z - _pivotRotationSpeed * Time.deltaTime));

            // Use the right direction vector of the pivot to place a point at a certain radius away
            // This is basically using polar coordinates to make a circle
            // Scale only on x and y 
            Vector3 daytimePoint = _pivotPoint.position - new Vector3(_pivotPoint.right.x * _pivotRadius, _pivotPoint.right.y * _pivotRadius, _pivotPoint.right.z);
            Vector3 nighttimePoint = _pivotPoint.position + new Vector3(_pivotPoint.right.x * _pivotRadius, _pivotPoint.right.y * _pivotRadius, _pivotPoint.right.z);

            Debug.DrawLine(_pivotPoint.position, daytimePoint, Color.red);
            Debug.DrawLine(_pivotPoint.position, nighttimePoint, Color.blue);

            // Point the directional light at this point
            _directionalDayLight.transform.LookAt(daytimePoint);
            _directionalNightLight.transform.LookAt(nighttimePoint);

            cycleProgress = (360.0f - _pivotPoint.rotation.eulerAngles.z) / 360.0f;

            Color lightColor = _colorGradient.Evaluate(cycleProgress);
            Color skyboxColor = _skyboxGradient.Evaluate(cycleProgress);

            RenderSettings.ambientLight = lightColor;
            RenderSettings.skybox.SetColor("_Tint", skyboxColor);
        }
    }
}
