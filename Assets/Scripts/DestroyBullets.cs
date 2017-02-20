using UnityEngine;
using System.Collections;

public class DestroyBullets : MonoBehaviour {
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
	}

