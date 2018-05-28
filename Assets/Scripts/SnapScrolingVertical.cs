using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using System.IO;

public class SnapScrolingVertical : MonoBehaviour
{
    //public--------------------------------------------------
    public int panCount;
    public GameObject PanPref;
    public int panOffset = 10;
    public ScrollRect scrollRect;
    public float snapSpeed;
    public bool isNotNull;
    public List<GameObject> instPan;
    //public Avatar Av;
    
    //private
    
    [SerializeField]
    private Vector2[] pansPos;
    private bool isScrolling;
    private RectTransform contentRect;
    private Vector2 contentVector;

    [SerializeField]
    private int selectedPanId;

    void Awake()
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
            instPan[i].transform.localPosition = new Vector2(instPan[i].transform.localPosition.x, instPan[i - 1].transform.localPosition.y - PanPref.GetComponent<RectTransform>().sizeDelta.y -
                panOffset);
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

    private void OnEnable()
    {
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




    void FixedUpdate()
    {

        if (contentRect.anchoredPosition.y >= pansPos[0].y && !isScrolling ||
            contentRect.anchoredPosition.y <= pansPos[pansPos.Length - 1].y && !isScrolling)
        {
            scrollRect.inertia = false;
        }

        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {


            float distance = Mathf.Abs(contentRect.anchoredPosition.y - pansPos[i].y);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanId = i;
                UIController.Instance.UpdateValuesSides(i);
            }
        }

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.y);


        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.y = Mathf.SmoothStep(contentRect.anchoredPosition.y, pansPos[selectedPanId].y, snapSpeed * Time.deltaTime);
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




