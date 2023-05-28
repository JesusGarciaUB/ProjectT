using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DashAttack: MonoBehaviour
{

    float maxDashLength = 1f;
    float minDashLength = 0.1f;

    Vector3 safeDashPosition;
    Vector3 position;

    int layerMask = 1 << 11;
    private Vector3 actualDirection;
    private float cooldownTime = 2f;
    private float timeSinceLastDash = 0f;
    [SerializeField] Rigidbody2D rb;
    Animator animator;
    public AudioClip clipsound;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceLastDash += Time.deltaTime;
        position = PersistentManager.Instance.PlayerGlobal.transform.position + (Vector3)PersistentManager.Instance.PlayerGlobal.GetComponent<CapsuleCollider2D>().offset; //position of the feet
        safeDashPosition = CalculateSafeDashPosition();
    }

    private void OnDash()
    {

        if (Vector3.Distance(position, safeDashPosition) > minDashLength)
            Dash();
    }
    private IEnumerator SetSoundDash()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.clip = clipsound;
        audio.Play();
    }

    private void Dash()
    {
        if (timeSinceLastDash > cooldownTime)
        {
            SetSoundDash();
            timeSinceLastDash = 0.0f;
            animator.SetTrigger("dashAttack");
            PersistentManager.Instance.PlayerGlobal.transform.position = safeDashPosition - (Vector3)PersistentManager.Instance.PlayerGlobal.GetComponent<CapsuleCollider2D>().offset;
            PersistentManager.Instance.dashUI.usedAbility(Mathf.FloorToInt(cooldownTime));
        }
    }
    
    Vector3 CalculateSafeDashPosition()
    {
        layerMask = LayerMask.GetMask("CollisionWall");

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        Color rayColor = Color.green;
        float delta = 0.0f;
        switch (PersistentManager.Instance.PlayerGlobal.GetComponent<PlayerController>().dir)
        {
            case PlayerController.Direction.NONE:
                actualDirection = Vector3.zero;
                break;
            case PlayerController.Direction.UP:
                delta = 0.08f;
                actualDirection = Vector3.up;
                break;
            case PlayerController.Direction.DOWN:
                delta = 0.08f;
                actualDirection = Vector3.down;
                break;
            case PlayerController.Direction.LEFT:
                delta = 0.05f;
                actualDirection = Vector3.left;
                break;
            case PlayerController.Direction.RIGHT:
                delta = 0.05f;
                actualDirection = Vector3.right;
                break;
        }

        for (int i = -1; i <= 1; i++) {
            hits.Add(Physics2D.Raycast(position + Vector3.Cross(Vector3.forward, actualDirection) * delta * (float)i, actualDirection, maxDashLength, layerMask));
            Debug.DrawRay(position + Vector3.Cross(Vector3.forward, actualDirection) * delta * (float)i, actualDirection * maxDashLength, rayColor);
        }

        for(int i = hits.Count - 1; i >= 0; i--)
        {
            if (hits[i].transform == null)
                hits.RemoveAt(i);
        }

        bool hitSomething = hits.Count > 0;

        if (hitSomething)
        {
            Vector3 closest = hits[0].point - (Vector2)actualDirection * delta * 2f;
            Debug.Log(hits.Count);
            Debug.DrawRay(position, closest - position, Color.yellow);

            for (int i = 1; i < hits.Count; i++)
            {
                Debug.DrawRay(position, (hits[i].point - (Vector2)actualDirection * delta * 2f) - (Vector2)position, Color.yellow);

                if (Vector3.Distance(position, closest) > Vector3.Distance(position, hits[i].point - (Vector2)actualDirection * delta * 2f))
                {
                    closest = hits[i].point - (Vector2)actualDirection * delta * 2f;
                }
            }
            Debug.DrawRay(position, Vector3.Project(closest - position, actualDirection), Color.magenta);
            return Vector3.Project(closest - position, actualDirection) + position;
        }
        else
        {
            Debug.DrawRay(position, actualDirection * maxDashLength - actualDirection * delta * 2f, Color.yellow);
            return position + actualDirection * maxDashLength - actualDirection * delta * 2f;
        }
    }
}
