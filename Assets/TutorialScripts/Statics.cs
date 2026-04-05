using UnityEngine;
using System.Collections;

public class TutEnemy
{
    public static int enemyCount = 0;
    public TutEnemy()
    {
        enemyCount++;
    }
}

public class TutGame
{
    void Start()
    {
        TutEnemy enemy1 = new TutEnemy();
        TutEnemy enemy2 = new TutEnemy();
        TutEnemy enemy3 = new TutEnemy();

        int x = TutEnemy.enemyCount;
    }
}

public class TutPlayer : MonoBehaviour
{
    public static int playerCount = 0;

    void Start()
    {
        playerCount++;
    }
}

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        int y = TutPlayer.playerCount;
    }
}

public static class Utilities
{
    public static int Add(int num1, int num2)
    {
        return num1 + num2;
    }
}

public class UtilitiesExample : MonoBehaviour
{
    void Start()
    {
        int z = Utilities.Add(5, 6);
    }
}