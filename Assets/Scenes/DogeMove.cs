using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DogeMove : MonoBehaviour
{
    public Transform Player;
    public float speed = 4f;
    public Rigidbody rb;
    public Image healthBar;
    float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 250f;
        healthBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.fixedDeltaTime);

        rb.MovePosition(pos);
        transform.LookAt(Player);
    }


}
