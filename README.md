# MLAgents-RollerBall
ml-agents是Unity提供的一套开源的深度强化学习工具包，官方提供了大量的范例以供学习。

* [官方网址](https://unity3d.com/cn/machine-learning)
* [github地址](https://github.com/Unity-Technologies/ml-agents)

本工程基于官方文档中作为教学的简单范例RollerBall：训练AI控制一个小球在一个平台上靠近方块所代表的目标点，并避免跌落平台。
AI只能获得小球当前的位置和速度，以及目标点的位置，然后自己决定应该施加何种方向的力，让自己滚动到目标点。

在学习范例的过程中增加了一些扩展实验，以供学习和加深理解之用。

## 开发环境
* Unity 2018.3.9f1 Personal
* Unity ML-Agents Toolkit v0.8
* Python 3.6.8

## ml-agents安装步骤
* 安装Unity 2017.4或更新版本
* 获取ml-agents源码
    `git clone https://github.com/Unity-Technologies/ml-agents.git`
    注意：使用本工程中的范例程序不需要额外下载，相关代码已经包含在工程代码中。
* 安装Pyton3.6以及mlagents包
    `pip3 install mlagents`
    注意：不支持Python3.7和Python3.5

## numpy版本问题
由于在Python中安装ml-agents包时需要依赖numpy，而最新的numpy包（>1.13.0）和ml-agents有兼容问题，在训练时会报如下错误：
Unable to shuffle if the fields are not of same length
解决办法：卸载numpy包，重新安装1.13.0版本的numpy
`pip3 uninstall numpy`
`pip3 install numpy==1.13.0`

## 实验<1>
1. 在Unity中打开Assets/Experiment/Experiment1场景
2. 进入Train/Experiment1文件夹，进行训练
    `cd Train/Experiment1`
    `mlagents-learn config/config.yaml --run-id=RollerBall-1 --train`
    在Unity编辑器中，点击“运行”
3. 查看训练报告
    `tensorboard --logdir=summaries`
    在浏览器中进入[http://localhost:6006](http://localhost:6006)查看报告