using UnityEngine;
using UnityEngine.UI;


public class Portal : Enemy
{
    [SerializeField] private Image hpBar;

    protected override void Start()
    {
        base.Start();
        UpdateHPBar();
    }
    protected override void Update()
    {
        UpdateHPBar();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateHPBar();
    }
    private void UpdateHPBar()
    {
        if (hpBar != null) {

            hpBar.fillAmount = currentHp / maxHp;
    }
}
}
