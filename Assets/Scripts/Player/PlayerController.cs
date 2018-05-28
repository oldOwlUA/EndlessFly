using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;
using System;
using UnityEngine.UI;

using CHe;
[Serializable]
public class Boundary
{
    public float xMin, xMax;
    private float zMin, zMax;
}

//[RequireComponent(typeof(PlayerWeapon))]

public class PlayerController : SingletonObj<PlayerController>
{

    // - public---------------------



    [Space]
    [Header("Этот скрипт отвечает за персонажа")]

    [Header("Oграничения по передвижению персонажа")]
    public Boundary boundary;
    public GameObject PlayerAv;
    public List<GameObject> PlayerHelps;
    public float tilt = 5;
    public bool isProtected = false;
    public List<GameObject> PlayerAvatar;

    [Header("MagicNumber увеличивает чувствительность игрока при смещения")]
    public int magic;

    // - private--------------------


    private Rigidbody2D _playerRB;
    private float _speed;

    // - methods--------------------

    // Use this for initialization
    void Start()
    {

        SetAv();

        float scl = (Camera.main.orthographicSize * Camera.main.aspect);
        float scaleP = (scl * 2) / 5;
        transform.localScale = new Vector3(scaleP, scaleP, scaleP);

        boundary.xMin = -scl;
        boundary.xMax = scl;
        _playerRB = GetComponent<Rigidbody2D>();


    }


    private void OnEnable()
    {

        SetAv();
        _playerRB = GetComponent<Rigidbody2D>();
        _playerRB.position = new Vector3
           (
              0f,
             _playerRB.position.y,
              0f
           );
    }

    public void SetAv()
    {
        PlayerAv.SetActive(true);
        PlayerAv.GetComponentInChildren<SpriteRenderer>().sprite = GameController.Instance.Heroes[GameController.Instance.IndCurrentHerro].HeroSprite;
        SetLeft();
        SetRight();
    }

    public void SetLeft()
    {
        PlayerHelps[0].GetComponentInChildren<SpriteRenderer>().sprite = GameController.Instance.SideKicks[GameController.Instance.IndCurrentLeft].HeroSprite;
    }

    public void SetRight()
    {
        PlayerHelps[1].GetComponentInChildren<SpriteRenderer>().sprite = GameController.Instance.SideKicks[GameController.Instance.IndCurrentRight].HeroSprite;
    }


    private void Update()
    {
        Vector3 rotate;
        float angle = _playerRB.velocity.x * -tilt * Time.deltaTime/2;

        if (angle > tilt / 2)
            angle = tilt / 2;
        if (angle < -tilt / 2)
            angle = -tilt / 2;

           
        rotate = new Vector3(0, 0, angle);
       // rotate.Normalize();
      
        PlayerAvatar[0].transform.localEulerAngles = rotate;

        if (PlayerHelps[0] != null)
            PlayerHelps[0].transform.localEulerAngles = new Vector3(0, 0, angle);

        if (PlayerHelps[0] != null)
            PlayerHelps[1].transform.localEulerAngles = new Vector3(0, 0, angle);
    }


    private void FixedUpdate()
    {
        _speed = GameSetAndStat.Instance._G_SensitiveTouch + magic;
        float moveHorisontal = CnInputManager.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorisontal, 0f, 0f);
        Debug.Log(_playerRB.velocity = movement * _speed * 0.02f);

        _playerRB.position = new Vector3
            (
                Mathf.Clamp(_playerRB.position.x, boundary.xMin, boundary.xMax),
               _playerRB.position.y,
                0f
            );

       
    }




}
