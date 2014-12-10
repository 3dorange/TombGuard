using UnityEngine;
using System.Collections;

public class BaseAliveObject : MonoBehaviour 
{
	//Базовый класс для всех живых организмов в подземелье

	private float CurrentHealth;			//текущее значение здоровья
	public float MaxHealth;					//стартовое значение здоровья

	private float CurrentSpeed;				//текущая скорость перемещения
	public float MaxSpeed;					//максимальная скорость

	private bool Grounded;					//Касается земли или нет

	void Start()
	{
		CurrentSpeed = 0;
		CurrentHealth = MaxHealth;
		Grounded = false;
	}

	void Update()
	{
		
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		//Проверяем на попадение в триггеры
		switch (other.tag)
		{
			case "MakeDecisionZone":
				ChooseWay(other.GetComponent<MakeDecisionZone>());
			break;

			default:
				break;
		}
	}

	private void ChooseWay(MakeDecisionZone decisionZone)
	{
		//Выбираем направление движения
	}
}
