using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjStats : MonoBehaviour
{
    [SerializeField] public float damage;
    private int proj;



    // Start is called before the first frame update
    void Start()
    {
        search();
    }

    // Update is called once per frame
    void Update()
    {

            switch (proj)
            {
                case 1:
                damage = 1;
                break;
            }

    }
    void search()
    {
        if (gameObject.TryGetComponent<Tags>(out var tags))
        {
            if (tags.HasTag("Proj1"))
            {
                proj = 1;
            }

        }
    }

}
