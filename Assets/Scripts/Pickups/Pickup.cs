using System.Collections;
using UnityEngine;
using DP.Utils;

public class Pickups : MonoBehaviour {



    [SerializeField] private PickupSO pickUpSO;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 0.5f;

    private Rigidbody2D rb;

    private Vector3 moveDirection;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update() {
        MagnetMoving();
    }

    private void MagnetMoving() {
        Vector3 playerPosition = Player.Instance.transform.position;
        if (Vector3.Distance(transform.position, playerPosition) < pickUpDistance) {
            moveDirection = (playerPosition - transform.position).normalized;
            moveSpeed += acceleration;
        } else {
            moveSpeed = 0;
            moveDirection = Vector3.zero;
        }
    }

    private void FixedUpdate() {
        rb.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            ProcessPickup();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine() {
        Vector2 startPoint = transform.position;

        float randomX = startPoint.x + Random.Range(-2f, 2f);
        float randomY = startPoint.y + Random.Range(-1f, 1f);
        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration) {

            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heighT = animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heighT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    private void ProcessPickup() {
        switch (pickUpSO.pickUpType) {
            case Utils.PickUpType.GoldCoin:
                Player.Instance.AddGoldCoin(pickUpSO.pickupAmount);
                break;
            case Utils.PickUpType.Heart:
                Player.Instance.HealPlayer(pickUpSO.pickupAmount);
                break;
            default:
                break;
        }
    }
}
