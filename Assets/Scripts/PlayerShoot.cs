using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static PlayerShoot instance;
    public GameObject arrowPrefab;
    public Transform shootingPoint;
    public GameObject skill1Effect;
    public Transform skill1EffectPos1,skill1EffectPos2,skill1EffectPos3, fireballSpawnPos, fireballSpawnPos2, fireballSpawnPos3;
    public GameObject prepareSkill1Effect, fireball;
    public float speed;
    private void Awake() {
        instance = this;
    }
    private void Update() {
        
    }

    public IEnumerator Shoot() {
        GameObject arrow = GameObject.Instantiate(arrowPrefab, shootingPoint.position,shootingPoint.rotation);
        Rigidbody2D arroRb = arrow.GetComponent<Rigidbody2D>();
        Vector2 originalScale = arrow.transform.localScale;
        arrow.transform.localScale = new Vector2(originalScale.x * transform.localScale.x > 0 ? 1 : -1, originalScale.y);
        arroRb.velocity = new Vector2(speed * arrow.transform.localScale.x, arrow.transform.localScale.y);
        yield return new WaitForSeconds(2);
        Destroy(arrow);
    }

    public IEnumerator SpawnSkil1Effect() {

        GameObject skill11 = Instantiate(skill1Effect, skill1EffectPos1.position, skill1EffectPos1.rotation);
        GameObject skill12 = Instantiate(skill1Effect, skill1EffectPos2.position, skill1EffectPos2.rotation);
        GameObject skill13 = Instantiate(skill1Effect, skill1EffectPos3.position, skill1EffectPos3.rotation);

        
        yield return new WaitForSeconds(0.65f);
        Destroy(skill11);
        Destroy(skill12);
        Destroy(skill13);
    }
    public IEnumerator SpawnPrepareSkill1() {
        GameObject skill11 = Instantiate(prepareSkill1Effect, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.45f);
        Destroy(skill11);
    }

    public void SpawnFireball() {
        GameObject fire1 = Instantiate(fireball, fireballSpawnPos.position, skill1EffectPos1.rotation);
        GameObject fire2 = Instantiate(fireball, fireballSpawnPos2.position, skill1EffectPos2.rotation);
        GameObject fire3 = Instantiate(fireball, fireballSpawnPos3.position, skill1EffectPos2.rotation);
    }
}
