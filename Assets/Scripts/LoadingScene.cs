using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public TextMeshPro titleText;

    private Color titleTextStartColor;
    private float t = 0;

    private float switchDuration = 3f;

    void Start()
    {
        StartCoroutine(SwitchScene());

        titleTextStartColor = titleText.color;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        titleText.color = Color.Lerp(titleTextStartColor, Color.black, t / switchDuration);
        titleText.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t / switchDuration);
    }

    private IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(switchDuration);
        SceneManager.LoadScene("HumanScene");
    }
}