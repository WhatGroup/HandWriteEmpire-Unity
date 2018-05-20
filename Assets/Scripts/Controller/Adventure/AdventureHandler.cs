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

    //Boss攻击倒计时显示
    public Text timeText;


    public UnityArmatureComponent attackRole;

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
        attackRole.AddDBEventListener(EventObject.COMPLETE, OnAnimationEventHandler);
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
                FadeInRoleAnim(attackRole, "behurt");
            }
        }

        timeText.text = bossFireTime.ToString("Boss : 0.00s");
    }

    public void JudgeResult(string btnName)
    {
        if (!WordHandler._instance.JudgeResult())
        {
            //TODO 错误效果
            FadeInRoleAnim(attackRole, "fail");
//            attackRole.animation.Play("normal");
        }
        else
        {
            //TODO 攻击效果
            if ("AttachBtn".Equals(btnName))
            {
                FadeInRoleAnim(attackRole, "attack");
//              attackRole.animation.Play("normal");
                AndroidUtil.Toast("攻击效果!!!");
            }
            else if ("CureBtn".Equals(btnName))
            {
                //TODO 需要修改成对应对象的动画
                FadeInRoleAnim(attackRole, "attack");

                AndroidUtil.Toast("治疗效果!!!");
            }
            else if ("DefensenBtn".Equals(btnName))
            {
                //TODO 需要修改成对应对象的动画
                attackRole.animation.FadeIn("attack", 0.2f, 1);

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
    }


    //动画完成后切换回默认动画
    private void OnAnimationEventHandler(string type, EventObject eventObject)
    {
        //TODO 攻击或失败动画播放完之后显示手写板
        var lastAnimationName = eventObject.armature.animation.lastAnimationName;
        if (lastAnimationName == "attack" || lastAnimationName == "fail")
        {
            GameSetting._instance.SetHWRModule(true);
            GameSetting._instance.PlayAnimState = false;
        }

        if (isNewAnim)
        {
            isNewAnim = false;
            if (!"".Equals(newAnimName))
            {
                StartCoroutine(DelayAnimPlay(attackRole, newAnimName, delayAnimPlayTime));
                newAnimName = "";
            }
        }
        else
        {
            PlayRoleAnim(attackRole, "normal");
            if (isNewRec)
            {
                isNewRec = false;
                SetNewWord();
            }

            isCalcTime = true;
        }
    }

    public void FadeInRoleAnim(UnityArmatureComponent role, string animName)
    {
        //TODO 播放攻击和失败动画时隐藏手写板
        if (animName == "attack" || animName == "fail")
        {
            GameSetting._instance.SetHWRModule(false);
            GameSetting._instance.PlayAnimState=true;

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