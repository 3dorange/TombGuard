using UnityEngine;
using System.Collections;

public class MakeDecisionZone : MonoBehaviour 
{
	//Возможные направления движения
	public enum MoveDirections
	{
		Right,
		Left,
		Up,
		Down
	}

	public MoveDirections[] moveDirectionForThisPoint;				//Возможные варианты направления движения в данной точке

}
