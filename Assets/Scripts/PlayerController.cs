using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 screenCentre;
    [SerializeField] private GameObject aimSphere;
    public int maxHealth = 100; 
    public int currentHealth;

    public float sensitivity = 2.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public Text healthText;

    public Timer timer;
    public float stopTimeInSeconds = 2f; 

    private void Awake()
    {
        screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Cursor.lockState = CursorLockMode.Locked;
        currentHealth = maxHealth; 
    }

    private void Update()
    {
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationY += Input.GetAxis("Mouse X") * sensitivity;

        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0.0f); 

        healthText.text = "Health: " + currentHealth.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            Ray screenRay = Camera.main.ScreenPointToRay(screenCentre);
            RaycastHit hit;

            if (Physics.Raycast(screenRay, out hit))
            {
                aimSphere.transform.position = hit.point;
                if (hit.collider.CompareTag("Enemy"))
                {
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(20); 
                    }
                }
            }
            else
            {
                aimSphere.transform.position = screenRay.GetPoint(200f);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            timer.StopTimerForSeconds(stopTimeInSeconds);

            StartCoroutine(ReloadSceneCoroutine());
        }
    }

    private System.Collections.IEnumerator ReloadSceneCoroutine()
    {
        yield return new WaitForSeconds(stopTimeInSeconds);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
