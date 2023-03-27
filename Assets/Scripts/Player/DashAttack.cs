using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashAttack: MonoBehaviour
{
    private Quaternion rotation;
    private Vector3 lastDashDir;
    private Vector3 trans;
    private Vector3 position;
    public float dash = 1f;
    [SerializeField]
    private GameObject dashzone;
    [SerializeField]
    private PlayerController controller;


    public void DashMovement()
    {
        #region pruebas
        float dashDistance = dash;
        Debug.Log("primer paso dash");
        DashDir();
        Rigidbody2D rb = PersistentManager.Instance.PlayerGlobal.GetComponent<Rigidbody2D>();
        Transform tr = PersistentManager.Instance.PlayerGlobal.transform;

        Vector3 beforeDashPosition = tr.position;
        Debug.Log("segundo paso dash");

        if (TryDash(lastDashDir, dashDistance))
        {
            Debug.Log("entraste");
        }
        #endregion

        //funciona pocho a
        tr.position = dashzone.gameObject.transform.position;
        //funciona pocho b
        //rb.MovePosition(rb.position  * dashDistance * Time.fixedDeltaTime);
    }

    #region dashtests

    private bool CanDash(Vector3 dir, float distance)
    {
        Transform tr = PersistentManager.Instance.PlayerGlobal.transform;
        return Physics2D.Raycast(tr.position, dir, distance).collider == null;
    }

    private bool TryDash(Vector3 actualDirection, float distance)
    {
        Vector3 dashDir = actualDirection;
        bool canDash = CanDash(dashDir, distance);
        if (canDash == false)
        {
            Debug.Log("no puedes dashear en x");

            dashDir = new Vector3(actualDirection.x, 0f).normalized;
            canDash = dashDir.x != 0f && CanDash(dashDir, distance);
            if (!canDash)
            {
                Debug.Log("no puedes dashear en y");
                dashDir = new Vector3(0f, actualDirection.y).normalized;
                canDash = dashDir.y != 0f && CanDash(dashDir, distance);
            }
        }

        if (canDash)
        {
            Debug.Log("puedes dashear");

            Transform tr = PersistentManager.Instance.PlayerGlobal.transform;
            lastDashDir = dashDir;
            tr.position += dashDir * distance;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DashDir() //Set dash direction and position
    {
        Debug.Log("dir dash");

        position = PersistentManager.Instance.PlayerGlobal.transform.position;
        switch (PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().dir) //Check player facing direction to set arrow direction and position
        {
            case PlayerController.Direction.UP:
                Debug.Log("dir dash up");
                rotation = Quaternion.Euler(0, 0, 90);                                        //Set arrow facing up
                trans = PersistentManager.Instance.PlayerGlobal.transform.up;
                position.y = position.y + 0.11f;
                break;
            case PlayerController.Direction.DOWN:
                rotation = Quaternion.Euler(0, 0, -90);
                trans = PersistentManager.Instance.PlayerGlobal.transform.up * -1;
                position.y = position.y - 0.20f;
                break;
            case PlayerController.Direction.LEFT:
                trans = PersistentManager.Instance.PlayerGlobal.transform.right * -1;
                rotation = Quaternion.Euler(0, 0, 180);
                position.x = position.x + 0.04f;
                break;
            case PlayerController.Direction.RIGHT:
                trans = PersistentManager.Instance.PlayerGlobal.transform.right;
                rotation = Quaternion.Euler(0, 0, 0);
                position.x = position.x + 0.04f;
                break;

        }
    }
    #endregion
}
