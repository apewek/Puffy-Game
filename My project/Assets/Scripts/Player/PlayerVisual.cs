using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private static readonly int AttackHash = Animator.StringToHash(Attack);
    private static readonly int Die = Animator.StringToHash(IsDie);
    private static readonly int Running = Animator.StringToHash(IsRunning);

    [SerializeField] private Bite bite;


    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private FlashBlink flashBlink;

    private const string Attack = "Attack";
    private const string IsRunning = "IsRunning";
    private const string IsDie = "IsDie";

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashBlink = GetComponent<FlashBlink>();
    }

    private void Start()
    {
        bite.OnBite += Bite_OnBite;
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e)
    {
        animator.SetBool(Die, true);
        flashBlink.StopBlinking();
    }

    private void Bite_OnBite(object sender, System.EventArgs e)
    {
        animator.SetTrigger(AttackHash);
    }

    private void Update()
    {
        animator.SetBool(Running, Player.Instance.IsRunning());

        if (Player.Instance.IsAlive())
            AdjustPlayerFacingDirection();
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x > playerPosition.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    public void TriggerEndAttackAnimation()
    {
        bite.AttackColliderTurnOff();
    }

    private void OnDestroy()
    {
        bite.OnBite -= Bite_OnBite;
        Player.Instance.OnPlayerDeath -= Player_OnPlayerDeath;
    }
}
