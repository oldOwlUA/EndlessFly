using UnityEngine;
using System.Collections;

public class RandomPriority : MonoBehaviour {

	//////////// блок float ////////////
	public static float[] GetRandom(int[] chances, float[] value, float[] current)
	{
		if(chances.Length != value.Length) return current;

		int[] tmp_chances = new int[chances.Length];
		chances.CopyTo(tmp_chances, 0);

		for(int i = 0; i < current.Length; i++)
		{
			int j = Get(tmp_chances);

			if(j > -1)
			{
				current[i] = Value(tmp_chances, value, j);
			}
		}

		return current;
	}

	static float Value(int[] chances, float[] value, int current)
	{
		float j = 0;

		for(int i = 0; i < chances.Length; i++)
		{
			if(i == current) j = value[i];
		}

		return j;
	}

	//////////// блок string ////////////
	public static string[] GetRandom(int[] chances, string[] value, string[] current)
	{
		if(chances.Length != value.Length) return current;

		int[] tmp_chances = new int[chances.Length];
		chances.CopyTo(tmp_chances, 0);

		for(int i = 0; i < current.Length; i++)
		{
			int j = Get(tmp_chances);

			if(j > -1)
			{
				current[i] = Value(tmp_chances, value, j);
			}
		}

		return current;
	}

	static string Value(int[] chances, string[] value, int current)
	{
		string j = string.Empty;

		for(int i = 0; i < chances.Length; i++)
		{
			if(i == current) j = value[i];
		}

		return j;
	}

	//////////// блок color ////////////
	public static Color[] GetRandom(int[] chances, Color[] value, Color[] current)
	{
		if(chances.Length != value.Length) return current;

		int[] tmp_chances = new int[chances.Length];
		chances.CopyTo(tmp_chances, 0);

		for(int i = 0; i < current.Length; i++)
		{
			int j = Get(tmp_chances);

			if(j > -1)
			{
				current[i] = Value(tmp_chances, value, j);
			}
		}

		return current;
	}

	static Color Value(int[] chances, Color[] value, int current)
	{
		Color j = Color.clear;

		for(int i = 0; i < chances.Length; i++)
		{
			if(i == current) j = value[i];
		}

		return j;
	}

	//////////// блок int ////////////
	public static int[] GetRandom(int[] chances, int[] value, int[] current)
	{
		if(chances.Length != value.Length) return current;

		int[] tmp_chances = new int[chances.Length];
		chances.CopyTo(tmp_chances, 0);

		for(int i = 0; i < current.Length; i++)
		{
			int j = Get(tmp_chances);

			if(j > -1)
			{
				current[i] = Value(tmp_chances, value, j);
			}
		}

		return current;
	}

	static int Value(int[] chances, int[] value, int current)
	{
		int j = 0;

		for(int i = 0; i < chances.Length; i++)
		{
			if(i == current) j = value[i];
		}

		return j;
	}

	//////////// общий блок ////////////
	static int Get(int[] ch)
	{
		int all = 0;
		foreach(int c in ch) all += c;
		int lr = 0;
		int r = Random.Range(0, all);

		for(int i = 0; i < ch.Length; i++)
		{
			if(ch[i] > 0)
			{
				if(r >= lr && r < lr + ch[i])
				{
					ch[i]--;
					return i;
				}
				else lr += ch[i];
			}
		}

		return -1;
	}
}

/*
 * 
    public Color[] colors = new Color[10];

    public int[] ins = new int[100];

    // Use this for initialization
    void Start () {
       
            int[] index = { 5, 3, 2 }; // указываем сколько раз должен выпасть тот или иной элемент
            Color[] col = { Color.red, Color.green, Color.yellow }; // указываем значения выпадающих элементов
            colors = RandomPriority.GetRandom(index, col, colors);

        int[] inde = { 95, 3, 2 }; // указываем сколько раз должен выпасть тот или иной элемент
        int[] vars = { 95,3,2}; // указываем значения выпадающих элементов
        ins = RandomPriority.GetRandom(inde, vars, ins);

    }
	
 * */