using UnityEngine;
using System.Collections;

public class BaseTrap : MonoBehaviour 
{
	//Базовый класс для ловушек
	public float Damage;				//Дамаг который ловушка наносит
	private bool isActivated;			//Была ли ловушка уже активирована
	public float RespTime;				//Время респа ловушки
	private float ActivatedTime;		//Время активации ловушки

	protected virtual void Start()
	{

	}

	protected virtual void Update()
	{
		
	}

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

	public float GetHit()
	{
		return Damage;
	}

	protected virtual void MakeTrap()
	{

	}

}
