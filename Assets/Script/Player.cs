using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject wallPrefab;
    public GameObject sandEffect;
    public Transform shootingPoint; 
    private Animator animator; 

    public float sphereSpeed; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootFlamingSphere();
        }

        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(CreateWall());
        }
    }

    void ShootFlamingSphere()
    {
        animator.SetTrigger("ShootTrigger");

        GameObject newSphere = Instantiate(spherePrefab, shootingPoint.position, shootingPoint.rotation);
        Rigidbody rb = newSphere.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootingPoint.forward * sphereSpeed; 
        }
        Destroy(newSphere, 5f);
    }

    IEnumerator CreateWall()
    {

        animator.SetTrigger("WallTrigger");

        yield return new WaitForSeconds(1.4f);  

        Vector3 wallPosition = shootingPoint.position + shootingPoint.forward * 50f;
        GameObject wall = Instantiate(wallPrefab, wallPosition, Quaternion.Euler(360f, transform.eulerAngles.y + 630f, 0));

        Instantiate(sandEffect, wallPosition, Quaternion.identity);

        StartCoroutine(RaiseWall(wall));

        Destroy(wall, 2.5f);
    }

    IEnumerator RaiseWall(GameObject wall)
    {
        Vector3 startPosition = wall.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 5f, 0); 

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            wall.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        wall.transform.position = targetPosition; 
    }
}
