using UnityEngine;
using System.Collections;

public class ColonTrap : BaseTrap
{
	public Transform StartPos;
	public Transform EndPos;

	override protected void MakeTrap ()
	{
		base.MakeTrap ();
		BeginColonMove();
	}

	protected void BeginColonMove()
	{

	}
}
