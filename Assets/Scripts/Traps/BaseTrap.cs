using UnityEngine;
using System.Collections;

public class BaseTrap : MonoBehaviour 
{
	//Базовый класс для ловушек
	public float Damage;				//Дамаг который ловушка наносит
	private bool isActivated;			//Была ли ловушка уже активирована
	public float RespTime;				//Время респа ловушки
	private float ActivatedTime;		//Время активации ловушки

	void OnMouseDown()
	{
		ActivateTrap();
	}

	private void ActivateTrap()
	{
		//Активируем ловушку
		if (Time.time - ActivatedTime > RespTime)
		{
			ActivatedTime = Time.time;
			MakeTrap();
		}
	}

	protected virtual void MakeTrap()
	{

	}

}
