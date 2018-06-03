using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using DragonBones;
using UnityEngine;
using UnityEngine.UI;

public class AdventureHandler : MonoBehaviour
{
    public static AdventureHandler _instance;


    //敌人攻击冷却时间
    public double BossFireTime;

    private double bossFireTime;

    //boss攻击的伤害
    [SerializeField] private int bossAttackValue;
    [SerializeField] private int bossBeHurtValue;

    //Boss攻击倒计时显示
    public Text timeText;


    public UnityArmatureComponent attackRole;
    public UnityArmatureComponent cureRole;
    public UnityArmatureComponent defensenRole;

    //是否是新的识别
    private bool isNewRec = false;

    //是否是新的动画
    private bool isNewAnim = false;
    private string newAnimName = "";

    //Boss攻击是否计时
    [HideInInspector] public bool isCalcTime = false;

    //演示显示游戏结束面板的时间
    public float delayShowGameOverTime = 2f;

    //当动画冲突时两个动画的播放间隔时间
    public float delayAnimPlayTime = 1f;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        bossFireTime = BossFireTime;


        //设置动画监听
        attackRole.AddDBEventListener(EventObject.COMPLETE, OnAttackAnimationEventHandler);
        cureRole.AddDBEventListener(EventObject.COMPLETE, OnCureAnimationEventHandler);
        defensenRole.AddDBEventListener(EventObject.COMPLETE, OnDefensenAnimationEventHandler);
    }

    private void Update()
    {
        if (isCalcTime)
        {
            //Boss攻击
            bossFireTime -= Time.deltaTime;
            if (bossFireTime <= 0)
            {
                isCalcTime = false;
                bossFireTime = BossFireTime;
//                FadeInRoleAnim(attackRole, "behurt");
//                FadeInRoleAnim(cureRole, "behurt");
                if (RoleLifeManager._instance.IsDefenseRoleAlive())
                {
                    FadeInRoleAnim(defensenRole, "behurt");
                    RoleLifeManager._instance.HurtRole(RoleInfo.DEFENSE, bossAttackValue);
                    if (!RoleLifeManager._instance.IsDefenseRoleAlive())
                    {
                        ScoreManager._instance.AddDeathRoleCount();
                        GameObject.FindGameObjectWithTag("DefenseRole").SetActive(false);
                    }
                }
                else if (RoleLifeManager._instance.IsAttachRoleAlive())
                {
                    FadeInRoleAnim(attackRole, "behurt");
                    RoleLifeManager._instance.HurtRole(RoleInfo.ATTACK, bossAttackValue);
                    if (!RoleLifeManager._instance.IsAttachRoleAlive())
                    {
                        GameObject.FindGameObjectWithTag("AttackRole").SetActive(false);
                        //如果攻击和防御角色死亡，则游戏结束
                        StartCoroutine(DelayShowGameOverPanel(2));
                    }
                }

                ScoreManager._instance.AddBeHurtCount();
            }
        }

        timeText.text = bossFireTime.ToString("Boss : 0.00s");
    }

    public void JudgeResult(string btnName)
    {
        if (!WordHandler._instance.JudgeResult())
        {
            //TODO 修改为具体的失败效果
            if ("AttachBtn".Equals(btnName))
            {
                FadeInRoleAnim(attackRole, "fail");
            }
            else if ("CureBtn".Equals(btnName))
            {
                //治疗角色没有错误的动画
//                FadeInRoleAnim(cureRole, "normal");
            }
            else if ("DefensenBtn".Equals(btnName))
            {
                FadeInRoleAnim(defensenRole, "fail");
            }
        }
        else
        {
            //TODO 修改为具体的效果
            if ("AttachBtn".Equals(btnName))
            {
                FadeInRoleAnim(attackRole, "attack");
                //动画播放完成造成伤害
//                EnemyLifeManager._instance.BeHurt(bossBeHurtValue);
//                if (!EnemyLifeManager._instance.IsEnemyAlive())
//                {
//                    ScoreManager._instance.IsSuccess = true;
//                    StartCoroutine(DelayShowGameOverPanel(2f));
//                }

                AndroidUtil.Toast("攻击效果!!!");
            }
            else if ("CureBtn".Equals(btnName))
            {
                FadeInRoleAnim(cureRole, "heal");
                AndroidUtil.Toast("治疗效果!!!");
            }
            else if ("DefensenBtn".Equals(btnName))
            {
                FadeInRoleAnim(defensenRole, "attack");
                AndroidUtil.Toast("防御效果!!!");
            }
        }

        isNewRec = true;
        isCalcTime = false;
    }

    //更新为新的单词
    private void SetNewWord()
    {
        if (WordHandler._instance.JudgeGameOver())
            StartCoroutine(DelayShowGameOverPanel(delayShowGameOverTime));
        else
            WordHandler._instance.UpdateWordInfo();

        isNewRec = false;
    }

    private IEnumerator DelayShowGameOverPanel(float second)
    {
        yield return new WaitForSeconds(second);
        GameSetting._instance.SetGameOver(true);
        //TODO 判断游戏是否胜利
        if (ScoreManager._instance.IsSuccess)
        {
            GameSetting._instance.VictoryPanel.SetActive(true);
            SetVictoryData();
        }
        else
        {
            GameSetting._instance.FailPanel.SetActive(true);
            SetFailData();
        }
    }

    private void SetFailData()
    {
    }

    private void SetVictoryData()
    {
        GameObject victoryPanel = GameSetting._instance.VictoryPanel;
        FlagStateController flagState = victoryPanel.GetComponent<FlagStateController>();
        TaskStateContorller taskState = victoryPanel.GetComponent<TaskStateContorller>();
        RewardController rewardController = victoryPanel.GetComponent<RewardController>();
        //任务完成情况
        if (ScoreManager._instance.IsDefeatAllEnemy())
        {
            taskState.taskOne.GetComponent<Image>().sprite = taskState.rightSprite;
        }
        else
        {
            taskState.taskOne.GetComponent<Image>().sprite = taskState.errorSprite;
        }

        if (ScoreManager._instance.IsLessErrorWord(4))
        {
            taskState.taskTwo.GetComponent<Image>().sprite = taskState.rightSprite;
        }
        else
        {
            taskState.taskTwo.GetComponent<Image>().sprite = taskState.errorSprite;
        }

        if (ScoreManager._instance.IsAllRoleLife())
        {
            taskState.taskThree.GetComponent<Image>().sprite = taskState.rightSprite;
        }
        else
        {
            taskState.taskThree.GetComponent<Image>().sprite = taskState.errorSprite;
        }

        taskState.taskOne.SetNativeSize();
        taskState.taskTwo.SetNativeSize();
        taskState.taskThree.SetNativeSize();

        //奖励分
        rewardController.attackReward.text = "+" + ScoreManager._instance.RewordAttackValue();
        rewardController.defenseReward.text = "+" + ScoreManager._instance.RewordDefenseValue();
        rewardController.cureReward.text = "+" + ScoreManager._instance.RewordCureValue();
        //旗子
        switch (ScoreManager._instance.GetRewardFlagNum())
        {
            case 1:
                flagState.flagImage.sprite = flagState.flagOne;
                break;
            case 2:
                flagState.flagImage.sprite = flagState.flagTwo;
                break;
            case 3:
                flagState.flagImage.sprite = flagState.flagThree;
                break;
        }
    }


    //动画完成后切换回默认动画
    private void OnAttackAnimationEventHandler(string type, EventObject eventObject)
    {
        //TODO 攻击或失败动画播放完之后显示手写板
        var lastAnimationName = eventObject.armature.animation.lastAnimationName;
        if (lastAnimationName == "attack" || lastAnimationName == "fail")
        {
            GameSetting._instance.SetHWRModule(true);
            GameSetting._instance.PlayAnimState = false;
            //敌人受伤显示
            EnemyLifeManager._instance.BeHurt(bossBeHurtValue);
            if (!EnemyLifeManager._instance.IsEnemyAlive())
            {
                ScoreManager._instance.IsSuccess = true;
                StartCoroutine(DelayShowGameOverPanel(2f));
            }
        }

        OnAnimationEventHanler(attackRole);
    }

    private void OnCureAnimationEventHandler(string type, EventObject eventObject)
    {
        //TODO 攻击或失败动画播放完之后显示手写板
        var lastAnimationName = eventObject.armature.animation.lastAnimationName;
        if (lastAnimationName == "heal" || lastAnimationName == "fail")
        {
            GameSetting._instance.SetHWRModule(true);
            GameSetting._instance.PlayAnimState = false;
        }

        OnAnimationEventHanler(cureRole);
    }

    private void OnDefensenAnimationEventHandler(string type, EventObject eventObject)
    {
        //TODO 攻击或失败动画播放完之后显示手写板
        var lastAnimationName = eventObject.armature.animation.lastAnimationName;
        if (lastAnimationName == "attack" || lastAnimationName == "fail")
        {
            GameSetting._instance.SetHWRModule(true);
            GameSetting._instance.PlayAnimState = false;
        }

        OnAnimationEventHanler(defensenRole);
    }

    private void OnAnimationEventHanler(UnityArmatureComponent role)
    {
        if (isNewAnim)
        {
            isNewAnim = false;
            if (!"".Equals(newAnimName))
            {
                StartCoroutine(DelayAnimPlay(role, newAnimName, delayAnimPlayTime));
                newAnimName = "";
            }
        }
        else
        {
            PlayRoleAnim(role, "normal");
            if (isNewRec)
            {
                isNewRec = false;
                SetNewWord();
            }

            isCalcTime = true;
        }
    }


    //TODO 如果同时显示播放多人失败动画的时候，手写板会冲突
    public void FadeInRoleAnim(UnityArmatureComponent role, string animName)
    {
        if (animName == "attack" || animName == "fail" || animName == "heal")
        {
            GameSetting._instance.SetHWRModule(false);
            GameSetting._instance.PlayAnimState = true;
        }

        if (role.animation.lastAnimationName != "normal")
        {
            isNewAnim = true;
            newAnimName = animName;
        }
        else
        {
            role.animation.FadeIn(animName, 0.2f, 1);
        }
    }

    public void PlayRoleAnim(UnityArmatureComponent role, string animName)
    {
        role.animation.Play(animName, 1);
    }

    private IEnumerator DelayAnimPlay(UnityArmatureComponent role, string animName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlayRoleAnim(role, animName);
    }
}