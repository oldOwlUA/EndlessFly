using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SylokHiddenCat
{
    public static class Localization
    {

        public static Local GetLanguage()
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
				return Local.RUS;
                case SystemLanguage.Ukrainian:
                    return Local.RUS;
                default:
                    return Local.ENG;
            }
        }

        public static void ChangeLanguage(List<GameObject> listRu, List<GameObject> listEn)
        {
            if (GetLanguage() == Local.RUS)
            {
                for (int i = 0; i < listRu.Count; i++)
                {
                    listRu[i].SetActive(true);
                    listEn[i].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < listEn.Count; i++)
                {
                    listRu[i].SetActive(false);
                    listEn[i].SetActive(true);
                }
            }
        }
    }

    public enum Local { ENG, RUS }
}
