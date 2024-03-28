using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenieShoot : MonoBehaviour {
    public GameObject magic;
    public Transform shootingPoint;
    public float speed = 10f;



    public IEnumerator Shoot() {
        GameObject arrow = GameObject.Instantiate(magic, shootingPoint.position, shootingPoint.rotation);
        Rigidbody2D arroRb = arrow.GetComponent<Rigidbody2D>();
        Vector2 originalScale = arrow.transform.localScale;
        arrow.transform.localScale = new Vector2(originalScale.x * transform.localScale.x > 0 ? 1 : -1, originalScale.y);
        arroRb.velocity = new Vector2(speed * arrow.transform.localScale.x, arrow.transform.localScale.y);
        yield return new WaitForSeconds(2);
        Destroy(arrow);
    }
}
