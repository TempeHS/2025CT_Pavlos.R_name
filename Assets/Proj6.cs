using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proj6 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(release());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator release()
    {


        yield return new WaitForSeconds(2.3f);
        Destroy(gameObject);
    }
}
