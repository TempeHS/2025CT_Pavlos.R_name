using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator fade()
    {

        for(int i = 0; i < 100; i++)
        {
            if(GetComponent<SpriteRenderer>().color.a > 0)
            {
               GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, GetComponent<SpriteRenderer>().color.a - 0.1f);
            }

            yield return new WaitForSeconds(0.005f);
        }

    }
}
