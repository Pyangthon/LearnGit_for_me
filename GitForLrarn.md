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


