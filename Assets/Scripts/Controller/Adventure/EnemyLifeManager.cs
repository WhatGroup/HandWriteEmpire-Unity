using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeManager : MonoBehaviour
{
    [SerializeField] private BarStat enemyStat;

    public static EnemyLifeManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        InitEnemyData();
    }

    private void InitEnemyData()
    {
        //需要请求网络获取到对应的数据
        enemyStat.MaxVal = 100;
        enemyStat.CurrentVal = 100;
    }

    public void BeHurt(int reduceValue)
    {
        enemyStat.CurrentVal -= reduceValue;
    }

    public bool IsEnemyAlive()
    {
        return enemyStat.CurrentVal > 0 ? true : false;
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) enemyStat.CurrentVal -= 10;

        if (Input.GetKeyDown(KeyCode.S)) enemyStat.CurrentVal += 10;
    }
}