using UnityEngine;

using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class test : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject target; // target sprite
    [SerializeField] Rigidbody2D objectToShoot;

    [SerializeField] float spawnDelay = 5f;
    [SerializeField] float destroyAfter = 5f;



    private bool canShoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red, 5f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                target.transform.position = hit.point;
                Debug.Log("Clicked on: " + hit.collider.name);

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                StartCoroutine(ShootWithDelay(projectileVelocity));
            }
        }
    }

    IEnumerator ShootWithDelay(Vector2 velocity)
    {
        canShoot = false;
        yield return new WaitForSeconds(spawnDelay);

        Rigidbody2D newProjectile = Instantiate(objectToShoot, shootPoint.position, Quaternion.identity);
        newProjectile.linearVelocity = velocity;
        Destroy(newProjectile.gameObject, destroyAfter); //destroy after not use.

        yield return new WaitForSeconds(0.1f); // allowing next shoot
        canShoot = true;
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        return new Vector2(velocityX, velocityY);
    }


}