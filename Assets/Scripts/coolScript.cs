using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class coolScript : MonoBehaviour
{
    [SerializeField]
    private Tag _tagCheck;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Tags>(out var tags))
        {
            
            Debug.Log($"Is Enemy? {tags.HasTag("Enemy")}");
        }
    }
}
