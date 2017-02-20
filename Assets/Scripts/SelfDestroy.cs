using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

    public float selfDestroyTimer;
    public float layer;
	// Use this for initialization
	void Awake ()
    {
        gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        selfDestroyTimer -= Time.deltaTime;
        if (selfDestroyTimer <= 0)
        {
            Destroy(gameObject);
        }
	}
}
