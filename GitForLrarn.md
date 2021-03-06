# 学习Git笔记

## 1. 安装Git

    Windows安装比较简单，直接下载软件安装即可。

## 2. 初始化Git

    ### 自报家门
    git config --global user.name "My name
    git config --global user.email "email@example.com"

## 3. 创建版本库

    git init  该步操作直接会将当前文件当做git版本库的根目录

### 3.1添加文件

    第一步：编写一个文件
    readme.md
    第二步：将文件添加到仓库，该文件必须在git初始化过的文件夹中
    git add readme.md将文件添加到仓库
    第三步：提交文件到仓库
    git commit -m "提交说明记录"

## 4. 查看当前仓库状态

    git status 该命令让我们时刻掌握仓库当前的状态。

## 5. 查看文件被修改的内容

    git diff fileName 该命令用于查看本次修改遇上次提交后具体修改的内容。

## 6. 提交修改文件
  
    提交修改与第一次添加内容一样。
    第一步：修改一个文件。
    第二步：将文件添加到仓库中。
    第三步：将文件提交到仓库中。

## 7. 查看提交记录命令

    git reflog  记录每一条命令(主要是版本号)。

## 8. HEAD总结

    HEAD指向的版本就是当前版本，因此，Git允许我们在版本的历史之间穿梭，使用命令git reset --head commit_id即可。
    穿梭前，用git log可查看提交历史，一遍确定要回退到哪个版本。
    要重返未来，用git reflog查看历史，一遍确定要回到未来的哪个版本。

## 9. git的工作区和暂存区

    Git和其他版本控制如SVN的一个不同之处就是有暂存区的概念

## 10. 名词解析

### 10.1 工作区(Working Firectory)

    工作区就是被git初始化过的文件夹，当前就是E:\Study_file_dir\GitofLearn这个文件夹

### 10.2 版本库(Repository)
  
    工作区有一个隐藏目录.git，它不算工作区，而是Git的版本库
    Git的版本库里村里很多东西，其中最重要的就是被称为stage(或者叫index)的暂存区,以及Git自动创建的第一个分支master，一直指向master的一个指针HEAD
    当把文件往Git版本库中添加时，是分两步执行的：
    第一步 用 git add filename 把文件添加进去，实际上就是把文件修改添加到暂存区；
    第二步 用 git commit 提交更改，实际上就是把从暂存区的所有内容提交到当前分支。

## 11. Git命令初步记录

    Git管理的文件分为: 工作区(当前文件夹)，版本库(当前文件夹下的.git文件夹),版本库又分为暂存区和暂存区分支master(仓库)
    工作区>>>>暂存区>>>>>仓库
    git add filename 把filename文件从工作区>>>>暂存区，git commit把文件从暂存区>>>仓库,
    git diff 查看工作区和暂存区差异,
    git diff --cached 查看暂存区和仓库的差异,
    git diff HEAD 查看工作区和仓库的差异,
    git add 的反向命令git checkout, 撤销工作区修改，即把暂存最新行版本转移到工作区
    git commit的反向命令git reset HEAD,就是把仓库罪行版本转移到暂存区。

## 12. Git添加和提交命令避坑

    在提交之前一定要将当前工作区的最新内容进行一次添加 (git add <filename>)，然后再进行提交，保证版本库中的内容与工作区中的内容是一致的。

## 13. 撤销修改

    如果仅仅是修改了文件，没有做添加到暂存区的操作，使用 git checkout -- <filename> 即可将本次文件修改撤销。类似于ctrl+z的操作。
    如果修改了文件有将其添加到暂存区中(git add <filename>),但还没有进行提交(git commit)，仍有办法撤销文件。
    第一步，使用命令git reset HEAD <filename> 把暂存区的修改撤销掉(unstage)，重新将其放到工作区中。此时暂存区就没有最新版本的文件了，可使用git status查看版本库中的文件。
    第二步，使用丢弃工作区的修改命令 git checkout -- <filename>，即可实现将已经添加到暂存区的文件撤销到最后一次提交时的状态。

## 14. 删除文件

    在Git中，删除是一个修改操作。现在新建一个文件test.txt,然后进行添加和提交
    git add test.txt
    git commit -m "add test.txt"
    现在进行了添加和修改，那么如何把这个文件工作区和版本库中删除呢?
    首先，在工作区中删除该文件
    rm test.txt
    此时仅仅只是在工作区中删除了该文件，如果使用git status 可以查看当前暂存区的状态，可了解到git已经记录删除文件的操作。那么怎么在暂存区也删除呢？
    手动删除文件，然后使用git rm <filename> 和git add <filename> 效果是一样的。
    情况1，确实需要从版本库中删除该文件。
    第一步，使用git rm命令删除文件
    git rm test.txt
    第二步，使用git commit进行提交，这个文件就彻底的被删除了。
    git commot -m "remove test.txt"
    情况2，文件被误删,但仅仅只是删除了工作区的文件，实际上版本库中还是有的，所以直接撤销操作就行。使用 git checkout -- test.txt 即可恢复文件
      git checkout 命令其实是用版本库里的版本替换工作区的版本，无论工作区是修改还是删除，都可以"一键还原"。
    注意: 从来没有天骄到版本库的就被删除的文件，是无法恢复的。

## 15. 删除文件小结

    命令git rm 用于删除一个文件。如果一个文件被提交到版本库，那么永远不用担心被误删，但是要注意，只能恢复文件的最新版本，会丢失对吼一次提交后文件被修改的内容。

## 16. Git远程仓库

    如何使用远程仓库(GitHub)
    第一步,创建SSH Key。在用户目录下，看看有没有.ssh目录，如果有再看看这个目录下有没有id_rsa和id_rsa.pub这两个文件。如果没有，打开Shell(Git Bash)，创建SSH Key:
    ssh-keygen -t rsa -C "myemail@example.com"
    第步，登录GitHub，打开"Account settings", "SSH Keys"页面
    然后点 "Add SSH Key"，填上任意Title，在Key文本框里粘贴id_rsa.pub文件的内容
    详情查阅:https://www.liaoxuefeng.com/wiki/896043488029600/896954117292416
    为什么GitHub需要SSH Key呢？
    因为GitHub需要识别出你推送的提交确实是你提交的，而不是别人冒充的，而Git支持SSH协议，所以，GitHub只要知道了公钥，就可以确定只有自己才能进行推送。

## 17. 解决上传文件时一直提示输入账号和密码但一直都不能正常上传(提示密码错误)

    详情查阅https://blog.csdn.net/u013977285/article/details/79726354
    重新添加一个token即可

## 18. 解决上传文件时每次都需要输入账号和密码

    详情查阅https://www.cnblogs.com/sky6862/p/7992736.html

## 19. 解决上传出错问题(没有再该输入yes的地方输入yes)

    问题详情
    The authenticity of host 'github.com (13.250.177.223)' can't be established.
    RSA key fingerprint is SHA256:nThbg6kXUpJWGl7E1IGOCspRomTxdCARLviKw6E5SY8.
    Are you sure you want to continue connecting (yes/no/[fingerprint])?
    Host key verification failed.
    fatal: Could not read from remote repository.
    Please make sure you have the correct access rights
    and the repository exists.
    解决措施 再次提交，并输入yes
    为什么GitHub需要SSH Key呢？
    因为GitHub需要识别出你推送的提交确实是你提交的，而不是别人冒充的，而Git支持SSH协议，所以，
    GitHub只要知道了公钥，就可以确定只有自己才能进行推送。

## 20. 添加远程库
  
    详情查阅https://www.liaoxuefeng.com/wiki/896043488029600/898732864121440
    此处遇到的坑在上面已经做出相应解析
    这里先与本地仓库进行关联，注意使用git模式，而不是https模式，该模式每次上传都需要输入密码。
    使用 git remote add origin <gitadders>
    即可将远端仓库关联到本地git仓库
    使用 git push -u origin master 
    即可将本地所有数据推送到远端库
    把本地库的内容推送到原调，使用git push命令，实际上是把当前分支master推送到远程。
    由于远端库是空的，我们第一次推送master分支时，加上了 -u 参数，Git不但会把本地的master分支内容推送到远程新的master分支和远程的master分支关联起来，在以后的推送或者拉取时就可以简化命令。
    在上面的操作完成后，只要本地做了提交commit，就可以通过命令
    git push origin master
    把本地master分支的最新修改推送至GitHub。至此，一个真正的分布式版本库就有了。

## 21. SSH警告

    该问题就是 上面的解决上传出错问题，详情查阅该段即可。

## 22. 添加远程库小结

    要关联一个远程库,使用命令git remote add origin git@server-name:path/repo-nam.git。
    关联后，使用命令 git push -u origin master第一次推送master分支的所有内容。
    此后，每次本地提交后，只要有必要，就可以使用命令git push origin master 推送最新修改

## 23. 解决提示警告

    Warning: Permanently added the RSA host key for IP address '52.74.223.119' to the list of known hosts.
    详情查阅 https://www.jianshu.com/p/a234f6e40a40
    问题在于需要把本地ip地址添加到hosts中

## 24. 从远程库克隆

    第一步，创建一个新的仓库，名字为gitskills
    第二步，在远端仓库添加README.md文件
    第三步，克隆远端仓库 使用git clone <gitaddress>
    完成克隆

## 25. 如何删除远端仓库

    详情查阅https://blog.csdn.net/weixin_39068791/article/details/80556278

## 26. 解决上传失败问题(SSH不能连接成功)

    使用 git push origin master 出错
    报错为
    ssh: connect to host github.com port 22: Connection timed out
    fatal: Could not read from remote repository.

    Please make sure you have the correct access rights
    and the repository exists.
    解决措施查阅 https://blog.csdn.net/qq_42146613/article/details/82772734

## 27. clone远端仓库小结

    要克隆一个仓库，首先必须知道仓库的地址，然后使用git clone命令进行克隆
    Git支持多种协议，包括https，但ssh协议速度最快，https除了速度慢以外，每次推送都必须要输入口令。但是在某些只开放http端口的公司内部就无法使用ssh协议，而只能用https。

## 28. 分支管理

### 28.1 分支创建

    第一步，创建一个分支  git checkout -b dev  dev 即是被创建的新的分支
    git checkout -b  <name>  表示创建并切换，相当于下面两条命令
    git branch dev     创建dev分支
    git checkout dev   切换到dev分支
    第二步 使用git branch 命令查看当前分支(该步非必要)
    git branch 命令会列出所有分支，当前分支前面会标有一个 * 号
    第三步，进行添加和提交
    git checkout <name> 该命令用于切换分支

### 28.2 分支合并

    现在需要把master 和 dev分支的内容进行合并
    第一步, 把dev分支的工作成果合并到master分支上
    git merge dev
    git merge <name> 命令用于合并指定分支到当前分支。

### 28.3 分支删除

    当确保需要删除的分支中的内容已不再需要或已经合并完成后，即可进行删除
    git branch -d <name> 删除分支

### 28.4 switch

    在切换分支时使用git checkout <branch>，这个命令与撤销修改是同一个命令，实际上这并不和是，所以雀环分支这个动作使用switch更加科学。所以后面的分支切换内容都将使用switch来进行。
    创建新的分支dev
    git switch -c dev 
    切换到主分支
    git switch master

### 28.5 分支管理小结

    查看分支: git branch
    创建分支: git branch <name>
    切换分支: git checkout <name>或者 git switch <name>
    创建+雀环分支: git checkout -b <name> 或者 git switch -c <name>
    合并某分支到当前分支; git merge <name>
    删除分支: git branch -d <name>

### 28.6 解决冲突

    当在两个分支中修改了同一个文件的相同内容，并且都做了添加和提交。
    这时便会出现冲突，需要手动进行选择需要保留的内容。
    选择好之后在进行添加和提交，即可解决文件修改的冲突。
    可使用git log --graph --pretty=oneline --abbrev-commit 这个条命令
    查看分支的合并情况。

### 28.7 分支管理策略

    通常合并分支时，如果可能Git会用Fast forward模式，但这种模式下，删除分支后会丢失分支信息。
    如果要强制禁用Fast forward模式， Git就会在merge时生产一个新的commit，这样分支历史上就可以看出分支信息。
    如何实现这种方法的分支管理策略呢
    * 1 创建并去换dev分支
    git switch -c dev
    * 2 修改或新建文件，并将其添加和提交
    git add readme.md
    git commit -m "add merge"
    * 3 切换分支到master
    git switch master
    * 4 准备合并dev分支，并使用--no--ff参数，表示禁用fast forward
    git merge --no--ff -m "merge wtih no-ff" dev
    本次合并要创建一个新的commit，所以加上-m的参数，把commit描述写进去。

### 28.8 分支策略

    在实际的开发过程中，应该按照几个基本原则进行分支管理：
    首先,master分支应该是非常稳定的，也就是仅用来发布新版本，平时不能再上面干活；
    编写新功能代码都在dev分支上，当某一个功能稳定完全后，即可将dev分支和并到master上，然后master中发布新版本

### 28.9 分支策略小结

    Git分支相当强大。
    合并分支时，加上--no-ff参数就可以用普通模式合并，合并后的历史有分支，能看出来曾经做过合并，而fast forward合并就看不出来曾经做过合并

### 28.10 Bug分支

    在软件开发中，bug就像家常便饭一样。有了bug就需要修复，在git中，由于分支是如此的强大，所以，
    每个bug都可以通过一个新的临时分支来修复，修复后，合并分支，然后将临时分支删除。
    但是当前手上的工作没完成怎么办？也不能提交，因为需要时间去完成。
    但任务的优先级是不能被打破的，只能先修复bug，那么怎么做呢？
    还好，Git还提供了一个stash功能,可以把当前工作现场“储存”起来，等以后恢复现场后继续工作
    git stash       存储并隐藏当前工作区
    在某个分支上修复bug就在某一个分支上创建新的分支，进行修复工作，当修复工作完成后，添加，提交修复后
    的文件，然后切换到需要修复的分支，进行合并，最后删除新创建的分支。此时已完成bug修复。
    那么怎么恢复到刚在的工作呢？
    首先需要切换到刚刚工作的分支。
    git switch dev
    然后查看当前分支的状态(非必须)
    git stash
    此时会发现，这个工作区是干净的，那么刚刚写的东西去哪了呢，此时就需要进行恢复了。
    这里查看一下工作现场
    git stash list
    只要先前有存储分支，应该可以查看到分支提交内容，现在就可以恢复了
    恢复文件有两种方法
    * 1 使用git stash apply 恢复，但是恢复后，stash内容并不会删除,需要使用git stash drop来删除stash中的内容。
    * 2 使用git stash pop，恢复的同时把stash内容也删掉了
    此时在用 git stash list查看，就看不到任何stash内容了
    当多次stash时，可以通过git stash apply stash@{0} 恢复到指定的stash { }的内容就是工作现场编号
    当然这里有一问题，因为bug是出在master分支中，所以dev这个分支极有可能也是存在该bug的，那么快速的修复dev分支中的bug呢？
    因为dev分支和master分支基本上是一样的，存在bug位置都差不多，所以只需要重新进行一次提交在master中修复bug的commit就可以，当然这里只是将提交复制到dev中而不是把整个master分支merge(合并)到dev中。
    这里Git专门提供了一个 cherry-pick命令，该命令可以赋值一个特定的提交到当前分支。

#### 28.11 Bug分支小结

    修复bug时，可以通过创建新的bug分支进行修复，然后合并，最后删除
    当手头的工作没有完成时，先把工作现场git stash（存储并隐藏）一下，然后去修复bug，修复后，在git stash pop，回到工作现场。
    在master分支上修复的bug，想要合并到当前dev分支，可以用 git cherry-pick <commit> 命令，把bug提交的修改"复制"到当前分支，避免重复劳动。

### 28.12 Feature分支

    软件开发中，总有无穷无尽的新的功能要不断添加进来。
    添加一个新功能时，肯定不希望因为一些实验性质的代码，把主分支搞乱，所以，每添加一个新功能，最好新建一个feature分支，在上面开发，完成后，合并，最后，删除该feature分支。
    现在准备开发新功能。
    git switch -c feature-vulcan

    添加一个新文件 vulcan.py
    git add vulcan.py   将文件添加到暂存区
    git commit -m "add feature vulcan" 将文件提交到仓库中
    功能开发完成后，突然，接到上级通知，这个新项目因为某种原因不能继续进行，且不需取消，虽然项目白干了，但是这个属于项目机密的分支必须就地销毁：
    git switch dev      切换到当前工作分支
    git branch -d feature-vulcan  删除新功能分区。
    但这时发现，这个命令执行不成功。
    git友情提醒，该分支还没有被合并，如果删除，将丢失掉修改。
    但如果确实不需要这个分支进行合并，那么就要进行强行删除，需要使用大写的-D参数
    git branch -D feature-vulcan
    这时，这个分支就被删除了。完全没有一丝痕迹

### 28.13 Feature分支小结

    开发一个新需求，最好新建一个分支；
    如果要丢弃一个没有被合并过的分支，可以通过git branch -D <name>强行删除

## 29. 多人协作

    当从远程仓库克隆时，实际上Git自动把本地的master分支和远程的master分支对应起来，并且，远程仓库的默认名称是origin。

    要查看远程库的信息，用git remote
    查看详细信息使用 git remote -v
    使用该命令可以显示可以抓取和推送的origin的地址。如果没有推送权限，就看不到push的地址

### 29.1 推送分支

    推送分支，就是把分支上的本地提交推送到远端库，推送时，要指定本地分支，这样，Git就会把该分支推送到远端库对应的远端分支上：
    git push origin master
    如果要推送其他分支，比如dev，就改成:
    git push origin dev
    但是，并不是一定要把本地分支王远程推送，那么，哪些分支需要推送，哪些不需要呢？
    * master分支是主分支，因此要时刻与远程同步；
    * dev分支是开发分支，团队的所有成员都在上面工作，所以也需要与远程同步；
    * bug分支知识用于本地修复bug，就没有不要推到远程了，除非需要记录bug的修复过程。
    * feature分支是否推到远程取决于是否有其他人需要在该分支上开发。

### 29.2 抓取分支

    多人协作时，大家都会往master和dev分支上推送各自的修改。
    当有一位小伙伴和自己在同一个远端仓库中进行修改添加。
    当小伙伴在origin/dev分支推送了它的提交，但碰巧自己也对同样的文件做了修改并试图进行推送。
    这时候就是可能会出错。
    推送失败，因为小伙伴和我自己的修改推送有冲突。
    这时候先用git pull 把罪行的提交从origin/dev抓取下来，然后在本地合并，解决冲突，再推送。
    git pull
    但是仍然会失败。
    因为没有指定本地dev分支远程origin/dev分支的链接，这时将本地dev分支和远端dev分支进行一次连接
    git branch --set-upsteram-to=origin/dev dev
    再进行pull
    git pull    这时应该会成功
    但是合并时会有冲突，需要手动解决。解决后，进行提交，再次push

    因此，多人协作的工作模式通常是这样的:
    * 首先，可以试图用git push origin <branch-name>推送自己的修改；
    * 如果推送失败，则因为远程分支比本地的更新，需要先用git pull 试图合并；
    * 如果合并有冲突，则解决冲突，并在本地提交；
    * 没有冲突或者解决冲突后，在用 git push origin <branch-name>推送就能成功！
    
    如果git pull提示no tracking information，则说明本地分支和远程分支的连接没有创建，用命令 git branch --set-upstream-to <branch-name> origin/<branch-name>。

### 29.3 多人协作小结

    * 查看远程库信息，使用git remote -v；
    * 本地新建的分支如果不推送到远端，对其他人就是不可见的；
    * 从本地推送分支，使用git push origin branch-name， 如果推送失败，先用git pull抓取远程的新提交；
    * 在本地创建和远程分支对应的分支，使用git checkout -b branch-name origin/branch-name, 本地和远程分支的名称最好一致；
    * 建立本地分支和远程分支的关联，使用git branch --set-upstream branch-name origin/branch-name；
    * 从远程抓取分支，使用git pull，如果有冲突，要先处理冲突。