学习Git笔记
# 安装Git
    Windows安装比较简单，直接下载软件安装即可。
# 初始化Git
  ## 自报家门
  git config --global user.name "My name"
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


