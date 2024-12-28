using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 6.0f;

    public Projectile handPrefab;

    private bool handActive;

    public GameOver gameover;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {            
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }        
        else if (Input.GetKey(KeyCode.D)) {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.transform.position = new Vector3(0,-8,0);
        }
    }

    private void Shoot() {
        if (!handActive) {
            Projectile projectile = Instantiate(this.handPrefab, this.transform.position, Quaternion.identity);        
            projectile.destroyed += handDestroyed;
            handActive = true;
        }
    }

    private void handDestroyed() {
        handActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Money")) {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameover.Setup();
            this.gameObject.GetComponent<Player>().enabled = false;
            
        }
    }
}
