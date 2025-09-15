using UnityEngine;

public class ModifierScript : MonoBehaviour
{
    private enum ModifierType { Damage, Heal }
    [SerializeField]
    private ModifierType modifierType;
    PlayerLivesScript playerLivesScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;
        playerLivesScript = collision.GetComponent<PlayerLivesScript>();

        ModifyPlayerLife();
    }

    void ModifyPlayerLife()
    {
        switch(modifierType)
        {
            case ModifierType.Damage:
                Damage();
                break;

            case ModifierType.Heal:
                Heal();
                break;
        }
    }

    void Heal(int heal = 1)
    {
        playerLivesScript.GainLife(heal);
    }
    void Damage()
    {
        playerLivesScript.LoseLife();
    }
}
