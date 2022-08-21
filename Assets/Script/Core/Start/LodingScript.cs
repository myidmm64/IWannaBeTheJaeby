using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingScript : MonoBehaviour
{

    [SerializeField]
    private Slider _loadSlider = null;
    [SerializeField]
    private TextMeshProUGUI _loadText = null;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while(!operation.isDone)
        {
            yield return null;

            if(_loadSlider.value < 0.9f)
            {
                _loadSlider.value = operation.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                _loadSlider.value = Mathf.Lerp(0.9f, 1f, timer);
                if(_loadSlider.value >= 1f)
                {
                    _loadText.text = "로딩 완료 !!";
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
