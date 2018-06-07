using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelListHandler : MonoBehaviour, HttpUtil.ICallBack
{
    public GameObject LevelItemPrefab;

    public GameObject LevelListGo;

    private Transform levelListTransform;

    //列表当前的位置
    private int currentLevelPositionY;

    private List<LevelInfo> levelList;


    private void Start()
    {
        levelListTransform = LevelListGo.GetComponent<Transform>();
        //TODO 需不需要判断网络是否更新了数据
        if (LevelDict.Instance.IsEmpty())
            RequestLevelInfos();
        else
            initLevelListItem();
    }

    private void Update()
    {
        //测试滚动列表功能，注意这里的位置为负数（和设置的Pivot有关,而且不能是localPosition）
//        if (Input.GetMouseButtonDown(1))
//            SetPanelPositionY(levelListTransform, (int) levelListTransform.position.y - 100);
    }

    //获取关卡的数据
    private void RequestLevelInfos()
    {
        HttpUtil.GetUserLevelInfos(this, this);
    }


    public void OnRequestError(string error)
    {
        AndroidUtil.Toast("网络出错!\n" + error);
        StartCoroutine(LaterRequest(HttpUtil.RetryNetWorkTime));
    }

    private IEnumerator LaterRequest(float second)
    {
        yield return new WaitForSeconds(second);
        RequestLevelInfos();
    }

    public void OnRequestSuccess(long responseCode, string response)
    {
        print(response);
        if (responseCode == 200)
        {

            //加载关卡列表
            var levelInfos =
                JsonUtility.FromJson<UserLevelInfoList>(response);
            levelList = levelInfos.userLevelInfos;
            foreach (var levelInfo in levelList) LevelDict.Instance.AddLevelInfo(levelInfo);

            initLevelListItem();
        }
    }

    private void initLevelListItem()
    {
        var levelCount = LevelDict.Instance.GetCount();
        for (var i = 1; i <= levelCount; i++)
        {
            var levelItem = Instantiate(LevelItemPrefab);
            levelItem.transform.SetParent(LevelListGo.transform, false);
            var controller = levelItem.GetComponent<LevelItemContorller>();
            //控制图标的位置，偶数在左，奇数在右
            if (i % 2 == 0)
                controller.SetItemContentLeft();
            else
                controller.SetItemContentRight();

            //设置激活应该面板
            var levelInfo = LevelDict.Instance.GetLevelInfo(i);
            controller.SetActiveState(levelInfo);

            //判断列表需要跳转到的位置
            if (levelInfo.state.Equals(LevelInfo.CURRENT))
            {
                var current = levelInfo.level;
                if (LevelDict.Instance.GetCount() >= 4)
                    if (current >= 3)
                    {
                        var positionY = (current - 2) * 450 + 50;
                        currentLevelPositionY = -positionY;
                    }
            }
        }

        //设置levelList当前定位的位置
        //使用协程解决网络加载数据时，列表自动跳转问题
        StartCoroutine(DirectPosition());
    }

    private IEnumerator DirectPosition()
    {
        yield return new WaitForEndOfFrame();
        //等待帧数据处理完之后再设置LevelList列表当前的位置
        SetPanelPositionY(levelListTransform, currentLevelPositionY);
    }


    //设置关卡图标在右

    private void SetPanelPositionY(Transform transform, int positionY)
    {
        var temp = transform.position;
        temp.y = positionY;
        transform.position = temp;
    }
}