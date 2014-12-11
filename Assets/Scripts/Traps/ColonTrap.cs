using UnityEngine;
using System.Collections;

public class ColonTrap : BaseTrap
{
	//Класс выдвигающийся ловушки
	public Transform StartPos;				//стартовая позиция колонны
	public Transform EndPos;				//позиция после удара
	public Transform ActiveElement;			//элемент который будет двигаться
	private bool isMoving = false;			//двигается ли колонна сейчас
	private float HitSpeed = 1.5f;			//скорость движения ловушки
	private float StarHitTime = 0;			//время удара
	private bool MovingDownOrUp = false;	//направление движение
	private float TimeToMove = 0;			//время необходимое на анимацию

	override protected void Start()
	{
		base.Start();
		ActiveElement.position = StartPos.position;
	}

	override protected void Update()
	{
		base.Update();

		if (isMoving)
		{
			if (Time.time - StarHitTime <  TimeToMove)
			{
				if (MovingDownOrUp)
				{
					ActiveElement.position = Vector3.Lerp(StartPos.position,EndPos.position,(Time.time - StarHitTime)/TimeToMove);
				}
				else
				{
					ActiveElement.position = Vector3.Lerp(EndPos.position,StartPos.position,(Time.time - StarHitTime)/TimeToMove);
				}
			}
			else
			{
				if (MovingDownOrUp)
				{
					BeginColonMoveToStart();
				}
				else
				{
					AimationEnds();
				}
			}
		}
	}

	override protected void MakeTrap ()
	{
		//произошла активация ловушки
		base.MakeTrap ();
		BeginColonMove();
	}

	protected void BeginColonMove()
	{
		//начинаем анимацию
		isMoving = true;
		MovingDownOrUp = true;
		StarHitTime = Time.time;

		TimeToMove = (Vector3.Distance(StartPos.position,EndPos.position))/HitSpeed;
	}

	protected void BeginColonMoveToStart()
	{
		//начинаем анимацию
		isMoving = true;
		MovingDownOrUp = false;
		StarHitTime = Time.time;
		
		TimeToMove = (Vector3.Distance(EndPos.position,StartPos.position))/(HitSpeed/3);
	}

	private void AimationEnds()
	{

	}
}
