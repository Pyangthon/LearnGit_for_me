学习Git笔记
# 安装Git
    Windows安装比较简单，直接下载软件安装即可。
# 初始化Git
  ## 自报家门
  git config --global user.name "My name
  git config --global user.email "email@example.com"
# 创建版本库
  git init  该步操作直接会将当前文件当做git版本库的根目录
# 添加文件
  第一步：编写一个文件
  readme.md
  第二步：将文件添加到仓库，该文件必须在git初始化过的文件夹中
  git add readme.md	将文件添加到仓库
  第三步：提交文件到仓库
  git commit -m "提交说明记录"
# 查看当前仓库状态
  git status 该命令让我们时刻掌握仓库当前的状态。
# 查看文件被修改的内容
  git diff fileName 该命令用于查看本次修改遇上次提交后具体修改的内容。
# 提交修改文件
  提交修改与第一次添加内容一样。
  第一步：修改一个文件。
  第二步：将文件添加到仓库中。
  第三步：将文件提交到仓库中。
# 查看提交记录命令
  git reflog  记录每一条命令(主要是版本号)。
# HEAD总结
  HEAD指向的版本就是当前版本，因此，Git允许我们在版本的历史之间穿梭，使用命令git reset --head commit_id即可。
  穿梭前，用git log可查看提交历史，一遍确定要回退到哪个版本。
  要重返未来，用git reflog查看历史，一遍确定要回到未来的哪个版本。
# git的工作区和暂存区
  Git和其他版本控制如SVN的一个不同之处就是有暂存区的概念
  ## 名词解析
  工作区(Working Firectory)
  工作区就是被git初始化过的文件夹，当前就是E:\Study_file_dir\GitofLearn这个文件夹
  版本库(Repository)
  工作区有一个隐藏目录.git，它不算工作区，而是Git的版本库
  Git的版本库里村里很多东西，其中最重要的就是被称为stage(或者叫index)的暂存区,以及Git自动创建的第一个分支master，一直指向master的一个指针HEAD
  当把文件往Git版本库中添加时，是分两步执行的：
  第一步 用 git add filename 把文件添加进去，实际上就是把文件修改添加到暂存区；
  第二步 用 git commit 提交更改，实际上就是把从暂存区的所有内容提交到当前分支。


