using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class GhostVisual : MonoBehaviour
{
    private static readonly int Die = Animator.StringToHash(IsDie);
    private static readonly int TakeHit = Animator.StringToHash(Takehit);
    private static readonly int Running = Animator.StringToHash(IsRunning);
    private static readonly int SpeedMultiplier = Animator.StringToHash(ChasingSpeedMultiplier);
    private static readonly int AttackHash = Animator.StringToHash(Attack);

    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private GameObject enemyShadow;

    private Animator animator;

    private const string IsRunning = "IsRunning";
    private const string Takehit = "TakeHit";
    private const string ChasingSpeedMultiplier = "ChasingSpeedMultiplier";
    private const string Attack = "Attack";
    private const string IsDie = "IsDie";

    SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += enemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += enemyEntity_OnTakeHit;
        enemyEntity.OnDeath += enemyEntity_OnDeath;
    }

    private void enemyEntity_OnDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(Die, true);
        spriteRenderer.sortingOrder = -1;
        enemyShadow.SetActive(false);
    }

    private void enemyEntity_OnTakeHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger(TakeHit);
    }


    private void Update() {
        animator.SetBool(Running, enemyAI.IsRunning);
        animator.SetFloat(SpeedMultiplier, enemyAI.GetRoamingAnimationSpeed());
    }

    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }

    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }

    private void enemyAI_OnEnemyAttack(object sender, System.EventArgs e)
    {
        animator.SetTrigger(AttackHash);
    }

    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack += enemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += enemyEntity_OnTakeHit;
        enemyEntity.OnDeath += enemyEntity_OnDeath;
    }
}
