using System.Collections;
using UnityEngine;

public class MedusaSkill : MonoBehaviour
{
    //public GameObject target;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerController.instance.gameObject.SetActive(false);
        MedusaController.instance.StopAllCoroutines();
        MedusaController.instance.StartCoroutine(MedusaController.instance.MoveRoutine2());
        //MedusaController.instance.StartCoroutine(MedusaController.instance.MoveRoutine2());
        //MedusaController.instance.StopCoroutine(MedusaController.instance.MoveRoutine());
        //target.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ActivateTarget() {
        yield return new WaitForSeconds(3);
        animator.SetBool("isDestroy", true);
    }

    public void DestroyRock() {
        PlayerController.instance.gameObject.SetActive(true);
        //MedusaController.instance.StartCoroutine(MedusaController.instance.MoveRoutine());
        MedusaController.instance.StopAllCoroutines();
        MedusaController.instance.StartCoroutine(MedusaController.instance.MoveRoutine());
        Destroy(gameObject);
        //target.SetActive(true);
    }
}
