using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyUIScript> enemies = new List<EnemyUIScript>();
    public GameObject enemyContainer;
    private PlayerScript playerScript;

    // Start is called before the first frame update
    private void Awake()
    {
        playerScript = PlayerScript.Instance;
    }
    public void spawnCombatInstance(string enemyType)
    {
        //loop through the enemy instances in the enemy container
        Object[] enemyInstances = Resources.LoadAll("EnemyInstances", typeof(GameObject));
        GameObject randomEnemyInstance;
        System.Random rand = new System.Random();
        do
        {
            randomEnemyInstance = enemyInstances[rand.Next(0, enemyInstances.Length)] as GameObject;
        } while (randomEnemyInstance.tag != enemyType || (randomEnemyInstance.tag != "Normal" && randomEnemyInstance.tag != "Elite" && randomEnemyInstance.tag != "Boss") );
        
        foreach (Transform child in enemyContainer.transform)
        {
            if (child)
            {
                Destroy(child.gameObject);
            }
        }
        StartCoroutine(AddNewEnemyInstances(randomEnemyInstance));
    }

    IEnumerator AddNewEnemyInstances(GameObject randomEnemyInstance)
    {
        yield return null; // Wait for one frame

        GameObject instantiatedRandomEnemyInstance = Instantiate(randomEnemyInstance, new Vector3(randomEnemyInstance.transform.position.x, randomEnemyInstance.transform.position.y, randomEnemyInstance.transform.position.z), Quaternion.identity);
        instantiatedRandomEnemyInstance.transform.SetParent(enemyContainer.transform, false);
        foreach (Transform childContainer in enemyContainer.transform)
        {
            if (childContainer)
            {
                //loop through the enemies in an enemy instance
                foreach (Transform enemyChild in childContainer.transform)
                {
                    if (!enemyChild.gameObject.activeSelf) enemyChild.gameObject.SetActive(true);
                    EnemyUIScript enemy = enemyChild.GetComponent<EnemyUIScript>();
                    enemies.Add(enemy);
                }
            }
        }
    }

    public void applyDamage() 
    {
        foreach (var enemy in enemies)
        {
            if (enemy.enemyAlive)
            {
                //reduce player health with the enemy's damage
                int currentPlayerHealth = playerScript.getHealth();
                currentPlayerHealth -= enemy.getEnemyDamage();
                playerScript.setHealth(currentPlayerHealth);
            }
        }
    }
}
