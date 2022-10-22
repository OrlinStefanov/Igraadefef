using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GrandeCollisosn : MonoBehaviour
{
    public Transform Player;
    public float speed = 6f;
    public Rigidbody rb;
    public Image healthbar;
    public Transform Doge;
    public GameObject Explosion;

    public float cooldown = 2;
    public float cooldownTimer;

    Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 granadePosistion = new Vector3(transform.position.x, 1, transform.position.z);

        transform.position = granadePosistion;

        getPlayerPosition();

        cooldownTimer = cooldown;

        Explosion.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 granadePosistion = new Vector3(transform.position.x, 1, transform.position.z);

        transform.position = granadePosistion;

        GranadeMove();
        CoolDown();
    }

    private void getPlayerPosition()
    {
        playerPos = Player.transform.position;

        playerPos.y = 1;
    }

    private void GranadeMove()
    {
        if (cooldownTimer == 0)
        {
            Vector3 position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.fixedDeltaTime);

            rb.MovePosition(position);
        }

        if (transform.position == playerPos)
        {
            getPlayerPosition();
            GetExplosion();
            transform.position = Doge.transform.position;

            cooldownTimer = cooldown;
        }

        if (transform.position == Player.transform.position)
        {
            getPlayerPosition();
            GetExplosion();
            transform.position = Doge.transform.position;

            healthbar.fillAmount -= 0.1f;

            cooldownTimer = cooldown;
        }
    }

    private void CoolDown()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer < 0)
        {
            cooldownTimer = 0;
        }
    }

    private void GetExplosion()
    {
        Explosion.SetActive(true);
        Explosion.transform.position = transform.position;
    }
}
