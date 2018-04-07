using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SOLOHandler : MonoBehaviour
{
    public Text effectTipText;

    private static int count = 0;

    void Start()
    {
        //unity嵌入Android时显示手写板
//        if (Application.platform == RuntimePlatform.Android)
//        {
//            ShowHWRModule();
//        }
    }

    void Update()
    {
        //unity嵌入Android隐藏手写板
//        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
//        {
//            HideHWRModule();
//        }
    }

    public void HideHWRModule()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("hideHWRModule");
    }

    private void ShowHWRModule()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("showHWRModule");
    }

    public void ShowEffect(String result)
    {
        effectTipText.text = result;
    }

    public void TestHWRRec()
    {
        short[] s = new short[]
        {
            383, 174, 373, 188, 356, 213, 332, 248, 304, 286, 277, 322, 254, 355, 235, 378, 221, 402, 211, 421, 207,
            435, 207, 445, 208, 451, 210, 453, 217, 458, 227, 466, 242, 479, 264, 499, 289, 523, 314, 551, 339, 586,
            364, 619, 384, 649, 404, 678, 420, 702, 431, 721, 441, 739, 447, 751, 450, 758, 451, 763, 451, 764, -1, 0,
            415, 415, 410, 416, 393, 445, 363, 499, 330, 557, 295, 610, 262, 659, 232, 698, 212, 721, 198, 733, 189,
            737, 184, 739, 185, 738, -1, 0, 159, 401, 176, 397, 219, 392, 274, 384, 325, 379, 374, 374, 417, 369, 449,
            366, 469, 364, 483, 363, 486, 363, 487, 363, -1, 0, 649, 227, 663, 224, 683, 224, 705, 224, 726, 228, 743,
            232, 757, 240, 767, 252, 770, 270, 764, 304, 752, 338, 739, 370, 729, 393, 722, 414, 719, 429, 718, 442,
            718, 448, 720, 451, 722, 457, 727, 467, 729, 481, 732, 498, 734, 519, 732, 551, 729, 591, 727, 628, 724,
            655, 722, 681, 719, 702, 716, 719, 716, 730, 714, 736, 713, 740, 711, 744, 708, 745, 703, 743, 689, 738,
            657, 715, 621, 682, 595, 648, 572, 619, 568, 614, -1, 0, 531, 468, 551, 465, 587, 463, 635, 461, 685, 460,
            742, 459, 793, 457, 833, 457, 863, 458, 884, 460, 898, 461, 904, 463, 905, 463, -1, 0, -1, -1
        };
        String result = HWRRecog(s);
        effectTipText.text = "第" + count++ + "次识别:" + result;
    }

    public String HWRRecog(short[] s)
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        return jo.Call<String>("hwrRec", s);
    }
}