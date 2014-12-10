using UnityEngine;
using System.Collections;

public class BaseAliveObject : MonoBehaviour 
{
	//Базовый класс для всех живых организмов в подземелье

	private float CurrentHealth;			//текущее значение здоровья
	public float MaxHealth;					//стартовое значение здоровья

	private float CurrentSpeed;				//текущая скорость перемещения
	public float MaxSpeed;					//максимальная скорость

	public Transform BottomPoint;			//Точка из которой стреляем лучем, для определения касаемся ли мы пола или нет
	private bool Grounded;					//Касается земли или нет

	private Transform myTransform;			//кешируем трансформ
	public LayerMask floorLayerMask;		//layers для определения пола
	private float DistanceToFloor = 0.2f;	//Длина луча до пола

	void Start()
	{
		myTransform = this.transform;

		CurrentSpeed = 0;
		CurrentHealth = MaxHealth;
		Grounded = false;
	}

	void Update()
	{
		FloorDetect();
	}

	private void FloorDetect()
	{
		//Определяем касаемся мы пола или нет
		RaycastHit2D hit = Physics2D.Raycast(BottomPoint.position, -Vector2.up,DistanceToFloor,floorLayerMask);

		if (hit.collider != null)
		{
			//если попали
			Grounded = true;
		}
		else
		{
			//пола снизу нет
			Grounded = false;
		}
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
