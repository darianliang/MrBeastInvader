using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Moneys : MonoBehaviour
{
    public Money[] prefabs;
    public int rows = 3;
    public int cols = 8;
    public float speed = 3.0f;
    private Vector3 direction = Vector2.right;
    public int grabbed;
    public int total = 24;

    public GameOver gameover;
    public YouWin youwin;
    public Player player;

    private void Awake() {
        for (int row = 0; row < this.rows; row++) {
            float width = 2.0f * (this.cols - 1);
            float height = 2.0f * (this.rows - 1);
            Vector3 rowpos = new Vector3(-width/2, -height/2 + (row * 1.5f), 0.0f);

            for (int col = 0; col < this.cols; col++) {
                Money money = Instantiate(this.prefabs[row], this.transform);
                money.grabbed += shot;
                Vector3 pos = rowpos;
                pos.x += col * 2.0f;
                money.transform.localPosition = pos;
            }
        }
    }

    private void shot() {
        this.grabbed++;

        if (this.grabbed >= this.total) {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            youwin.Setup();
            player.gameObject.GetComponent<Player>().enabled = false;
            player.gameObject.GetComponent<Projectile>().enabled = false;
            player.gameObject.GetComponent<MrBeast>().enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        this.transform.position += direction * this.speed * Time.deltaTime;

        Vector3 leftedge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightedge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (Input.GetKey(KeyCode.R)) {            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    

        foreach (Transform money in this.transform) {
            if (!money.gameObject.activeInHierarchy) {
                continue;
            }
            if ((direction == Vector3.right && money.position.x >= (rightedge.x - 1.0f)) || (direction == Vector3.left && money.position.x <= (leftedge.x + 1.0f))) {
                GoDown();
            }

        }
    }

    private void GoDown() {
        direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }
    
}