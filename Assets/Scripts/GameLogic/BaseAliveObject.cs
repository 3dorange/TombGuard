using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class BaseAliveObject : MonoBehaviour 
{
	//Базовый класс для всех живых организмов в подземелье

	private float CurrentHealth;			//текущее значение здоровья
	public float MaxHealth;					//стартовое значение здоровья

	private float CurrentSpeed;				//текущая скорость перемещения
	public float MaxSpeed;					//максимальная скорость
	private Vector2 speed;

	public Transform BottomPoint;			//Точка из которой стреляем лучем, для определения касаемся ли мы пола или нет
	//Точки из которых определяем столкновения со стенами
	public Transform LeftPoint;			
	public Transform RightPoint;	
	//
	public Transform TopPoint;				//Точка пока без применения

	private bool Grounded;					//Касается земли или нет

	public LayerMask floorLayerMask;		//layers для определения пола
	private float DistanceToFloor = 0.1f;	//Длина луча до пола

	private Transform myTransform;			//кешируем трансформ
	private Rigidbody2D myRigidBody;		//кешируем риджитбоди

	private float WallDetectTime = 0;
	private float WallDetect_WaitTime = 1.5f;

	private Transform LastWallHit;
	//Направление движения
	private enum MoveDirection
	{
		Non,
		Left,
		Right,
		UpStairs,
		DownStairs
	}

	private MoveDirection moveDirection;
	//

	void Start()
	{
		myTransform = this.transform;
		myRigidBody = GetComponent<Rigidbody2D>();

		Reset();
	}

	public void Reset()
	{
		//Обнуляем параметры
		CurrentSpeed = 0;
		CurrentHealth = MaxHealth;
		Grounded = false;
		speed = Vector2.zero;
		moveDirection = MoveDirection.Non;
	}

	void Update()
	{
		FloorDetect();

		if (Grounded)
		{
			//Если касаемся пола, то проверяем что не уперлись в стену
			if (Time.time - WallDetectTime > WallDetect_WaitTime)
			{
				LastWallHit = null;
			}
			WallDetect();
		}
	}

	void FixedUpdate()
	{
		if (Grounded)
		{
			myRigidBody.AddForce(speed,ForceMode2D.Force);
		}
	}

	private void WallDetect()
	{
		////Определяем касаемся мы стены или нет
		RaycastHit2D hit = Physics2D.Raycast(LeftPoint.position, -Vector2.right,DistanceToFloor,floorLayerMask);

		if (hit.collider != null)
		{
			if (LastWallHit !=  hit.collider.transform)
			{
				LastWallHit = hit.collider.transform;
				WallDetectTime = Time.time;
				ChangeMoveDirectionToOpposite();
				return;
			}
		}

		hit = Physics2D.Raycast(RightPoint.position, Vector2.right,DistanceToFloor,floorLayerMask);
		
		if (hit.collider != null)
		{
			if (LastWallHit !=  hit.collider.transform)
			{
				LastWallHit = hit.collider.transform;
				WallDetectTime = Time.time;
				ChangeMoveDirectionToOpposite();
				return;
			}
		}
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

	private void ChangeMoveDirectionToOpposite()
	{
		//Меняем движение на противоположное
		if (moveDirection == MoveDirection.Left)
		{
			MoveRight();
		}
		else if (moveDirection == MoveDirection.Right)
		{
			MoveLeft();
		}
	}

	private void CalculateSpeed()
	{
		if (moveDirection == MoveDirection.Left)
		{
			speed = new Vector2(-MaxSpeed,0);
		}
		else if (moveDirection == MoveDirection.Right)
		{
			speed = new Vector2(MaxSpeed,0);
		}
	}

	//Движение
	private void MoveLeft()
	{
		moveDirection = MoveDirection.Left;
		CalculateSpeed();
	}

	private void MoveRight()
	{
		moveDirection = MoveDirection.Right;
		CalculateSpeed();
	}
	////

	void OnTriggerEnter2D(Collider2D other) 
	{
		//Проверяем на попадение в триггеры
		if (other.name == "DestroyZone")
		{
			Destroy(this.gameObject);
		}

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
		int Des = (int) Random.Range(0.0f,decisionZone.moveDirectionForThisPoint.Length);

		if (decisionZone.moveDirectionForThisPoint[Des] == MakeDecisionZone.MoveDirections.Left)
		{
			MoveLeft();
		}
		else if (decisionZone.moveDirectionForThisPoint[Des] == MakeDecisionZone.MoveDirections.Right)
		{
			MoveRight();
		}
	}
}
