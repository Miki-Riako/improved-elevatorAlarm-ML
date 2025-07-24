# improved-elevatorAlarm-ML
an improved elevatorAlarm method in ML based on openCV

# 基于混合计算机视觉与机器学习的电梯困人智能告警系统
> An Intelligent Elevator Entrapment Alarm System Based on Hybrid Computer Vision and Machine Learning

![.NET Framework](https://img.shields.io/badge/.NET-Framework%204.7.2-blue)
![OpenCV](https://img.shields.io/badge/OpenCV-OpenCvSharp-5C3EE8?logo=opencv&logoColor=white)
![Tesseract](https://img.shields.io/badge/OCR-Tesseract-blueviolet)
![Baidu AI](https://img.shields.io/badge/AI%20API-Baidu-2932E1?logo=baidu&logoColor=white)

---

## 1. 项目概述 (Project Overview)

本项目旨在设计并实现一个**低成本、高效率的电梯困人智能告警系统**。系统以标准的电梯内监控视频作为输入数据源，利用计算机视觉和机器学习技术，实时分析电梯的**门状态**、**轿厢内人数**、**楼层显示**及**运行状态**，并根据一套智能决策逻辑，自动判断是否发生“人员被困”的异常事件，从而实现主动告警。

项目的核心创新在于提出了一套**混合模型框架**，它有机地结合了：
- **传统CV算法**：用于快速、高效地处理门状态等具有明显特征的事件。
- **机器学习方法（K-Means）**：显著提升了在复杂光照和低质量视频中对电梯楼层显示的识别准确率。
- **外部AI服务（百度AI）**：作为高精度的人数统计模块，用于关键时刻的确认。

整个系统在 C#/.NET 平台下采用多线程并行处理，确保了分析的实时性和效率。

## 2. 问题定义 (Problem Statement)

电梯作为现代建筑中不可或缺的垂直交通工具，其安全性至关重要。传统的电梯故障依赖于乘客手动按铃求救或定期的人工巡检，存在以下痛点：
- **被动响应**：无法在第一时间主动发现困人事件，尤其是在乘客无法求救（如恐慌、无行为能力）的情况下。
- **巡检成本高**：需要大量人力进行视频监控和物理巡检，效率低下。
- **数据未被利用**：海量的存量监控视频数据未能被有效利用于安全预警。

本项目旨在解决以上问题，将传统的被动安防升级为**主动智能预警**。

## 3. 系统核心功能 (Core Features)

- **🚪 门状态实时监控**：准确识别电梯门的**开启**、**关闭**和**静止**状态。
- **👨‍👩‍👧‍👦 轿厢载人状态检测**：通过背景建模和云端AI，判断轿厢内是否有人。
- **🔢 楼层与运行方向识别**：
    - **OCR与数码管识别**：兼容新式LCD/LED显示屏和老式数码管（Nixie Tube）的楼层数字。
    - **K-Means增强**：利用K-Means聚类进行颜色分割，作为识别前的预处理步骤，大幅提升了识别的鲁棒性。
    - **箭头识别**：判断电梯的上下行方向。
- **🚨 智能困人告警逻辑**：融合多模块信息，当满足 **“有人” + “门长时间关闭” + “楼层长时间无变化”** 的条件时，触发“疑似困人”告警。
- **🔄 动态背景自适应建模**：系统能够定时在电梯空载、静止时自动更新背景模型，适应轿厢内部环境的渐变（如光线变化、新增污渍等）。

## 4. 技术架构与方法 (Architecture & Methodology)

系统处理流程遵循“数据层面 -> 方法层面 -> 分析层面”的完整链路。

### 4.1 数据层面 (Data Layer)
- **数据源**：标准的电梯监控视频（`.mp4`格式）。
- **数据预处理**：
    - **兴趣区域 (ROI) 裁剪**：代码中通过硬编码坐标（`Rect`）的方式，将视频帧分割为“门区域”、“显示屏区域”和“轿厢主体区域”，送入不同模块处理，减少计算量。
    - **帧率控制**：采用隔帧处理的策略，在不影响关键信息捕捉的前提下，提升处理效率。

### 4.2 方法层面 (Method Layer)

这是本项目的核心，各个模块并行工作：

#### 模块一：门状态检测 (ObjectTracking)
- **技术**：传统计算机视觉。
- **流程**：
    1. 提取ROI（门缝区域）。
    2. 使用 **Sobel算子** 进行垂直边缘检测，增强门缝的轮廓。
    3. **二值化**处理，将门缝线条突出显示。
    4. **形态学操作**（腐蚀、膨胀）去除噪点，连接断裂的线条。
    5. **轮廓检测 (`FindContours`)**，寻找最能代表门缝的垂直轮廓线。
    6. 通过比较连续帧之间轮廓线的位置变化，判断门的“开”、“关”或“静止”状态，并进行帧累积以增加判断的稳定性。
- **对应代码**：`ObjectTracking2.cs`, `ObjectTracking_OnlyDoorStatus.cs`

#### 模块二：轿厢载人检测 (Person Detection)
- **技术**：背景减除法 + 外部AI API（混合方法）。
- **流程**：
    1. **背景建模 (`CannyBackgroundModeling2`)**：
        - 在电梯空载时，截取多帧画面。
        - 对每帧进行**Canny边缘检测**，并将所有帧的边缘点集合成一个“背景模型”（`HashSet<Coordinate>`）。这个模型代表了空电梯的静态轮廓。
    2. **前景检测 (`CannyBackgroundDetection2`)**：
        - 对新视频帧进行同样的Canny边缘检测。
        - 将新帧的边缘点与“背景模型”作差集，得到的即为前景（如乘客）的边缘。
        - 通过计算前景边缘点的数量或轮廓长度，判断是否有人。
    3. **高精度确认 (`BaiduAl`)**:
        - 在触发疑似困人告警的关键时刻，将当前视频帧上传至**百度AI人体分析API**，获取精确的人数统计，作为最终确认，避免误报。
- **对应代码**：`CannyBackground.cs`, `BaiduAl.cs`

#### 模块三：楼层显示识别 (Display Recognition) - **【机器学习核心创新点】**
- **技术**：传统图像处理、K-Means聚类、OCR。
- **流程**：
    1. 提取ROI（电梯楼层显示屏区域）。
    2. **ML增强预处理 - K-Means聚类 (`Kmeans.cs`)**：
        - 将ROI区域的图像像素作为样本点，应用 **K-Means算法 (k=2)** 进行颜色聚类。
        - 此步骤能有效地将发光的数字/符号（一类颜色）与背景（另一类颜色）完美分离，生成一张清晰的黑白二值图像。相比传统的阈值分割，K-Means对光照变化和背景干扰有更强的鲁棒性。
    3. **字符识别**：
        - **方法A - OCR (`TestOcr2_kmeans.cs`)**: 将K-Means处理后的清晰图像送入 **Tesseract OCR引擎** 进行文字识别。适用于LED/LCD等点阵显示屏。
        - **方法B - 数码管识别 (`NixieTubeIdentification_kmeans.cs`)**: 对于老式七段数码管，采用**穿线法 (Threading Method)**，通过在特定位置采样像素点来判断哪几段被点亮，从而解析出数字。
    4. **箭头识别**：通过分析上下/左右区域的亮像素点数量差异，判断电梯运行方向。
- **对应代码**：`Kmeans.cs`, `TestOcr2_kmeans.cs`, `NixieTubeIdentification_kmeans.cs`

### 4.3 分析与验证层面 (Analysis & Validation)

- **告警决策逻辑 (`ElevatorAlarmNix.cs`, etc.)**：
    - 这是一个基于时间序列状态的决策系统。
    - 系统维护了多个状态计数器：`门关闭持续帧数`、`载人状态持续帧数`、`楼层不变持续帧数`。
    - **告警规则**：当这三个计数器同时超过预设阈值（例如：有人状态持续10帧，门关闭持续500帧，楼层不变持续500帧），系统判定为“疑似困人”事件。
- **多线程架构**：
    - 利用 C# 的 `ThreadPool`，将“门状态”、“载人”和“楼层识别”三个计算密集型任务分发到不同线程并行处理，最大化利用CPU资源，保证了系统的准实时性。
- **参数化与验证**：
    - 系统中的关键阈值（如Canny阈值、轮廓面积、告警时间等）均以参数形式提供，便于针对不同场景的电梯视频进行调试和验证，体现了良好的工程实践。

## 5. 项目文件结构 (Project Structure)

```
elevatorAlarm/
├── elevatorAlarm.sln                # Visual Studio 解决方案文件
├── elevatorAlarm/
│   ├── elevatorAlarm.csproj           # C# 项目文件
│   ├── packages.config              # NuGet 包依赖
│   ├── App.config                   # 应用程序配置文件
│   ├── Kmeans.cs                    # 【ML核心】K-Means聚类算法实现
│   ├── CannyBackground.cs           # 背景建模与前景（人）检测
│   ├── ObjectTracking2.cs           # 门状态检测
│   ├── TestOcr2_kmeans.cs           # 【ML应用】结合K-Means和Tesseract OCR的楼层识别
│   ├── NixieTubeIdentification.cs   # 基于传统方法的数码管识别
│   ├── BaiduAl.cs                   # 百度AI接口封装
│   ├── ElevatorAlarmNix.cs          # 主告警逻辑（数码管版本）
│   ├── ElevatorAlarmOcr.cs          # 主告警逻辑（OCR版本）
│   └── ... (其他版本的实现和测试类)
└── tessdata/                        # Tesseract OCR 语言数据
```

## 6. 如何运行 (Setup & Usage)

### 6.1 环境要求
- **IDE**: Visual Studio 2017 或更高版本
- **框架**: .NET Framework 4.7.2
- **依赖**: OpenCvSharp, Tesseract, Baidu.AI SDK (Newtonsoft.Json)

### 6.2 安装步骤
1. **克隆仓库**
   ```bash
   git clone https://github.com/Miki-Riako/improved-elevatorAlarm-ML.git
   ```
2. **打开项目**
   - 使用 Visual Studio 打开 `elevatorAlarm.sln` 解决方案。
3. **还原NuGet包**
   - 在Visual Studio中，右键点击解决方案 -> “还原NuGet程序包”。VS会自动下载所有必要的依赖项。
4. **配置视频路径**
   - 本项目中的视频文件路径是硬编码在代码中的。请打开其中一个主程序文件（如 `ElevatorAlarmNix_Frame_Kmeans711.cs` 的 `Main` 函数），修改 `filename1` (建模视频) 和 `filenameAlarm` (测试视频) 的路径为你本地的视频文件路径。
5. **配置百度AI (可选)**
   - 如果需要使用人数统计功能，请在 `BaiduAl.cs` 文件中或在调用处填入你自己的百度AI应用的 `API_KEY` 和 `SECRET_KEY`。

### 6.3 运行项目
- 在Visual Studio中，设置一个主程序类（如`ElevatorAlarmOcr_Frame_Kmeans711`）为启动对象。
- 点击“启动”或按 `F5` 运行。
- 程序将开始处理视频，并在控制台输出实时的检测状态信息。

## 8. 未来工作 (Future Work)

- **模型优化**：使用更先进的本地化目标检测模型（如YOLOv5/v8）替代背景减除法和外部API，以实现更高精度和更低延迟的本地化部署。
- **GUI开发**：开发一个简单的图形用户界面，用于更直观地展示实时状态和告警信息。
- **自动化ROI**：研究自动定位电梯门和显示屏区域的算法，以适应不同型号和安装位置的电梯，提高系统的泛化能力。
- **代码重构**：将多个功能相似的类（如不同版本的告警逻辑）重构为更通用的、可通过配置切换的模式，提升代码的可维护性。

---
