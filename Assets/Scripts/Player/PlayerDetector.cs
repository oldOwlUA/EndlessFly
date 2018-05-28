using UnityEngine;
using System.Collections;

public class PlayerDetector : MonoBehaviour
{
    //Отвечает за детектирование играка с єлементами игры(враги. монетки и прочее)
    public GameObject magnet;
    public GameObject components;
    Coroutine cur;

    public CameraShake.ShakeMode ShMod = CameraShake.ShakeMode.XY;
    public float shakePower = 1;
    public float shakeDuration = 1;
    public GameObject ExplodePrefab;

    public GameObject shild;
    private void OnTriggerEnter2D(Collider2D col)
    {


        switch (col.tag)
        {
            case "Loot":
                {
                    Destroy(col.gameObject);
                    GameSetAndStat.Instance.CoinPlus();
                    AudioController.Instance.source[3].Play();
                }
                break;
            case "Enemy":
                {
                    if (shild.activeSelf)
                        break;


                    print("En");

                    if (PlayerController.Instance.isProtected)
                        break;

                    AudioController.Instance.source[1].Play();
                    AudioController.Instance.source[2].Play();
                    GameController.Instance.currentState = GameState.PlayerDead;

                    PlayerWeapon.Instance.ResetPA();
                    Handheld.Vibrate();
                    CameraShake.Shake(shakeDuration, shakePower, ShMod);
                    Destroy(col.gameObject);
                    gameObject.SetActive(false);
                    
                    if (cur != null)
                    {
                        StopCoroutine(cur);
                        cur = null;
                    }
                    magnet.GetComponent<MagnetEffect>().OffMagnet();
                   
                  //  UIController.Instance.GameScrean.SetActive(false);
                    Instantiate(ExplodePrefab, transform.position, Quaternion.identity);
                  
                    break;
                }
            case "Magnet":
                {
                    print("Magnet");
                    Destroy(col.gameObject);
                    AudioController.Instance.source[4].Play();
                    if (cur != null && magnet.GetComponent<MagnetEffect>().iswork)
                    {
                        magnet.GetComponent<MagnetEffect>().resetTime();
                        Debug.Log("Reset magnet");
                    }
                    else if (!magnet.GetComponent<MagnetEffect>().iswork)
                    {

                        cur = null;
                        Debug.Log("Start new magnet");
                        cur = StartCoroutine(magnet.GetComponent<MagnetEffect>().MagnetCur());
                    }
                    else if (cur == null && !magnet.GetComponent<MagnetEffect>().iswork)
                    {
                        Debug.Log("Start magnet");
                        cur = StartCoroutine(magnet.GetComponent<MagnetEffect>().MagnetCur());
                    }

                    break;
                }
            case "PA":
                {                  
                    Destroy(col.gameObject);
                    AudioController.Instance.source[4].Play();
                    PlayerWeapon.Instance.PoverUp += 5;
                    break;
                }

            default:
                break;
        }




    }

}
