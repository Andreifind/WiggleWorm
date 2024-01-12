using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnPeriod = 3;

    [SerializeField]
    GameObject[] littleStones;
    [SerializeField]
    GameObject[] longStones;
    [SerializeField]
    GameObject coin;
    [SerializeField]
    private int coinDropChancePerStoneSpawnPrecent=25;

    private const float stoneSizeInUnitsFromZeroToSides = 1f;
    private const float mid = 0;
    //private float rightLimit = -2;   //Border at -2
    //private float leftLimit = 2;  //Border at 2
    [SerializeField]
    private float ratio = 2f; //Border at 2 (This is the left limit, left border, but named as ratio xD)
    private float bottomLimit = -6f;

    private bool spawn = true;
    private float coinSpawnPosition;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            spawn = false;
            Invoke("Spawn", spawnPeriod);
        }

    }

    void Spawn()
    {
        spawn = true;

        int randomLittleStone = Random.Range(0, littleStones.Length);

        int coinSpawnChance = Random.Range(0, 101);
        bool spawnCoin = coinSpawnChance <= coinDropChancePerStoneSpawnPrecent ? true : false;

        int spawnCase = Random.Range(0, 3);
        switch (spawnCase)
        {
            //2 chietre
            case 0:
                float randomOffset = Random.Range(-0.5f, 0.5f);
                int randomLeftStone = Random.Range(0, longStones.Length);
                int randomRightStone = Random.Range(0, longStones.Length);

                Instantiate(longStones[randomLeftStone], new Vector2(ratio + randomOffset, bottomLimit), Quaternion.identity);
                Instantiate(longStones[randomRightStone], new Vector2(-ratio + randomOffset, bottomLimit), transform.rotation * Quaternion.Euler(0f, 180f, 0f));

                if (spawnCoin)
                    Instantiate(coin, new Vector2(mid + randomOffset, bottomLimit), Quaternion.identity);
                
                #region Trash
                /*if (variation==0)
                {
                    Instantiate(longStones[0], new Vector2(ratio+ randomOffset, bottomLimit), Quaternion.identity);        //0 is the long stone
                    Instantiate(longStones[0], new Vector2(-ratio+ randomOffset, bottomLimit), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                }
                else if (variation==1)
                {
                    Instantiate(longStones[0], new Vector2(ratio+ randomOffset, bottomLimit), Quaternion.identity);        //0 is the long stone
                    Instantiate(longStones[1], new Vector2(-ratio+ randomOffset, bottomLimit), Quaternion.identity);
                }
                else if (variation==2)
                {
                    Instantiate(longStones[1], new Vector2(ratio+ randomOffset, bottomLimit), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                    Instantiate(longStones[0], new Vector2(-ratio+ randomOffset, bottomLimit), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                }
                else //if (variation==3)
                {
                    Instantiate(longStones[1], new Vector2(-ratio+ randomOffset, bottomLimit), Quaternion.identity);        //0 is the long stone
                    Instantiate(longStones[1], new Vector2(ratio+ randomOffset, bottomLimit), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
                }*/
                #endregion
                break;

            //1 chietra miojloc
            case 1:
                //Stone Area
                //Instantiate(spawnObjects[randomStone], new Vector2(mid, bottomLimit), Quaternion.identity);
                Instantiate(littleStones[randomLittleStone], new Vector2(mid, bottomLimit), Quaternion.identity);

                //Coin Area
                if (spawnCoin)
                {
                    //To remove Loops and Coroutins:
                    bool left = Random.Range(0, 2) == 0 ? true : false;
                    coinSpawnPosition = Random.Range(-ratio, ratio);               
                    if (left)
                        coinSpawnPosition = Random.Range(-ratio, -stoneSizeInUnitsFromZeroToSides);                  //-0.9F the size of the stone from 0. ~ -0.9F -> 0.9F (almost 2 units)
                    else
                        coinSpawnPosition = Random.Range(stoneSizeInUnitsFromZeroToSides, ratio);                  
                    Instantiate(coin, new Vector2(coinSpawnPosition, bottomLimit), Quaternion.identity);
                }

                break;

            //1 chiatra random
            case 2:
                //Stone Area
                //Instantiate(spawnObjects[randomStone], new Vector2(Random.Range(ratio, -ratio), bottomLimit), Quaternion.identity);
                GameObject spawnedStone = Instantiate(littleStones[randomLittleStone], new Vector2(Random.Range(ratio, -ratio), bottomLimit), Quaternion.identity);

                //Coin Area
                if (spawnCoin)
                {
                    if (spawnedStone.transform.position.x>0)
                        coinSpawnPosition = Random.Range(-ratio, -stoneSizeInUnitsFromZeroToSides);  //0.9F the size of the stone from 0. ~ -0.9F -> 0.9F (almost 2 units)
                    else
                        coinSpawnPosition = Random.Range(stoneSizeInUnitsFromZeroToSides, ratio);  
                    if (spawnCoin)
                        Instantiate(coin, new Vector2(coinSpawnPosition, bottomLimit), Quaternion.identity);
                }

                break;

        }
    }
}
