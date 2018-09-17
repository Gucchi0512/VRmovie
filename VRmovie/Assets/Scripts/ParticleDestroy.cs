using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour {

    private void OnParticleCollision(GameObject other) {
        Debug.Log("touch");
        Destroy(this.gameObject);
    }
}
