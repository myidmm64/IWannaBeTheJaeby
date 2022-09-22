using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugCharacterSO", menuName = "ScriptableObject/DebugCharacterSO")]
public class DebugCharacterSO : ScriptableObject
{
	public List<DebugCharacterData> debugCharacterDatas = new List<DebugCharacterData>();

	[ContextMenu("ClearDatas")]
	public void ClearDatas()
	{
		debugCharacterDatas.Clear();
	}
}

public class DebugCharacterData
{
	public Sprite sprite;
	public Vector2 position;
	public Vector2 spriteSize;
}
