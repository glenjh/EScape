using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scenefade : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    // Start is called before the first frame update
    void Start()
    {
       // StartCoroutine(FadeIn());
    }
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
    public IEnumerator FadeIn()
    {
        float t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime * 1f;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 1f;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        SceneManager.LoadScene(scene);
    }



}