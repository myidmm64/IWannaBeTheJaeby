using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCharacterDrawer : MonoBehaviour
{
    public Queue<CharacterData> characterDatas = new Queue<CharacterData>();
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Transform playerTrm;
	[SerializeField] private Transform parent;
	[SerializeField] private int maxCount = 10;

	private WaitForSeconds waitForSeconds;


	public void Start()
	{
		waitForSeconds = new WaitForSeconds(0.12f);

		characterDatas.Clear();
		RemoveCharacters();

		StartCoroutine(AddCharacterData());
	}

	public void DrawCharacters()
    {
        if (Save.Instance.IsDeffrentMapFromSaveMap()) return; // 같은 맵일 때만 실행

        RemoveCharacters();
		int index = 1;
		while(characterDatas.Count > 0)
		{
			var data = characterDatas.Dequeue();
			GameObject obj = new GameObject();
			obj.transform.SetParent(parent);
			var spriteRender = obj.AddComponent<SpriteRenderer>();
			spriteRender.sortingLayerName = "Agent";
			spriteRender.sprite = data.sprite;
			spriteRender.color = new Color(1, 1, 1, 0.3f);
			obj.transform.position = data.position;
			obj.transform.localScale = data.spriteSize;
			Destroy(obj, index++ * 0.3f);
		}
	}

	public void RemoveCharacterDatas()
	{
		characterDatas.Clear();
	}

	public void RemoveCharacters()
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

	private IEnumerator AddCharacterData()
	{
		while (true)
		{
			if(playerTrm.gameObject.activeSelf)
			{
				var data = new CharacterData();
				data.sprite = spriteRenderer.sprite;
				data.position = playerTrm.position;
				data.spriteSize = new Vector2(spriteRenderer.transform.localScale.x, spriteRenderer.transform.localScale.y);
				characterDatas.Enqueue(data);

				if (characterDatas.Count > maxCount)
				{
					characterDatas.Dequeue();
				}
			}

			yield return waitForSeconds;
		}
	}
}


public class CharacterData
{
    public Sprite sprite;
    public Vector2 position;
    public Vector2 spriteSize;
}
