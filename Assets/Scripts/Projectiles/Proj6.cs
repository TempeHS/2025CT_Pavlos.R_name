using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj6 : MonoBehaviour
{

    private SpriteRenderer rend;

    private Material swirl;
    // Start is called before the first frame update

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.material = new Material(rend.material);
    }
    void Start()
    {
        swirl = rend.material;
        StartCoroutine(release());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator release()
    {

        float strong = 0;
        swirl.SetFloat("_strength", 0);
        swirl.SetFloat("_Radius", 0);
        for (int i = 0; i <= 20; i++)
        {


            strong += 0.05f;
            swirl.SetFloat("_strength", strong);
            swirl.SetFloat("_Radius", strong);
            yield return new WaitForSeconds(0.0125f);
        }

        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i <= 20; i++)
        {


            strong -= 0.05f;
            swirl.SetFloat("_strength", strong);
            swirl.SetFloat("_Radius", strong);
            yield return new WaitForSeconds(0.0125f);
        }
        Destroy(gameObject);
    }
}
