using UnityEngine;
using System.Collections;

/// <summary>
/// Camera shake.
/// </summary>
[AddComponentMenu("X/Camera/Shake")]
public class CameraShake : MonoBehaviour
{
    /// <summary>
    /// The tareget.
    /// </summary>
    public Camera Tareget;

    /// <summary>
    /// The speed.
    /// </summary>
    public Vector2 Speed;

    /// <summary>
    /// The distance.
    /// </summary>
    public Vector2 Distance;

    /// <summary>
    /// The shake time.
    /// </summary>
    public float ShakeTime = 0.5f;

    /// <summary>
    /// The shake count.
    /// </summary>
    public int ShakeCount = 3;

    /// <summary>
    /// The delay.
    /// </summary>
    public float Delay = 0;

    private Vector2 seed;

    private Vector3 position;

    private Vector3 startPosition;

    private float startTime;

    private int count = 0;

    private XUtility.CustomerAction Over;

    private float delayTick = 0;

    /// <summary>
    /// Play this instance.
    /// </summary>
    public void Play()
    {
        if (Tareget == null)
            return;
        enabled = true;
        startPosition = position = Tareget.transform.position;
        startTime = Time.time;
        Shake(0);
    }

    /// <summary>
    /// Play the specified target, speed, distance, shakeTime, shakeCount and overCallback.
    /// </summary>
    /// <param name="target">Target.</param>
    /// <param name="speed">Speed.</param>
    /// <param name="distance">Distance.</param>
    /// <param name="shakeTime">Shake time.</param>
    /// <param name="shakeCount">Shake count.</param>
    /// <param name="overCallback">Over callback.</param>
    public void Play(Camera target, Vector2 speed, Vector2 distance, float shakeTime, int shakeCount, XUtility.CustomerAction overCallback)
    {
        Tareget = target;
        Speed = speed;
        Distance = distance;
        ShakeTime = shakeTime;
        ShakeCount = shakeCount;
        Over = overCallback;
        Play();
    }

    /// <summary>
    /// Stop this instance.
    /// </summary>
    public void Stop()
    {
        Over = null;
        enabled = false;
    }

    void Start()
    {
        if (Tareget == null && Camera.main != null)
            Tareget = Camera.main;
        if (Tareget == null)
        {
            enabled = false;
            return;
        }
        if (Delay <= 0)
        {
            delayTick = -1.0f;
            Play();
        }
        else
            delayTick = Time.time + Delay;
    }

    void OnEnable()
    {
        if(Delay > 0)
            delayTick = Time.time + Delay;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (delayTick > 0)
        {
            if(Time.time >= delayTick)
            {
                delayTick = -1.0f;
                Play();
            }
        } else
        {
            if (Time.time - startTime > ShakeTime)
            {
                count++;
                Shake(Time.time - startTime);
                startTime = Time.time;
                if (count == ShakeCount)
                {
                    if (Over != null)
                        Over();
                    Tareget.transform.position = startPosition;
                    enabled = false;
                    return;
                }
            }
        }
    }

    void Shake(float time)
    {
        seed = Random.insideUnitCircle;
        if(Speed.x > 0)
            position.x = startPosition.x + seed.x * Mathf.Sin(time* Speed.x) * Distance.x;
        if (Speed.y > 0)
            position.y = startPosition.y + seed.y * Mathf.Cos(time * Speed.y) * Distance.y;;
        
        Tareget.transform.position = position;
    }
}

