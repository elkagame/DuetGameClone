using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    #region Singleton class: Splatter;

    public static Splatter Instance;

    void Awake()
    {
        if(Instance == null)
        Instance = this;
    }
    #endregion

    [SerializeField] Color[] colors = new Color[2];
    [SerializeField] GameObject splatterPrefabs;
    [SerializeField] Sprite[] splatterSprites;

    public void AddSplatter(Transform obstacle, Vector3 pos, int colorIndex)
    {
        GameObject splatter = Instantiate(splatterPrefabs,pos,Quaternion.Euler(new Vector3(0f,0f,Random.Range(-320f,320f))),obstacle);
        SpriteRenderer sr = splatter.GetComponent<SpriteRenderer>();
        sr.color = colors[colorIndex];
        sr.sprite = splatterSprites[Random.Range(0,splatterSprites.Length)];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
