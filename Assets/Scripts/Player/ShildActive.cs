using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildActive : MonoBehaviour {

    public enum ThisShakeMode { onlyX, onlyY, onlyZ, XY, XZ, XYZ };

    private static Transform tr;
    private static float elapsed, i_Duration, i_Power, percentComplete;

    private static ThisShakeMode i_Mode;
    private static Vector3 originalPos;
    public static bool stop;
    public GameObject FX;

    public float ShildTime;

    public void Shake()
    {
        GetShildTime();
        Shake(ShildTime, 0.1f, ThisShakeMode.onlyX);
    }

    public void ShakeContinue(float time)
    {        
        Shake(time, 0.1f, ThisShakeMode.onlyX);
    }

    void GetShildTime()
    {
        float tempShildTime = GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].attribute.TimeMagnet;
        if (tempShildTime <= 0)
            ShildTime = 10;
        else ShildTime = tempShildTime;

    }

    private void Start()
    {
        GetShildTime();
        stop = false;
        percentComplete = 1;
        tr = GetComponent<Transform>();
    }

    public static  void StopShake()
    {
        elapsed = i_Duration;
        originalPos = tr.localPosition;
        PlayerController.Instance.isProtected = false;       
    }

    public static void Shake(float duration, float power)
    {
        stop = false;

        if (percentComplete == 1) originalPos = tr.localPosition;

        i_Mode = ThisShakeMode.XYZ;
        elapsed = 0;
        i_Duration = duration;
        i_Power = power;
    }


    public static void Shake(float duration, float power, ThisShakeMode mode)
    {
        stop = false;

        PlayerController.Instance.isProtected = true;
        if (percentComplete == 1) originalPos = tr.localPosition;

        
        i_Mode = mode;
        elapsed = 0;
        i_Duration = duration;
        i_Power = power;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopShake();
            //Shake(10, 0.1f, ThisShakeMode.onlyX);           
        }
        if (elapsed < i_Duration)
        {
            FX.SetActive(true);
            elapsed += Time.deltaTime;

            percentComplete = elapsed / i_Duration;
            percentComplete = Mathf.Clamp01(percentComplete);

            Vector3 rnd = Random.insideUnitSphere * i_Power * (1f - percentComplete);
            if (stop)
            {
                tr.localPosition = originalPos;
                elapsed = 1;
                return;
            }

            switch (i_Mode)
            {
                case ThisShakeMode.XYZ:
                    tr.localPosition = originalPos + rnd;
                    break;

                case ThisShakeMode.onlyX:
                    tr.localPosition = originalPos + new Vector3(rnd.x, 0, 0);
                    break;

                case ThisShakeMode.onlyY:
                    tr.localPosition = originalPos + new Vector3(0, rnd.y, 0);
                    break;

                case ThisShakeMode.onlyZ:
                    tr.localPosition = originalPos + new Vector3(0, 0, rnd.z);
                    break;

                case ThisShakeMode.XY:
                    tr.localPosition = originalPos + new Vector3(rnd.x, rnd.y, 0);
                    break;

                case ThisShakeMode.XZ:
                    tr.localPosition = originalPos + new Vector3(rnd.x, 0, rnd.z);
                    break;

                default:
                    break;
            }

        }
        else
        {
            FX.SetActive(false);
            PlayerController.Instance.isProtected = false;
        }
    }
}
