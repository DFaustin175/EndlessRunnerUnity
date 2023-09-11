using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    GameManager gameManager;
    public enum PlatformJob
    {
        Spawn,
        Destroy
    }
    public PlatformJob job;
    public GameObject platform;
    // Start is called before the first frame update
    private void Awake()
    { gameManager = GameManager.Instance; }
    void Start()
    {
        if(gameManager != null) { Debug.Log("PlatformManager: Game Manager Found"); }
        if(job == PlatformJob.Spawn) { Instantiate(platform, transform.position, Quaternion.identity); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (job == PlatformJob.Destroy) { Destroy(collision.gameObject); }
        else if(job == PlatformJob.Spawn && collision.gameObject.tag == "Platform") { Instantiate(platform, transform.position, Quaternion.identity); }
    }
}
