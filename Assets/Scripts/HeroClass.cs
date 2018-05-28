using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class HeroClass
{
    public bool isOpen;

    [Range(0, 49)]
    public int Level;

    public Sprite HeroSprite;

    public List<Sprite> ShotIm;

    public Color HeroColor;

    public PlayerRPGAttribute attribute = new PlayerRPGAttribute();

    public int OpenPrice;
    public int PricePerLevel;
    public int MaxHeroLevel;


}

[Serializable]
public class SideClass : HeroClass
{
    
}
