using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";  

    private float inputHorizontal;
    private float inputVertical;
    private Player player;

    public void Init(Player player)
    {
        this.player = player;        
    }
    
    
    void Update()
    {
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        if (player.canRotationTowardJoystick)
        {
            if ((player.isFacingRigt && inputHorizontal < 0) || (!player.isFacingRigt && inputHorizontal > 0))
            {
                player.Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2 (inputHorizontal, inputVertical)*player.speed;        
    }
}


