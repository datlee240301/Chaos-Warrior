using UnityEngine;

public class SkillEffect1 : MonoBehaviour {
    public GameObject explosion;
    public Transform spawnPos;

    public void SpawnExplosion() {
        GameObject explodingEffect = Instantiate(explosion, spawnPos.position,spawnPos.rotation);
        Destroy(explodingEffect, 0.15f);
    }
}
