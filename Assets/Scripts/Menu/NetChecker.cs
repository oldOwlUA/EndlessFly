using UnityEngine;
using System.Collections;

public class NetChecker : MonoBehaviour {

    public static NetChecker instance = null;
	public static bool NetCheck = false;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
	{
		CheckNet ();

	}

	public void CheckNet()
	{
		StartCoroutine (_netChecker());
	}

	IEnumerator _netChecker()
	{

		WWW www = new WWW("https://www.google.com/");
		yield return www;

		if (!string.IsNullOrEmpty (www.error))
		{
			NetCheck = false;
		} 
		else {
			NetCheck = true;
		}
		print (NetCheck);
		yield return new WaitForSeconds(30f);
		StartCoroutine (_netChecker());
	}


}
