using UnityEngine;
using UnityEngine.AI;
using DP.Utils;
using System;

public class EnemyAI : MonoBehaviour {

    [SerializeField] private State startingState;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingDistanceMax = 7f;

    [SerializeField] private bool isChasingEnemy = false;
    [SerializeField] private float chasingRange = 4f;
    [SerializeField] private float chasingSpeedMutltiplayer = 2f;

    [SerializeField] private bool isAttackingEnemy = false;
    [SerializeField] private MonoBehaviour shooter;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private float fireRate = 2f;
    private float nextAttackTime = 0f;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float roamingTimer;

    private float nextCheckDirectionTime = 0f;
    private float checkDirectionDuration = 0.1f;
    private Vector3 lastPosition;

    private float chasingSpeed;
    private float walkingSpeed;

    private NavMeshAgent agent;
    private State state;

    public event EventHandler OnAttack;


    public enum State {
        Idle,
        Roaming,
        Chasing,
        AttackingTarget,
        Death
    }

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        state = startingState;

        walkingSpeed = agent.speed;
        chasingSpeed = agent.speed * chasingSpeedMutltiplayer;
    }

    private void Update() {
        StateHandler();
        MovementDirectionHandler();
    }


    private void MovementDirectionHandler() {
        if (Time.time > nextCheckDirectionTime) {
            if (IsRunning()) {
                ChangeFacingDirection(lastPosition, transform.position);
            } else if (state == State.AttackingTarget) {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
            }

            lastPosition = transform.position;
            nextCheckDirectionTime = Time.time + checkDirectionDuration;
        }
    }


    private void StateHandler() {
        switch (state) {
            case State.Roaming:
                roamingTimer -= Time.deltaTime;
                if (roamingTimer < 0f) {
                    Roaming();
                    roamingTimer = roamingTimerMax;
                }

                state = GetCurrentState();
                break;
            case State.Chasing:
                ChasingTarget();
                state = GetCurrentState();
                break;
            case State.AttackingTarget:
                AttackingTarget();
                state = GetCurrentState();
                break;
            default:
            case State.Idle:
                break;
            case State.Death:
                break;
        }
    }


    private State GetCurrentState() {

        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Roaming;

        if (isChasingEnemy) {
            if (distanceToPlayer <= chasingRange && distanceToPlayer > attackRange) {
                newState = State.Chasing;
            }
        }

        if (isAttackingEnemy) {
            if (distanceToPlayer <= attackRange && Player.Instance.IsAlive()) {
                newState = State.AttackingTarget;
            }
        }


        if (newState != state) {
            state = newState;
            if (newState == State.AttackingTarget) {
                agent.ResetPath();
            } else if (newState == State.Chasing) {
                agent.ResetPath();
                agent.speed = chasingSpeed;
            } else if (newState == State.Roaming) {
                roamingTimer = 0f;
                agent.speed = walkingSpeed;
            }
        }

        return state;
    }


    private void ChasingTarget() {
        agent.SetDestination(Player.Instance.transform.position);
    }

    private void AttackingTarget() {
        if (Time.time > nextAttackTime) {
            if (shooter) {
                (shooter as IEnemyShooter).Attack();
            }

            OnAttack?.Invoke(this, EventArgs.Empty);
            nextAttackTime = Time.time + fireRate;
        }
    }

    private void Roaming() {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        agent.SetDestination(roamPosition);
    }

    private Vector3 GetRoamingPosition() {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition) {
        if (sourcePosition.x > targetPosition.x) {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public float GetWalkingAnimationSpeed() {
        return agent.speed / walkingSpeed;
    }

    public bool IsRunning() {
        if (agent.velocity == Vector3.zero) {
            return false;
        } else {
            return true;
        }
    }

    public void SetDeathState() {
        agent.ResetPath();
        state = State.Death;
    }

    public float GetFireRate() {
        return fireRate;
    }

}
