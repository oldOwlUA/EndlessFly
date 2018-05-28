using UnityEngine;
using CHe;
using UnityEngine.UI;
using System.Collections;

public enum stages
{
    Wait,
    On,
    Off,
    Cycle
}

public class FadeInOut : SingletonObj<FadeInOut>
{

    public float fadeOnSpeed = 1.5f;
    public float fadeOffSpeed = 1;

    public Color fadeColor = Color.black;
    public float delayOnCycle;



    private Image _image;
    [SerializeField]
    private bool cycleWork;



    public stages switch_on;

    void Awake()
    {
        _image = GetComponentInChildren<Image>();
        _image.enabled = true;
    }

    void Update()
    {

        switch ((int)switch_on)
        {

            case 1:
                {
                    OnFade();
                }
                break;
            case 2:
                {
                    OffFade();
                }
                break;
            case 3:
                {
                    StartCoroutine(Cycle());
                }
                break;

            default:
                break;
        }

    }

    void OffFade()
    {

        _image.color = Color.Lerp(_image.color, Color.clear, fadeOffSpeed * Time.deltaTime);

        if (_image.color.a <= 0.01f)
        {
            _image.color = Color.clear;
            _image.enabled = false;
            switch_on = stages.Wait;
        }
    }

    void OnFade()
    {
        _image.enabled = true;
        _image.color = Color.Lerp(_image.color, fadeColor, fadeOnSpeed * Time.deltaTime);

        if (_image.color.a >= 0.98f)
        {
            _image.color = fadeColor;
            switch_on = stages.Wait;
        }
    }


    public IEnumerator Cycle()
    {
        if (!cycleWork)
        {
            cycleWork = true;
            bool t = true;
            float time = delayOnCycle;
            print("one");

            while (t)
            {
                _image.enabled = true;
                _image.color = Color.Lerp(_image.color, fadeColor, fadeOnSpeed * Time.deltaTime);
                
                if (_image.color.a >= 0.98f)
                {
                    _image.color = fadeColor;
                    t = false;
                }
                yield return null;
            }
            print("two");
            t = true;

            yield return new WaitForSeconds(time);



            while (t)
            {
                _image.color = Color.Lerp(_image.color, Color.clear, fadeOffSpeed * Time.deltaTime);
                
                if (_image.color.a <= 0.01f)
                {
                    _image.color = Color.clear;
                    _image.enabled = false;
                    switch_on = stages.Wait;
                    t = false;
                }
                yield return null;
            }
            print("four");
            cycleWork = false;
        }

        else
        {          
            yield return null;
        }
    }
}