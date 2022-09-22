using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCharacter : MonoBehaviour
{
	[SerializeField] private DebugCharacterSO debugCharacterSO;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Transform playerTrm;
	[SerializeField] private Transform parent;

	public void Start()
	{
		debugCharacterSO.debugCharacterDatas.Clear();
		RemoveDebugCharacters();

		StartCoroutine(DrawCharacters());
	}

	[ContextMenu("DrawDebugCharacters")]
	public void DrawDebugCharacters()
	{
		for(int i = 0; i < debugCharacterSO.debugCharacterDatas.Count; ++i)
		{
			var data = debugCharacterSO.debugCharacterDatas[i];
			GameObject obj = new GameObject();
			obj.transform.SetParent(parent);
			var spriteRender = obj.AddComponent<SpriteRenderer>();
			spriteRender.sprite = data.sprite;
			spriteRender.color = new Color(1, 1, 1, 0.3f);
			obj.transform.position = data.position;
			obj.transform.localScale = data.spriteSize;
		}
	}

	[ContextMenu("RemoveDebugCharacters")]
	public void RemoveDebugCharacters()
	{
		var tempArray = new GameObject[parent.transform.childCount];

		for (int i = 0; i < tempArray.Length; i++)
		{
			tempArray[i] = parent.transform.GetChild(i).gameObject;
		}

		foreach (var child in tempArray)
		{
			DestroyImmediate(child);
		}
	}

	private IEnumerator DrawCharacters()
	{
		while(true)
		{
			var data = new DebugCharacterData();
			data.sprite = spriteRenderer.sprite;
			data.position = playerTrm.position;
			data.spriteSize = new Vector2(spriteRenderer.transform.localScale.x, spriteRenderer.transform.localScale.y);
			debugCharacterSO.debugCharacterDatas.Add(data);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
