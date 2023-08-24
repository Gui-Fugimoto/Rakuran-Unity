using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeCommand : MonoBehaviour
{
    /*
    public void DodgeRoll()
    {
        if (Input.GetKeyDown(dodgeKey) && !isRolling && UseStamina(staminaCost))
        {
            Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (movementDirection.magnitude > 0)
            {
                rollDirection = movementDirection.normalized;
                isRolling = true;
                rollStartTime = Time.time;
                invulnerabilityEndTime = Time.time + invulnerabilityDuration;
            }
        }

        if (isRolling)
        {
            float rollProgress = (Time.time - rollStartTime) / rollDuration;
            if (rollProgress < 1)
            {
                Vector3 rollVelocity = rollDirection * rollDistance / rollDuration;
                controller.Move((rollVelocity + rollDirection * rollSpeedBoost) * Time.deltaTime);
            }
            else
            {
                isRolling = false;
            }
        }
    }
    */
}
