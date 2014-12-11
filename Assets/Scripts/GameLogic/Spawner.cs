using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	//Простой спаунер мобов
	public float WaitTime = 0.7f;
	private float SpawnTime = 0;
	public GameObject Hero;
	public Transform SpawnPos;

	void Update()
	{
		if (Time.time - SpawnTime > WaitTime)
		{
			SpawnTime = Time.time;
			GameObject hero = Instantiate(Hero,SpawnPos.position,Quaternion.identity) as GameObject;
			hero.transform.parent = this.transform;
		}
	}

}
