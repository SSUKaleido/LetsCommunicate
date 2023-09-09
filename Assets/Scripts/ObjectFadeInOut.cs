using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public GameObject[] FadePannel = new GameObject[5];

    SpriteRenderer sr;
    public Image[] Renderer = new Image[5];
    public GameObject go;
    public Image[] Story_Image = new Image[5];

    bool[] isImgFadeInDone = new bool[5];
    bool[] isImgFadeInStart = new bool[5];

    // Start is called before the first frame update
    void Start()
    {
        /*
        sr = go.GetComponent<SpriteRenderer>();
        for (int i = 0; i < isImgFadeInDone.Length; i++)
        {
            isImgFadeInDone[i] = false;
            Renderer[i] = Story_Image[i].GetComponent<Image>();
        }
        */
        StartCoroutine("FadeInStart", 0);
        isImgFadeInStart[0] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
            StartCoroutine("FadeIn");

        if (Input.GetKeyDown("o"))
            StartCoroutine("FadeOut");

        for (int i = 1; i < isImgFadeInDone.Length; i++)
        {
            if (isImgFadeInDone[i - 1] == true && isImgFadeInStart[i] == false)
            {
                StoryProgress(i);
                isImgFadeInStart[i] = true;
                break;
            }
        }
    }

    void StoryProgress(int Index)
    {

        StartCoroutine("FadeInStart",Index);
        Debug.Log(Index);
    }

    public IEnumerator FadeInStart(int Index)
    {
        FadePannel[Index].SetActive(true);
        for (float f = 1f; f > 0; f -= 0.01f)
        {
            Color c = FadePannel[Index].GetComponent<Image>().color;
            c.a = f;
            FadePannel[Index].GetComponent<Image>().color = c;
            yield return new WaitForSeconds((float)(0.1));
        }
        yield return new WaitForSeconds(1);
        isImgFadeInDone[Index] = true;
        FadePannel[Index].SetActive(false);
    }


    /*
    IEnumerator FadeIn(int Index)
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;

            Renderer[Index].color = new Color(Renderer[Index].color.r, Renderer[Index].color.g, Renderer[Index].color.b, 1f);
            //Color c = Renderer[Index].material.color;
            //c.a = f;
            //Renderer[Index].material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOut(int Index)
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = Renderer[Index].material.color;
            c.a = f;
            Renderer[Index].material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void FadeInBtn()
    {
        StartCoroutine("FadeIn");
    }

    public void FadeOutBtn()
    {
        StartCoroutine("FadeOut");
    }
    */
}