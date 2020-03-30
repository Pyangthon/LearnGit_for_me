using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace serialPort_Bord
{ 
    /// <summary>
    /// 链路数据帧包头
    /// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct StructHeader
    {
        public byte HEAD;                                       //数据帧头1
        public byte LEN;                                        //数据域长度
        public byte SRC;                                        //源类型
        public byte DEST;                                       //目标类型
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]    //5字节0xaa
        public byte[] COMADDR;                                  //通信地址
        public byte CMD;                                        //指令码
    }

    /// <summary>
    /// 唐山上报应答数据
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct StructTsAckData
    {
        public UInt32 time;            //表号
        public byte activeSeconds;     //保持连接数
    }

    //升级固件包结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Dstruct      //分包
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]  //开辟256个字节长度符型数组 
        public byte[] dPack;   //数组名
    }

    //升级固件包数据域结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Updatestruct      //分包
    {
        //升级包编号
       public byte index;
        //升级包编号非
        public byte noIndex;
        //固件总长度
        public UInt16 fileLength;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]  //开辟256个字节长度符型数组 
        public byte[] dPack;   //数组名
    }

    //升级固件包数据域结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ReadUpdateP      //读固件包
    {
        //包头
        StructHeader header;
        //升级包编号
        public byte index;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct StructGsmACKData
    {
        public StructGsmHeader gsmHeader;        //包头信息
        public byte ACK_DATA;                    //应答数据
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct structGsmReportData
    {
        public StructGsmHeader gsmHeader;          //包头信息
        public UInt32 UsedGasVolume;               //用气量--传入的数是扩大了100倍的---需要UsedGasVolume/100 得到真正的数
        public UInt32 ImpluseNum;                  //脉冲数
        public UInt32 TotalPurchaseAmount;         //总购金额--传入的数是扩大了100倍的---需要TotalPurchaseAmount/100 得到真正的数
        public UInt32 WTotalPurchaseAmount;        //网充总购金额--传入的数是扩大了100倍的---需要WTotalPurchaseAmount/100 得到真正的数
        public Int32 ResidualAmount;              //剩余金额--传入的数是扩大了100倍的---需要ResidualAmount/100 得到真正的数
        //public UInt32 WResidualAmount;           //网充剩余金额--传入的数是扩大了100倍的---需要WResidualAmount/100 得到真正的数
        public UInt32 CurrentPrice;                //当前价格--传入的数是扩大了10000倍的---需要CurrentPrice/10000 得到真正的数
        public UInt32 LastPurchaseAmount;          //上次购气金额--传入的数是扩大了100倍的---需要LastPurchaseAmount/100 得到真正的数
        public UInt16 PurchaseTimes;               //购气次数
        public UInt16 PriceVersion;                //价格版本
        public UInt16 ErrorStatus;                 //错误状态
        public UInt16 CellVoltage;                 //电池电压--传入的数是扩大了100倍的---需要CellVoltage/100 得到真正的数
        public byte SignalIntensity;               //无线信号强度
        public Int32 Temperature;                 //温度
        public UInt32 Pressure;                    //压力
        public byte ValvStatus;                    //阀门状态
    }

    /// <summary>
    /// 唐山上报数据帧格式
    /// </summary>
   [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct StructTsReportData
    {
        public UInt16 comVersion;                       //通信版本
        public UInt16 programVersion;                  //程序版本
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string iccid;                            //ICCID
        public byte digits;                         //小数位数
        public byte reportReason;                   // 上报原因
        public UInt16 standby;                      //备用
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string apn;                            //APN
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string ip;                            //IP地址
        public UInt16 port;                             //端口号
        public byte comFailureDays;                         //连续通信失败天数
        public byte noGas;                         //连续未用气关阀天数
        public byte valveControll;                         //阀门控制参数
        public byte meterStatus;                         //表用户状态
        public UInt16 largeFlow;               //超大切断流量
        public UInt16 smallFlow;               //超小切断流量
        public byte monthly;                //月用量统计日
        public byte intervalHours;          //通信间隔小时数
        public byte startTime;               //开始传输的时间
        public byte transRange;                //传输区间时长
        public UInt16 offsetTime;                 //传输偏移时间
        public byte allowTime;                //超大流量后允许开阀时间
        public byte triesLimit;                 //日按键上报限制次数
        public byte largeFlowTime;                 //超大流量切断持续时间
        public UInt16 smallFlowTime;                 //超小流量切断持续时间
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string spare;                             //备用
        public UInt32 errCode ;               //表端异常代码记录
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string errTime;                             //异常时间记录
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string valveReason;                             //备用
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string actionTime;                             //阀门动作记录时间
        public UInt32 standardTotal;               //标况累计用气量
        public UInt16 capacitorVoltage ;                 //电容电压
        public UInt16 batteryVoltage;                 //电池电压
        public UInt32 meterTime;                    //表内时间
        public byte signal;                    //Gsm信号
        public byte meterInStatus;                    //表内状态
        public UInt32 workingTotal;                    //工况累计用气量
        public UInt16 minimumTemperature;                    //最低温度
        public UInt32 statisticalTime;                    //统计时间
        public UInt32 statisticalStandard;                    //统计标况累计用气量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string monthStandard;                             //标况月用量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string monthWorking;                             //工况月用量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
        public string monthTemperature;                             //月记录温度值
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string dayStandard;                             //日标况用量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string dayWorking;                             //日工况用量
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string dayTemperature;                             //日记录温度值
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 96)]
        public string hourStandard;                             //标况小时用量
        public UInt32 mmid;                    //加密表号

    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    class GsmDataDetail
    {
        public float UsedGasVolume;                //用气量
        public uint ImpluseNum;                    //脉冲累计数
        public float TotalPurchaseAmount;          //总购金额
        public float ResidualAmount;               //剩余金额
        public float CurrentPrice;                 //当前价格
        public float LastPurchaseAmount;           //上次购气金额
        public ushort PurchaseTimes;               //购气次数
        public ushort PriceVersion;                //价格版本
        public float CellVoltage;                  //电池电压
        public byte SignalIntensity;               //信号强度
        public uint Temperature;                   //温度
        public uint Pressure;                      //压力
        public string ValvStatus;                  //阀门状态
        public string ErrorStatus;                 //故障情况
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    class GsmMeterErrorCode
    {
        public const ushort Error_Sample0_E = 0x0008;          //采样0故障
        public const ushort Error_Sample1_E = 0x0010;          //采样1故障
        public const ushort Error_PowerDown_E = 0x0020;         //外部掉电
        public const ushort Error_SmallFlow_E = 0x0200;        //超小流量
        public const ushort Error_FarCloseValve_E = 0x0400;      //远程关阀
        public const ushort Error_BatteryLow_E = 0x0004;          //电池电量低
        public const ushort Error_MeterStorer_E = 0x0002;          //内部存储故障
        public const ushort Error_Valve_E = 0x0001;               //阀门故障
        public const ushort Error_MagneticInterferon_E = 0x0080;  //防磁故障
       // public const ushort Error_ClosedCust = 0x0200;           //用户未开通
       // public const ushort Error_OutOfMoney = 0x0400;           //用户欠费
        public const ushort Error_ValveZT_E = 0x0040;         //阀门直通
        public const ushort Error_ExceedFlow_E = 0x0100;         //超大流量
        public const ushort Error_SurplusEmpty_E = 0x0800;              //余量为空

    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct StructGasVolume
    {
        public byte PaymentType;//付费类型
        public uint UsedGasVolume;//用气量
        public uint ImpulseNum;//总脉冲数
        public uint TotalPurchaseAmount;//总购金额
        public int ResidualAmount;//剩余金额
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct AddressDetail
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string port;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string serverIP;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct ServerAddress
    {
        public AddressDetail server1;
        public AddressDetail server2;
        public AddressDetail server3;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct StructGsmSettlementData
    {
        public byte SettlementPeriod;//结算周期
        public byte EntryTimeYear;//生效时间（年）
        public byte EntryTimeMonrh;//生效时间（月）
        public byte EntryTimeDay;//生效时间（日）
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct StructGsmRecharge
    {
        public byte ladder;//是否带阶梯
        public ushort PurchaseTimes;//购气次数
        public uint payMoney;//支付的金额
        public uint price;//平均计价当前的单价
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    struct StructLoginProduction
    {
        public StructGsmHeader gsmHeader;          //包头信息
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] iccid;            //
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] imei;             //
        public byte rssi;               //
        public byte moduleType;               //
    }




    //GPRS协议结构体
    class Structs
    {
        /// <summary>
        /// 求传入的byte数组的和校验码，返回校验码
        /// </summary>
        /// <param name="bytes">byte数组</param>
        /// <param name="length">数组长度</param>
        /// <returns></returns>
        public static byte GetGsmCheck(byte[] bytes, int length)
        {
            byte Num = 0;
            byte checkNum = 0;
            for (int i = 0; i < length; i++)
            {
                Num += bytes[i];
            }
            checkNum = byte.Parse((Num & 0xff).ToString());
            return checkNum;
        }
        /// <summary>
        /// ushort字节序转换
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static ushort SwapUInt16(ushort n)
        {
            return (ushort)(((n & 0xff) << 8) | ((n >> 8) & 0xff));
        }

        /// <summary>
        /// Uint32字节序转换
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static uint SwapUInt32(uint n)
        {
            return (uint)(((SwapUInt16((ushort)n) & 0xffff) << 0x10) |
                           (SwapUInt16((ushort)(n >> 0x10)) & 0xffff));
        }

        /// <summary>
        /// int32字节序转换
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SwapInt32(int n)
        {
            return (int)(((SwapUInt16((ushort)n) & 0xffff) << 0x10) |
                           (SwapUInt16((ushort)(n >> 0x10)) & 0xffff));
        }
       
    }
}
