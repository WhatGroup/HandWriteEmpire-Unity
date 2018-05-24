### 汇总
接口
* api/user_data 获取用户的相关料
* api/level_infos 获取用户所有关卡的信息
* api/level_info 获取某个关卡的数据

资源目录
* res/images/portrait   存放用户的头像
* res/images/rolePortrait   存放角色的头像
* res/level_infos   存放用户关卡的数据
* res/level_info    具体关卡的内容

### 1. 获取用户数据
地址:api/user_data  
传参: token  
返回值:  
参数              |描述          |类型
:---:            | :---:        | :---:
portrait         | 用户头像路径  | string 
defenseProperty  | 防御值       | int
attackProperty   | 攻击值       | int
cureProperty     | 治疗值       | int
roleInfos         | 当前已获得角色信息|json
levelInfosUrl     | 用户关卡数据保存的路径|string 

roleInfos参数
参数              |描述          |类型
:---:            | :---:        | :---:
state   |   是否是选中状态  |   boolean
name    |   选取的角色名称  |   string
type    |   角色的类型      |   string
value   |   属性值          |   int
rolePortrait    | 角色的头像 | string

请求示例:api/user_data?token=abcdef0123456  
返回示例:
```json
{
    "portrait": "res/images/portrait/img_20180524111830.jpg",
    "attackProperty": 100,
    "defenseProperty": 200,
    "cureProperty": 300,
    "roleInfos": [
        {
            "rolePortrait":"res/images/rolePortrait/img_1356.jpg",
            "state":"1",
            "name": "XXX",
            "type": "attack",
            "value": 100,
        },
        {
            "rolePortrait":"res/images/rolePortrait/img_1236.jpg",
            "state":"1",
            "name": "YYY",
            "type": "defense",
            "value": 200
        },
        {
            "rolePortrait":"res/images/rolePortrait/img_3456.jpg",
            "state":"1",
            "name": "ZZZ",
            "type": "cure",
            "value": 300
        },
        {
            "rolePortrait":"res/images/rolePortrait/img_1231.jpg",
            "state":"0",
            "name": "AAA",
            "type": "cure",
            "value": 300
        }
    ],
    "levelInfosUrl":"res/LevelInfos/levelInfos_20180524111830"
}
```
### 2. 获取全部关卡数据
地址: api/level_infos  
传参: token  
返回值: 关卡数据
参数 |描述|类型
:---:|:---:|:---
state   |   该关卡的状态:ok/current/lock    |   string
flag    |   当前关卡获得的旗子数:current和lock对应的值为0，ok对应的值为1/2/3    |    int
data    |   本关卡数据保存的路径    |   string
level   |   当前的关卡数    |   int
请求示例: api/level_infos?token=abcdef0123456  
返回示例:
```json
[
    {
        "state": "ok",
        "flag": 1,
        "data": "res/level_info/1.json",
        "level": 1
    },
    {
        "state": "current",
        "flag": 0,
        "data": "res/level_info/2.json",
        "level": 2
    },
    {
        "state": "lock",
        "flag": 0,
        "data": "res/level_info/3/json",
        "level": 3
    }
]
```
### 3. 获取关卡的详细数据
地址: api/level_info  
传参: token  
返回值:  
参数 |描述|类型
:--:|:--:|:--
pinyin  |   词语对应的拼音  |   string
content |   具体的词语     |   string
pinyin  |   词语的解释      |   string

请求示例: api/level_info?token=abcdef0123456  
返回示例
```json
[
    {
        "pinyin": "xiàn zài",
        "content": "现 在",
        "detail": "现世,今生;眼前一刹那"
    },
    {
        "pinyin": "wèi lái",
        "content": "未 来",
        "detail": "从现在往后的时间"
    }
]
```
