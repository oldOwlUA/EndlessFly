using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using System.IO;

public class SnapScroling : MonoBehaviour
{
    public enum Avatar { Hero, Left, Right }

    //public--------------------------------------------------
    public int panCount;
    public GameObject PanPref;
    public int panOffset = 10;
    public ScrollRect scrollRect;
    public float snapSpeed;
    public bool isNotNull;
    public List<GameObject> instPan;
    public Avatar Av;



    //private
    
    [SerializeField]
    private Vector2[] pansPos;
    private bool isScrolling;
    private RectTransform contentRect;
    private Vector2 contentVector;

   
    public int selectedPanId;

    void Awake()
    {

        switch ((int)Av)
        {
            case 0:
                {
                    panCount = GameController.Instance.Heroes.Count;

                    contentRect = GetComponent<RectTransform>();

                    pansPos = new Vector2[panCount];

                    selectedPanId = GameController.Instance.IndCurrentHerro;

                    for (int i = 0; i < panCount; i++)
                    {
                        GameObject gm;

                        gm = Instantiate(PanPref, transform.position, transform.rotation, transform) as GameObject;

                        instPan.Add(gm);
                        if (i == 0) continue;
                        instPan[i].transform.localPosition = new Vector2(instPan[i - 1].transform.localPosition.x + PanPref.GetComponent<RectTransform>().sizeDelta.x +
                            panOffset, instPan[i].transform.localPosition.y);
                        pansPos[i] = -instPan[i].transform.localPosition;

                    }

                    for (int j = 0; j < panCount; j++)
                    {
                        instPan[j].GetComponentInChildren<Image>().sprite = GameController.Instance.Heroes[j].HeroSprite;
                        instPan[j].GetComponent<HeroPanelData>().id = j;
                        instPan[j].GetComponent<HeroPanelData>().isOpenHero = GameController.Instance.Heroes[j].isOpen;
                    }

                    if (isNotNull)
                    {
                        contentRect.anchoredPosition = pansPos[selectedPanId];
                    }
                }
                break;

            case 1:
                {
                    panCount = GameController.Instance.SideKicks.Count;

                    contentRect = GetComponent<RectTransform>();

                    pansPos = new Vector2[panCount];

                    selectedPanId = GameController.Instance.IndCurrentLeft;

                    for (int i = 0; i < panCount; i++)
                    {
                        GameObject gm;

                        gm = Instantiate(PanPref, transform.position, transform.rotation, transform) as GameObject;

                        instPan.Add(gm);
                        if (i == 0) continue;
                        instPan[i].transform.localPosition = new Vector2(instPan[i - 1].transform.localPosition.x + PanPref.GetComponent<RectTransform>().sizeDelta.x +
                            panOffset, instPan[i].transform.localPosition.y);
                        pansPos[i] = -instPan[i].transform.localPosition;

                    }

                    for (int j = 0; j < panCount; j++)
                    {
                        instPan[j].GetComponentInChildren<Image>().sprite = GameController.Instance.SideKicks[j].HeroSprite;
                        instPan[j].GetComponent<HeroPanelData>().id = j;
                        instPan[j].GetComponent<HeroPanelData>().isOpenHero = GameController.Instance.SideKicks[j].isOpen;
                    }

                    if (isNotNull)
                    {
                        contentRect.anchoredPosition = pansPos[selectedPanId];
                    }
                }
                break;
            case 2:
                {
                    panCount = GameController.Instance.SideKicks.Count;

                    contentRect = GetComponent<RectTransform>();

                    pansPos = new Vector2[panCount];

                    selectedPanId = GameController.Instance.IndCurrentRight;

                    for (int i = 0; i < panCount; i++)
                    {
                        GameObject gm;

                        gm = Instantiate(PanPref, transform.position, transform.rotation, transform) as GameObject;

                        instPan.Add(gm);
                        if (i == 0) continue;
                        instPan[i].transform.localPosition = new Vector2(instPan[i - 1].transform.localPosition.x + PanPref.GetComponent<RectTransform>().sizeDelta.x +
                            panOffset, instPan[i].transform.localPosition.y);
                        pansPos[i] = -instPan[i].transform.localPosition;

                    }

                    for (int j = 0; j < panCount; j++)
                    {
                        instPan[j].GetComponentInChildren<Image>().sprite = GameController.Instance.SideKicks[j].HeroSprite;
                        instPan[j].GetComponent<HeroPanelData>().id = j;
                        instPan[j].GetComponent<HeroPanelData>().isOpenHero = GameController.Instance.SideKicks[j].isOpen;
                    }

                    if (isNotNull)
                    {
                        contentRect.anchoredPosition = pansPos[selectedPanId];
                    }
                }
                break;



        }       
    }

    private void OnEnable()
    { 
        switch ((int)Av)
        {
            case 0:
                {
                    selectedPanId = GameController.Instance.IndCurrentHerro;
                    if (pansPos[selectedPanId] == null)
                    {
                        print(pansPos[selectedPanId]);
                        return;
                    }
                    else
                    {
                        contentRect.anchoredPosition = pansPos[selectedPanId];
                    }
                }
                break;

            case 1:
                {
                    selectedPanId = GameController.Instance.IndCurrentLeft;
                    if (pansPos[selectedPanId] == null)
                    {
                        print(pansPos[selectedPanId]);
                        return;
                    }
                    else
                    {
                        contentRect.anchoredPosition = pansPos[selectedPanId];
                    }
                }
                break;
            case 2:
                {
                    selectedPanId = GameController.Instance.IndCurrentRight;
                    if (pansPos[selectedPanId] == null)
                    {
                        print(pansPos[selectedPanId]);
                        return;
                    }
                    else
                    {
                        contentRect.anchoredPosition = pansPos[selectedPanId];
                    }
                }
                break;
        }
    }

    public void SetHero()
    {
        GameController.Instance.IndCurrentHerro = selectedPanId;
        PlayerController.Instance.SetAv();
    }

    public void SetLeft()
    {
        GameController.Instance.IndCurrentLeft = selectedPanId;
        PlayerController.Instance.SetLeft();
    }
    public void SetRight()
    {
        GameController.Instance.IndCurrentRight = selectedPanId;
        PlayerController.Instance.SetRight();
    }


    void FixedUpdate()
    {

        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling ||
            contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
        {
            scrollRect.inertia = false;
        }

        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {


            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanId = i;
                UIController.Instance.UpdateValues(i);
            }
        }

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);


        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanId].x, snapSpeed * Time.deltaTime);
        contentRect.anchoredPosition = contentVector;


    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll)
        {
            scrollRect.inertia = true;
        }

    }

}




