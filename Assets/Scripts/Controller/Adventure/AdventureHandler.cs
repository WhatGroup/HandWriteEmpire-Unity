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
    public AudioClip attackClip;
    public AudioClip defenseClip;
    public AudioClip cureClip;
    public AudioClip enemyClip;

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
    public UnityArmatureComponent defenseRole;
    public UnityArmatureComponent enemy;

    [SerializeField] private int defenseRemainValue;

    //是否是新的识别
    private bool isNewRec = false;

    //是否是新的动画
    private bool isAttackNewAnim = false;
    private string newAttackAnimName = "";
    private bool isDefenseNewAnim = false;
    private string newDefenseAnimName = "";
    private bool isCureNewAnim = false;
    private string newCureAnimName = "";

    //Boss攻击是否计时
    [HideInInspector] public bool isCalcTime = false;

    //演示显示游戏结束面板的时间
    public float delayShowGameOverTime = 2f;

    //当动画冲突时两个动画的播放间隔时间
    public float delayAnimPlayTime = 1f;

    private void Awake()
    {
        if (UserInfoManager._instance.GetUserInfo() == null) BackHandler._instance.GoToMain();

        _instance = this;
    }

    private void Start()
    {
        bossFireTime = BossFireTime;

        //设置动画监听
        attackRole.AddDBEventListener(EventObject.COMPLETE, OnAttackAnimationEventHandler);
        cureRole.AddDBEventListener(EventObject.COMPLETE, OnCureAnimationEventHandler);
        defenseRole.AddDBEventListener(EventObject.COMPLETE, OnDefensenAnimationEventHandler);
        enemy.AddDBEventListener(EventObject.COMPLETE, OnEnemyAnimationEventHandler);
    }

    private void Update()
    {
        if (isCalcTime)
        {
            //Boss攻击
            bossFireTime -= Time.deltaTime;
            if (bossFireTime <= 0)
            {
                bossFireTime = BossFireTime;
                FadeInAnim(enemy, "attack");
                if (PrefsManager.SilderState.Equals("ture"))
                    AudioSource.PlayClipAtPoint(enemyClip, new Vector3(), PrefsManager.Volume);
                if (RoleLifeManager._instance.IsDefenseRoleAlive())
                    if (defenseRemainValue > 0)
                        FadeInAnim(defenseRole, "defencing_hurt");
                    else
                        FadeInAnim(defenseRole, "behurt");
                else if (RoleLifeManager._instance.IsAttachRoleAlive())
                    FadeInAnim(attackRole, "behurt");

                ScoreManager._instance.AddBeHurtCount();
            }
        }

        timeText.text = bossFireTime.ToString("Boss : 0.00s");
    }

    public void JudgeResult(string btnName)
    {
        if (!WordHandler._instance.JudgeResult())
        {
            if ("AttachBtn".Equals(btnName))
            {
                FadeInAnim(attackRole, "fail");
            }
            else if ("CureBtn".Equals(btnName))
            {
                //TODO 治疗角色没有错误的动画
//                FadeInRoleAnim(cureRole, "normal");
            }
            else if ("DefensenBtn".Equals(btnName))
            {
                //TODO 攻击角色错误动画
                FadeInAnim(defenseRole, "fail");
            }
        }
        else
        {
            //TODO 修改为具体的效果
            if ("AttachBtn".Equals(btnName))
            {
                FadeInAnim(attackRole, "attack");
                FadeInAnim(enemy, "behurt");
                if (PrefsManager.SilderState.Equals("ture"))
                    AudioSource.PlayClipAtPoint(attackClip, new Vector3(), PrefsManager.Volume);
                //已经修改为在动画播放完成造成伤害
//                EnemyLifeManager._instance.BeHurt(bossBeHurtValue);
//                if (!EnemyLifeManager._instance.IsEnemyAlive())
//                {
//                    ScoreManager._instance.IsSuccess = true;
//                    StartCoroutine(DelayShowGameOverPanel(2f));
//                }

//                AndroidUtil.Toast("攻击效果!!!");
            }
            else if ("CureBtn".Equals(btnName))
            {
                FadeInAnim(cureRole, "heal");
//                AndroidUtil.Toast("治疗效果!!!");
                if (PrefsManager.SilderState.Equals("ture"))
                    AudioSource.PlayClipAtPoint(cureClip, new Vector3(), PrefsManager.Volume);
            }
            else if ("DefensenBtn".Equals(btnName))
            {
                FadeInAnim(defenseRole, "defence");
//                AndroidUtil.Toast("防御效果!!!");
                if (PrefsManager.SilderState.Equals("ture"))
                    AudioSource.PlayClipAtPoint(defenseClip, new Vector3(), PrefsManager.Volume);
            }
        }

        SetNewWord();
        isNewRec = true;
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
        if (ScoreManager._instance.IsGameSuccess())
        {
            //更新网络数据
            WordHandler._instance.UpdateLevelData();
            GameSetting._instance.VictoryPanel.SetActive(true);
            SetVictoryData();
        }
        else
        {
            GameSetting._instance.FailPanel.SetActive(true);
            SetFailData();
        }

        //TODO 更新网络数据
        HttpUtil.PostUserInfo(this);
        HttpUtil.PostUserLevelInfos(this);
        HttpUtil.PostErrorWordInfos(this);
    }

    private void SetFailData()
    {
    }

    private void SetVictoryData()
    {
        var victoryPanel = GameSetting._instance.VictoryPanel;
        var flagState = victoryPanel.GetComponent<FlagStateController>();
        var taskState = victoryPanel.GetComponent<TaskStateContorller>();
        var rewardController = victoryPanel.GetComponent<RewardController>();
        //任务完成情况
        if (ScoreManager._instance.IsDefeatAllEnemy())
            taskState.taskOne.GetComponent<Image>().sprite = taskState.rightSprite;
        else
            taskState.taskOne.GetComponent<Image>().sprite = taskState.errorSprite;

        if (ScoreManager._instance.IsLessErrorWord(4))
            taskState.taskTwo.GetComponent<Image>().sprite = taskState.rightSprite;
        else
            taskState.taskTwo.GetComponent<Image>().sprite = taskState.errorSprite;

        if (ScoreManager._instance.IsAllRoleLife())
            taskState.taskThree.GetComponent<Image>().sprite = taskState.rightSprite;
        else
            taskState.taskThree.GetComponent<Image>().sprite = taskState.errorSprite;

        taskState.taskOne.SetNativeSize();
        taskState.taskTwo.SetNativeSize();
        taskState.taskThree.SetNativeSize();

        //奖励分
        rewardController.attackReward.text = "+" + ScoreManager._instance.RewordAttackValue();
        UserInfoManager._instance.AttackValue += ScoreManager._instance.RewordAttackValue();
        rewardController.defenseReward.text = "+" + ScoreManager._instance.RewordDefenseValue();
        UserInfoManager._instance.DefenseValue += ScoreManager._instance.RewordDefenseValue();
        rewardController.cureReward.text = "+" + ScoreManager._instance.RewordCureValue();
        UserInfoManager._instance.CureValue += ScoreManager._instance.RewordCureValue();
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
        var lastAnimationName = eventObject.armature.animation.lastAnimationName;
        if (lastAnimationName == "attack")
        {
            //敌人受伤显示
            EnemyLifeManager._instance.BeHurt(UserInfoManager._instance.GetAttackRoleSkillValue());

            if (!EnemyLifeManager._instance.IsEnemyAlive())
            {
//                ScoreManager._instance.IsSuccess = true;
                //TODO
                //敌人消失
                var enemyGo = GameObject.FindGameObjectWithTag("Enemy");
                if (enemyGo != null)
                {
                    enemyGo.SetActive(false);
                    bossFireTime = 987654321;
                }

                ScoreManager._instance.AddDefeatEnemyCount();
                StartCoroutine(DelayShowGameOverPanel(2f));
            }
        }
        else if (lastAnimationName == "behurt")
        {
            RoleLifeManager._instance.HurtRole(RoleInfo.ATTACK, bossAttackValue);
            if (!RoleLifeManager._instance.IsAttachRoleAlive())
            {
                var attackRoleGo = GameObject.FindGameObjectWithTag("AttackRole");

                if (attackRoleGo != null)
                    attackRoleGo.SetActive(false);
                //如果攻击和防御角色死亡，则游戏结束
                StartCoroutine(DelayShowGameOverPanel(2));
            }
        }


        if (isAttackNewAnim)
        {
            isAttackNewAnim = false;
            if (!"".Equals(newAttackAnimName))
            {
                StartCoroutine(DelayAnimPlay(attackRole, newAttackAnimName, delayAnimPlayTime));
                newAttackAnimName = "";
            }
        }
        else
        {
            PlayAnim(attackRole, "normal");
            if (isNewRec) isNewRec = false;
        }
    }

    private void OnCureAnimationEventHandler(string type, EventObject eventObject)
    {
        var lastAnimationName = eventObject.armature.animation.lastAnimationName;
        if (lastAnimationName == "heal")
            if (RoleLifeManager._instance.IsDefenseRoleAlive())
                RoleLifeManager._instance.CureRole(RoleInfo.DEFENSE, UserInfoManager._instance.GetCureRoleSkillValue());
            else if (RoleLifeManager._instance.IsAttachRoleAlive())
                RoleLifeManager._instance.CureRole(RoleInfo.ATTACK, UserInfoManager._instance.GetCureRoleSkillValue());

        if (isCureNewAnim)
        {
            isCureNewAnim = false;
            if (!"".Equals(newCureAnimName))
            {
                StartCoroutine(DelayAnimPlay(cureRole, newCureAnimName, delayAnimPlayTime));
                newCureAnimName = "";
            }
        }
        else
        {
            PlayAnim(cureRole, "normal");
            if (isNewRec) isNewRec = false;
        }
    }

    private void OnDefensenAnimationEventHandler(string type, EventObject eventObject)
    {
        var lastAnimationName = eventObject.armature.animation.lastAnimationName;
        if (lastAnimationName == "defence")
        {
            defenseRemainValue = UserInfoManager._instance.GetDefenseRoleSkillValue();
        }
        else if (lastAnimationName == "behurt")
        {
            RoleLifeManager._instance.HurtRole(RoleInfo.DEFENSE, bossAttackValue);
            if (!RoleLifeManager._instance.IsDefenseRoleAlive())
            {
                ScoreManager._instance.AddDeathRoleCount();
                var defenseRoleGo = GameObject.FindGameObjectWithTag("DefenseRole");
                if (defenseRoleGo != null) defenseRoleGo.SetActive(false);
            }
        }
        else if (lastAnimationName == "defencing_hurt")
        {
            defenseRemainValue -= bossBeHurtValue;
            if (defenseRemainValue <= 0)
            {
                defenseRemainValue = 0;
                FadeInAnim(defenseRole, "defencing_finish");
            }
        }


        //        OnAnimationEventHanler(defenseRole);
        if (isDefenseNewAnim)
        {
            isDefenseNewAnim = false;
            if (!"".Equals(newDefenseAnimName))
            {
                StartCoroutine(DelayAnimPlay(defenseRole, newDefenseAnimName, delayAnimPlayTime));
                newDefenseAnimName = "";
            }
        }
        else if (defenseRemainValue > 0)
        {
            PlayAnim(defenseRole, "defencing_normal");
            if (isNewRec) isNewRec = false;
        }
        else
        {
            PlayAnim(defenseRole, "normal");
            if (isNewRec) isNewRec = false;
        }
    }

    private void OnEnemyAnimationEventHandler(string type, EventObject eventObject)
    {
        PlayAnim(enemy, "normal");
    }

    //TODO 如果同时显示播放多人失败动画的时候，手写板会冲突
    public void FadeInAnim(UnityArmatureComponent role, string animName)
    {
        if (animName == "attack" || animName == "fail" || animName == "heal" || animName == "defence")
        {
//            GameSetting._instance.SetHWRModule(false);
//            GameSetting._instance.PlayAnimState = true;
        }

        if (role.animation.lastAnimationName != "normal")
        {
            if (role == attackRole)
            {
                isAttackNewAnim = true;
                newAttackAnimName = animName;
            }
            else if (role == defenseRole)
            {
                isDefenseNewAnim = true;
                newDefenseAnimName = animName;
            }
            else if (role == cureRole)
            {
                isCureNewAnim = true;
                newCureAnimName = animName;
            }
        }
        else
        {
            role.animation.FadeIn(animName, 0.2f, 1);
        }
    }

    public void PlayAnim(UnityArmatureComponent role, string animName)
    {
        role.animation.Play(animName, 1);
    }

    private IEnumerator DelayAnimPlay(UnityArmatureComponent role, string animName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        PlayAnim(role, animName);
    }
}