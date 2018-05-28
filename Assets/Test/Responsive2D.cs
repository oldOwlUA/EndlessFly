/**********************************************************/
/** 	© 2018 NULLcode Studio. All Rights Reserved.
/** 	Разработано в рамках проекта: https://null-code.ru/
/** 	Помощь проекту - Яндекс.Деньги: 410011769316504
/**********************************************************/

using System.Collections;
using UnityEngine;
[ExecuteInEditMode]
public class Responsive2D : MonoBehaviour {

	private enum Anchor
	{
		Center,
		MiddleLeft,
		MiddleRight,
		BottomCenter,
		BottomLeft,
		BottomRight,
		TopLeft,
		TopRight,
		TopCenter
	}

	[SerializeField] private Prefs[] objectPrefs;
	private Vector2 resolution = Vector2.zero;

	[System.Serializable] struct Prefs
	{
		public SpriteRenderer target;
		public Vector2 offset;
		public Anchor anchor;
	}

	Vector2 ScreenAnchor(Anchor value, out Vector2 delta) // определяем координаты по якорю
	{
		Vector2 result = Vector2.zero;
		delta = Vector2.zero;

		switch(value)
		{
		case Anchor.Center:
			delta = new Vector2(.5f, .5f);
			result = new Vector2(Screen.width/2, Screen.height/2);
			break;
		case Anchor.MiddleLeft:
			delta = new Vector2(1f, .5f);
			result = new Vector2(0, Screen.height/2);
			break;
		case Anchor.MiddleRight:
			delta = new Vector2(0, .5f);
			result = new Vector2(Screen.width, Screen.height/2);
			break;
		case Anchor.BottomCenter:
			delta = new Vector2(.5f, 1f);
			result = new Vector2(Screen.width/2, 0);
			break;
		case Anchor.BottomLeft:
			delta = new Vector2(1f, 1f);
			result = new Vector2(0, 0);
			break;
		case Anchor.BottomRight:
			delta = new Vector2(0, 1f);
			result = new Vector2(Screen.width, 0);
			break;
		case Anchor.TopCenter:
			delta = new Vector2(.5f, 0);
			result = new Vector2(Screen.width/2, Screen.height);
			break;
		case Anchor.TopLeft:
			delta = new Vector2(1f, 0);
			result = new Vector2(0, Screen.height);
			break;
		case Anchor.TopRight:
			delta = new Vector2(0, 0);
			result = new Vector2(Screen.width, Screen.height);
			break;
		}

		return result;
	}

	void UpdatePosition() // обновление массива спрайтов
	{
		for(int i = 0; i < objectPrefs.Length; i++)
		{
			if(objectPrefs[i].target != null)
			{
				Vector2 delta;
				Vector2 anchor = ScreenAnchor(objectPrefs[i].anchor, out delta);
				objectPrefs[i].target.transform.position = TargetPosition(objectPrefs[i].target.transform.position, anchor, objectPrefs[i].target.bounds, delta, objectPrefs[i].offset);
			}
		}
        print(objectPrefs[0].target.bounds.size.x);
	}

	Vector3 TargetPosition(Vector3 worldPoint, Vector2 screenPoint, Bounds bounds, Vector2 delta, Vector2 offset)
	{
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPoint + offset);
		worldPosition.x += bounds.size.x / 2f * (delta.x - screenPoint.x / Screen.width);
		worldPosition.y += bounds.size.y / 2f * (delta.y - screenPoint.y / Screen.height);
		worldPosition.z = worldPoint.z;
		return worldPosition;
	}

	void LateUpdate()
	{
		if(resolution.x != Screen.width || resolution.y != Screen.height) // только, если было изменено разрешение экрана
		{
			UpdatePosition();
			resolution.x = Screen.width;
			resolution.y = Screen.height;
		}

		#if UNITY_EDITOR
		UpdatePosition(); // постоянное обновление в редакторе
		#endif
	}
}