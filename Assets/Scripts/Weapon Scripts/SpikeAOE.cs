using System.CodeDom.Compiler;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeAOE : MonoBehaviour
{
    public float duration = 0.5f;
    public int damage = 10;

    private float spawnTime;
    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        
        
        
        if (Time.time - spawnTime >= duration)
        {
            Destroy(gameObject);
        }
    }



}
