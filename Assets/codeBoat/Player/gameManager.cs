using UnityEngine;

public class gameManager : MonoBehaviour
{
    public float playerLvl;
    public float playerMoney;
    public float fishtSpawnTime;

    public virtual void money()
    {
        playerMoney = 100;
    }
}
