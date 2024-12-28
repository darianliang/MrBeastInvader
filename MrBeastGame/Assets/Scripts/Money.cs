using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{

    public Sprite[] animationSprites;

    private float animationTime = 0.6f;

    private int animationFrame;

    public System.Action grabbed;

    private SpriteRenderer spriteRenderer;

    public GameOver gameover;
    public Player player;

    public AudioSource audiosource;
    public AudioClip chaching;


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
            this.grabbed.Invoke();
            this.gameObject.SetActive(false);
            ScoreKeeper.AddToScore(10);
            
            audiosource.PlayOneShot(chaching);

        }
        // gameover.Setup();
        // player.gameObject.GetComponent<Player>().enabled = false;
        // player.gameObject.GetComponent<Projectile>().enabled = false;
        // player.gameObject.GetComponent<MrBeast>().enabled = false;
        

    }

}
