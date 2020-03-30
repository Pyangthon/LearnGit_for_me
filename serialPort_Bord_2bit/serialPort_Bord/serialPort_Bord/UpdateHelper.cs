using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace serialPort_Bord
{
    class UpdateHelper
    {
        /// <summary>
        /// 启动升级固件
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="ep"></param>
        public static byte[] getFirmwareUpdate()
        {
            try
            {
                //创建包头
                StructHeader header = new StructHeader();
                //启动固件升级
                header.CMD = GSMCMDEnum.firmwareUpdate;
                //帧长度
                header.LEN = 14;
                //通信地址
                byte[] comAddr = { 0xAA, 0xAA, 0xAA, 0xAA, 0xAA };
                header.COMADDR = comAddr;
                //源类型
                header.SRC = 0xE1;
                //目标类型
                header.DEST = 0xA1;
                //数据域
                byte[] dataBytes = { 0xff };
                //转换包头
                byte[] headerBytes = StructToBytes(header);
                //创建总包bytes
                byte[] sendBytes = new byte[headerBytes.Length + dataBytes.Length + 2 + 1];
                //拷贝Header
                Array.Copy(headerBytes, 0, sendBytes, 0, headerBytes.Length);
                //拷贝xxTea
                Array.Copy(dataBytes, 0, sendBytes, headerBytes.Length, dataBytes.Length);
                //crc
                UInt16 crc = CRC16.GetCRC16(sendBytes, sendBytes.Length - 4);
                // 创建2字节byte数组用于存放校验位
                byte[] Check_Arr = new byte[2];
                // 获取校验位高8位         
                Check_Arr[0] = Convert.ToByte(crc >> 8);
                // 获取校验位低8位   
                Check_Arr[1] = Convert.ToByte(crc & 0xFF);   
                //拷贝crc
                Array.Copy(Check_Arr, 0, sendBytes, sendBytes.Length - 4, 2);
                //包尾
                sendBytes[sendBytes.Length - 1] = 0x16;
                //返回数据
                return sendBytes;
            }
            catch (Exception er)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取固件
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="ep"></param>
        public static void getFirmwarePag(byte[] reportBuffer)
        {
            try
            {
                //转读固件包结构
                ReadUpdateP readPag = (ReadUpdateP)BytesToStruct(reportBuffer, typeof(ReadUpdateP));
                //获取Index
                int index = readPag.index;
                //取升级固件包
                Dstruct dataSt = Form1.DataPacks[index];
                byte[] data = StructToBytes(dataSt);
                //升级包数据域
                Updatestruct upStruct = new Updatestruct();

                //创建包头
                StructHeader header = (StructHeader)BytesToStruct(reportBuffer, typeof(StructHeader));
                header.CMD = GSMCMDEnum.ackFirmware;
                header.LEN = 65;
                //升级包编号
                upStruct.index = (byte)index;
                //固件总长度
                UInt16 fileLength = (UInt16)Param.fileLenth;
                //转换大端
                fileLength = BytesArrayHelper.SwapUInt16(fileLength);
                //设置总长度
                upStruct.fileLength = fileLength;
                //升级包
                upStruct.dPack = data;
                //数据域
                byte[] dataBytes = new byte[260];
                //active data
                byte[] acts = structHelper.StructToBytes(upStruct);
                //拷贝acts
                Array.Copy(acts, 0, dataBytes, 0, acts.Length);
                //转换加密表号大端
                // MMID = BytesArrayHelper.SwapUInt32(MMID);
                //转bytes
                byte[] mmidBytes = header.MID;
                //xxtea的字节数组
                byte[] xxTeaBytes = new byte[dataBytes.Length + 4];
                //拷贝DataBytes
                Array.Copy(dataBytes, 0, xxTeaBytes, 0, dataBytes.Length);
                //拷贝mmid
                Array.Copy(mmidBytes, 0, xxTeaBytes, dataBytes.Length, mmidBytes.Length);
                //加密
                // xxTeaBytes = XxTeaUtil.encryptBuff(xxTeaBytes, (ushort)xxTeaBytes.Length, new byte[20]);
                //转换包头
                byte[] headerBytes = structHelper.StructToBytes(header);
                //创建总包bytes
                byte[] sendBytes = new byte[headerBytes.Length + xxTeaBytes.Length + 2 + 1 + 1];
                //拷贝Header
                Array.Copy(headerBytes, 0, sendBytes, 0, headerBytes.Length);
                //拷贝xxTea
                Array.Copy(xxTeaBytes, 0, sendBytes, headerBytes.Length, xxTeaBytes.Length);
                //crc
                UInt16 crc = Crc16Util.CalcCrc16(sendBytes, sendBytes.Length - 4);
                //大端转换
                //crc = BytesArrayHelper.SwapUInt16(crc);
                //拷贝crc
                Array.Copy(structHelper.StructToBytes(crc), 0, sendBytes, sendBytes.Length - 4, 2);
                //求和校验
                byte check = Protocol2.Structs.GetGsmCheck(sendBytes, sendBytes.Length - 2);
                //赋值
                sendBytes[sendBytes.Length - 2] = check;
                //包尾
                sendBytes[sendBytes.Length - 1] = 0x16;
                //发送数据
                socket.SendTo(sendBytes, ep);
                string res = BytesArrayHelper.ToHexString(sendBytes);
                log.WriteLog("SEND:" + res);
            }
            catch (Exception er)
            {
                //若发送报错则关闭套接字
                socket.Close();
            }
        }


        /// <summary>
        /// byte[]转换为struct  
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="strcutType"></param>
        /// <returns></returns>
        public static object BytesToStruct(byte[] bytes, Type strcutType)
        {
            int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// byte[]转换为struct  
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="strcutType"></param>
        /// <returns></returns>
        public static object BytesToStruct(byte[] bytes, int index, Type strcutType)
        {
            int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, index, buffer, size);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        //struct转换为  byte[]
        public static byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

    }
}
