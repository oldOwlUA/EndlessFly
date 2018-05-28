using UnityEngine;
using System.Collections;

namespace CHe
{

    public class SingletonObj<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        var singleton = new GameObject("[Singleton] " + typeof(T));
                        _instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                    }
                }
                return _instance;
            }
        }

    }
}
/*
для создания синглтона унаследуйте класс в виде

public class GameController : SingletonObj<GameController>
{
       // TODO Что надо?
}
     
*/
