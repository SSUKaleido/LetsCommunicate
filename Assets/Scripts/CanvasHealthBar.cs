using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHealthBar : MonoBehaviour
{
    public List<GameObject> obj;
    public List<GameObject> hp_bar;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            hp_bar[i].transform.position = obj[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            hp_bar[i].transform.position = obj[i].transform.position + new Vector3(0, 1f, 0);
            //Camera.main.WorldToScreenPoint(obj[i].transform.position + new Vector3(0, 1f, 0));
        }
    }
}