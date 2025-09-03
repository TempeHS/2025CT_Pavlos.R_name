using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoppleGanger : MonoBehaviour
{
    [SerializeField] private Material Purple;
    [SerializeField] private float value;
    [SerializeField] private float hue;
    [SerializeField] private float saturation;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject Player;
    [SerializeField] private float PlayerOffset = 10;
    private bossAi bAi;


    void Start()
    {
        value = 0f;
        Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / value, 0));
        bAi = boss.GetComponent<bossAi>();
        gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

    }

    public void StartSpread()
    {
        StartCoroutine(Spread());
    }

    public void StartSpread2()
    {
        StartCoroutine(Spread2());
    }


    IEnumerator Spread()
    { 
        transform.position = Player.transform.position;
        for(int i = 0; i < 100; i++)
        {
            value = i;
            Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / 100, value / 100));
            yield return new WaitForSeconds(.02f);
        }

        value = 0f;
        Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / 100, 0 / 100));
        boss.transform.position = transform.position;
        for(int  j = 0; j < 3; j++)
        {
            bAi.attack2Con(3);
            yield return new WaitForSeconds(.15f);
        }

        StartCoroutine(SpreadCon());
        //gameObject.SetActive(false);

    }
    IEnumerator Spread2()
    {
        transform.position = Player.transform.position;
        for (int i = 0; i < 100; i++)
        {
            value = i;
            Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / 100, value / 100));
            yield return new WaitForSeconds(.02f);
        }

        value = 0f;
        Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / 100, 0 / 100));
        boss.transform.position = transform.position;

        for(int i = 0; i < 15; i++)
        {
            bAi.attack2Con(Random.Range(3, 7));
            yield return new WaitForSeconds(.2f);
        }
        gameObject.SetActive(false);

    }

    IEnumerator SpreadCon()
    {
        for(int i = 0; i < 3; i++)
        {
            transform.position = new Vector2(Player.transform.position.x + PlayerOffset, Player.transform.position.y);
            for (int j = 0; j < 100; j++)
            {
                value = j;
                Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / 100, value / 100));
                yield return new WaitForSeconds(.001f);
            }

            boss.transform.position = transform.position;
            for (int j = 0; j < 3; j++)
            {
                bAi.attack2Con(3);
                yield return new WaitForSeconds(.1f);
            }

            value = 0f;
            Purple.SetColor("_Purple", Color.HSVToRGB(hue / 360, saturation / 100, 0 / 100));
            PlayerOffset = PlayerOffset * -1;
        }
        gameObject.SetActive(false);
    }
}
