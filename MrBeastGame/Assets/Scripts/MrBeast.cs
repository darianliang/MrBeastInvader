using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrBeast : MonoBehaviour
{

    public Sprite[] animationSprites;

    private float animationTime = 1.0f;

    private SpriteRenderer spriteRenderer;

    private int animationFrame;
    
    private float speed = 1.0f;

    private Vector3 direction = Vector2.left;

    public AudioSource audiosource;
    public AudioClip mrbeastsong;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }
    private void AnimateSprite() {
        animationFrame++;

        if (animationFrame >= this.animationSprites.Length) {
            animationFrame = 0;
        }

        spriteRenderer.sprite = this.animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Hand")) {      
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
            ScoreKeeper.AddToScore(100);

            audiosource.PlayOneShot(mrbeastsong);

        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * this.speed * Time.deltaTime;

        Vector3 leftedge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightedge = Camera.main.ViewportToWorldPoint(Vector3.right);

        if (direction == Vector3.right && this.transform.position.x >= (rightedge.x - 1.0f)) {
            direction.x *= -1.0f;
        }
        else if (direction == Vector3.left && this.transform.position.x <= (leftedge.x + 1.0f)) {
            direction.x *= -1.0f;
        }
    }
}
