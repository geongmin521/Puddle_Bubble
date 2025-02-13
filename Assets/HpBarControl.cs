
using UnityEngine;

public class HpBarControl : MonoBehaviour
{
    Monster monster;
    float maxHp;
    RectTransform rect;

    void Start()
    {
        monster = this.transform.parent.parent.GetComponent<Monster>();
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, 0.1f);
        maxHp = monster.Health;
    }

    void Update()
    {
        if (monster.Health < maxHp)
        {
            rect.sizeDelta = new Vector2(monster.Health / maxHp, 0.1f);
        }       
    }
}
