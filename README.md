# Rule_Nonsense - 规则怪谈生存游戏

> 一款基于规则怪谈题材的独立恐怖生存游戏

---

## 游戏简介

**Rule_Nonsense** 是一款第一人称恐怖生存解谜游戏，灵感来源于规则怪谈类小说。

### 故事背景

你醒来发现自己身处一间诡异的病房中。这里有着不可名状的存在，以及一系列必须遵守的"规则"。病房中处处透着诡异，每一个角落都可能隐藏着危险。你需要探索环境、收集线索、发现规则，并严格遵守它们——否则，后果自负。

### 核心玩法

- **探索病房**：在第一人称视角下探索病房环境
- **发现规则**：通过观察和解谜找出隐藏的规则
- **生存挑战**：违反规则的代价是致命的
- **物品交互**：使用道具来帮助你在危险中存活

---

## 技术架构

本项目是一个用于练习 Unity 游戏客户端开发的练手 DEMO，重点实现了以下核心系统：

### 1. 3C 控制系统 (Character-Control-Camera)

| 模块 | 实现 | 说明 |
|------|------|------|
| **角色移动** | `PlayerMove.cs` | 基于 Rigidbody 的物理移动，支持行走动画混合 |
| **视角控制** | `FirstPersonalLook.cs` | 第一人称鼠标视角控制 |
| **交互系统** | `PlayerInteraction.cs` | 射线检测 + 状态机驱动的交互逻辑 |

**交互状态机设计**：
```
InteractionIdleState (空闲)
    ↓ 检测到门
DetectingDoorState (可开门)
    ↓ 检测到杯子
DetectingCupState (可拾取)
    ↓ 拾取后
holdingCupState (持杯状态)
```

### 2. 怪物 AI 状态机

**鸟医生 (BirdDor)** - 病房中的主要威胁：

| 状态 | 行为 | 触发条件 |
|------|------|----------|
| **Idle** | 在指定导航点巡逻 | 默认状态 |
| **Check** | 检查可疑声音/区域 | 听到异常声响 |
| **FindPlayer** | 主动追踪玩家 | 发现玩家位置 |

**架构设计**：
- 基类 `Enemy.cs`：提供 NavMeshAgent、Animator 等通用组件管理
- 状态基类 `EnemyState.cs`：定义 Enter/Update/Exit 接口
- 具体状态：继承并实现特定行为逻辑

### 3. 规则驱动引擎 (Rule Engine)

基于 **观察者模式** 实现的核心系统：

```csharp
public enum GameState
{
    isEatMedicine,      // 是否吃药
    isMedicineDestory,  // 药物是否销毁
    isRoomClean,        // 房间是否清洁
    isDoorTouch,        // 门是否被触碰
    isCupRight,         // 水杯位置是否正确
    isThermometerRight  // 温度计位置是否正确
}
```

**核心特性**：
- `StateDetector.cs`：全局状态管理器（单例）
- **订阅/发布机制**：模块可订阅特定状态变化事件
- **解耦设计**：规则逻辑与业务逻辑分离

**使用示例**：
```csharp
// 订阅状态变化
StateDetector.Instance.SubscribeToState(GameState.isDoorTouch, OnDoorTouched);

// 修改状态
StateDetector.Instance.SetState(GameState.isEatMedicine, true);
```

### 4. UI 系统架构

**栈式面板管理** (`UIManager.cs`)：

```
┌─────────────────────────────┐
│  Panel Stack (栈顶显示)      │
├─────────────────────────────┤
│  DiePanel (死亡界面)         │ ← 当前显示
├─────────────────────────────┤
│  PackagePanel (背包)         │ ← 被遮挡，OnDisable
├─────────────────────────────┤
│  SubTitlePanel (字幕)        │
└─────────────────────────────┘
```

**工厂模式** (`PanelFactory.cs`)：
- 通过字符串标识创建对应面板
- 便于扩展新面板类型
- 解耦面板创建逻辑

**面板生命周期**：
- `OnStart()` - 初始化
- `OnEnable()` - 激活（恢复栈顶）
- `OnDisable()` - 失活（被遮挡）
- `OnDestory()` - 销毁

### 5. XLua 集成（预留）

- `xLuaEnv.cs`：Lua 虚拟机环境管理（已搭建基础框架）
- 预留 Lua 脚本加载接口
- 为后续可能的脚本扩展预留架构支持

---

## 项目结构

```
Assets/
├── Scrpits/
│   ├── AI/                    # 敌人 AI 状态机
│   │   ├── BirdDor/           # 鸟医生敌人
│   │   ├── Enemy.cs
│   │   └── EnemyState.cs
│   ├── Detecting/             # 检测系统（杯子/药物/温度计）
│   ├── Interaction/           # 玩家交互系统
│   │   ├── Control/           # 交互控制（门/杯子/IK）
│   │   └── *_State.cs         # 交互状态机实现
│   ├── Item/                  # 物品/背包系统
│   ├── SceneFrame/            # 场景管理框架
│   ├── UI/                    # UI 面板系统
│   └── StateDetector.cs       # 规则驱动引擎
├── Lua/                       # Lua 脚本目录（预留）
├── StreamingAssets/           # 流式资源
└── Resources/                 # UI 预制体资源
```

---

## 技术栈

- **引擎**：Unity 2022+
- **语言**：C# (.NET Standard 2.1)
- **AI 导航**：Unity NavMesh
- **UI 框架**：UGUI + TextMeshPro
- **脚本扩展**：XLua（已导入，基础框架搭建完成）
- **版本控制**：Git

---

## 开发目标

本项目作为个人练手 DEMO，主要练习以下技能：

- 第一人称 3C 控制实现
- 状态机模式在 AI 和交互系统中的应用
- 观察者模式驱动的规则引擎
- 栈结构 UI 管理系统
- Unity 物理、动画、导航系统的综合运用
- XLua 基础框架搭建

---

## 后续规划

- [ ] 添加更多房间和场景
- [ ] 扩展规则系统，支持动态规则生成
- [ ] 完善怪物 AI，增加更多行为状态
- [ ] 添加存档系统
- [ ] 音效和氛围优化
- [ ] XLua 脚本扩展功能落地

---

## 作者

**独立开发者**

> 这是一个用于学习和练习 Unity 游戏开发的个人项目，灵感来源于规则怪谈类作品。

---

*注：本项目为学习用途，部分资源可能来源于 Unity Asset Store 免费资源。*
