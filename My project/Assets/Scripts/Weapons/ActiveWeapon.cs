using UnityEngine;

public class ActiveWeapon : MonoBehaviour {

    public static ActiveWeapon Instance { get; private set; }
    [SerializeField] private Bite bite;

    private void Awake() {
        Instance = this;
    }

    private void Update()
    {
        AdjustAttackDirection();
    }

    public Bite GetActiveWeapon() {
        return bite;
    }

    private void AdjustAttackDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePos.x > playerPosition.x) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
