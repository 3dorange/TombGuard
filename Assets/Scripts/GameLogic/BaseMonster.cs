using UnityEngine;
using System.Collections;

public class BaseMonster : BaseAliveObject 
{
	//Базовый класс всех героев
	public float Damage;

	public float GetHit()
	{
		return Damage;
	}
}
