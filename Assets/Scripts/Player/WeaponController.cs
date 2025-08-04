using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private BoxCollider2D BoxCol;

    [SerializeField] private float swingStrength;
    private float swingTime;
    public float SwingFrame;
    private int swingCount;
    public Animator anim;
    private TopAnimController TAC;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        TAC = Player.GetComponentInChildren<TopAnimController>();
        BoxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        SwingFrame = TAC.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (TAC.IsSwinging && TAC.SwingCount == 1)
        {
            BoxCol.enabled = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            swingCount = 2;


        } else if (TAC.IsSwinging && TAC.SwingCount == 2)
        {
            BoxCol.enabled = true;
            transform.eulerAngles = new Vector3(180, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            swingCount = 1;


        } else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            BoxCol.enabled = false;
            SwingFrame = 0;
            swingCount = 1;
        }

        anim.SetBool("IsSwinging", TAC.IsSwinging);
        //anim.SetFloat("SwingFrame", SwingFrame);
    }

    public void Attack(int attackCount)
    {

    }
}
