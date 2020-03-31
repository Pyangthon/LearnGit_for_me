using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace serialPort_Bord
{
    //以下是命令域CMD中需要用到的命令，服务器下行的命令
    class GSMCMDEnum
    {
        //以下是命令域CMD中需要用到的命令，服务器下行的命令
        public const byte firmwareUpdate = 0xF4;            //启动固件更新
        public const byte readFirmware = 0xF5;              //表头读固件
        public const byte ackFirmware = 0xF6;               //返回固件包
        public const byte updateDone = 0xF7;                //升级完成
    }
}
