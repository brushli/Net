﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using MessageLib;
using QueueMessage;

namespace RateService
{
    public class Service : WebSocketServer, IServiceUI
    {
        JavaScriptSerializer convert = new JavaScriptSerializer();
        RateProcess rateProcess = new RateProcess();
        List<string> loginOperation = new List<string>();
        byte[] btDisConn = new byte[] { 0x3, 0xe9 };
        internal Extra<IntPtr, DeviceInfo> deviceList = new Extra<IntPtr, DeviceInfo>();
        ServiceClient client = new ServiceClient();
        OperateIni ini;

        public Service()
        {
            ini = new OperateIni(AppDomain.CurrentDomain.BaseDirectory + "RateUpdate.ini");
            this.client.ReceiveMessage += new Action<IntPtr, Message>(client_ReceiveMessage);
            this.OnPrepareListen += new TcpServerEvent.OnPrepareListenEventHandler(Service_OnPrepareListen);
            this.OnClose += new TcpServerEvent.OnCloseEventHandler(Service_OnClose);
            this.OnWSMessageHeader += new WebSocketEvent.OnWSMessageHeaderEventHandler(Service_OnWSMessageHeader);
            this.OnWSMessageBody += new WebSocketEvent.OnWSMessageBodyEventHandler(Service_OnWSMessageBody);
            loginOperation.AddRange("raterequest,rateoperate,ratesubmit".Split(','));
        }

        HandleResult Service_OnPrepareListen(TcpServer sender, IntPtr soListen)
        {
            dynamic section = ConfigurationManager.GetSection("ServiceConfig");
            var port = section.Configs["排队消息服务"].Port;
            this.client.Connect("127.0.0.1", ushort.Parse(port));
            return HandleResult.Ignore;
        }

        void client_ReceiveMessage(IntPtr connId, Message message)
        {
            switch (message.GetType().Name)
            {
                case MessageName.ClientQueryMessage:
                    #region clientQuery
                    {
                        var msg = message as ClientQueryMessage;
                        var allClient = this.deviceList.Dictionary.Values.ToArray();
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        foreach (var client in allClient)
                        {
                            dic[client.WindowNumber] = client.UserID;
                        }
                        msg.QueryType = ClientQueryType.Response;
                        msg.ClientList = dic;
                        this.client.SendMessage(msg);
                    }
                    #endregion
                    break;
                case MessageName.RateMessage:
                    #region rateRequest
                    {
                        var msg = message as RateMessage;
                        var allClient = this.deviceList.Dictionary.Values.ToArray();
                        var listRate = allClient.Where(p => p.WindowNumber == msg.WindowNo);
                        foreach (var client in listRate)
                        {
                            var rateData = new RequestData
                            {
                                method = "RateQuest",
                                param = new
                                {
                                    rateId = msg.RateId,
                                    transactor = msg.Transactor,
                                    item = msg.ItemName,
                                    date = msg.WorkDate,
                                    reserveSeq = msg.reserveSeq
                                }
                            };
                            this.SendWSMessage(client.ID, rateData.ToResultData());
                        }
                    }
                    #endregion
                    break;
                case MessageName.OperateMessage:
                    #region operate
                    {
                        var msg = message as OperateMessage;
                        var allClient = this.deviceList.Dictionary.Values.ToArray();
                        var listRate = allClient.Where(p => p.WindowNumber == msg.WindowNo);
                        foreach (var client in listRate)
                        {
                            var rateData = new RequestData
                            {
                                method = "RateOperate",
                                param = new
                                {
                                    operate = msg.Operate.ToString()
                                }
                            };
                            this.SendWSMessage(client.ID, rateData.ToResultData());
                        }
                    }
                    #endregion
                    break;
            }
        }

        HandleResult Service_OnClose(TcpServer sender, IntPtr connId, SocketOperation enOperation, int errorCode)
        {
            if (!this.deviceList.Dictionary.ContainsKey(connId))
                return HandleResult.Ignore;
            var deviceInfo = this.deviceList.Dictionary[connId];
            this.client.SendMessage(new ClientChangedMessage()
            {
                ChangedType = ClientChangedType.Remove,
                UserID = deviceInfo.UserID,
                WindowNumber = deviceInfo.WindowNumber
            });
            this.deviceList.Remove(connId);
            this.deviceList.Changed = true;
            return HandleResult.Ignore;
        }

        HandleResult Service_OnWSMessageHeader(IntPtr connId, bool final, byte reserved, byte operationCode, byte[] mask, ulong bodyLength)
        {
            if ((WSOpcode)operationCode == WSOpcode.Close)
                this.Disconnect(connId);
            return HandleResult.Ignore;
        }

        HandleResult Service_OnWSMessageBody(IntPtr connId, byte[] data)
        {
            if (BitConverter.ToString(data) == BitConverter.ToString(btDisConn))
                return HandleResult.Ignore;
            var strData = Encoding.UTF8.GetString(data);
            RequestData requestData = null;
            try
            {
                requestData = convert.Deserialize<RequestData>(strData);
            }
            catch
            {
                return HandleResult.Ignore;
            }
            var method = requestData.method.Trim().ToLower();
            var device = this.deviceList.Get(connId);
            if (loginOperation.Contains(method) && string.IsNullOrEmpty(device == null ? "" : device.UserCode))
            {
                var rData = new ResponseData { code = "1000", request = requestData.method, result = "当前用户尚未登录" };
                this.SendWSMessage(connId, rData.ToResultData());
                return HandleResult.Ignore;
            }
            switch (method)
            {
                #region debug
                case "raterequest":
                    if (device.UserCode == "QueueService")
                    {
                        var param = requestData.param as Dictionary<string, object>;
                        var targetList = this.deviceList.Dictionary.Values.Where(p => p.WindowNumber == param["winNum"].ToString()).ToList();
                        foreach (var target in targetList)
                        {
                            var rateData = new RequestData
                            {
                                method = "RateQuest",
                                param = new
                                {
                                    rateId = param["rateId"] == null ? "" : param["rateId"].ToString(),
                                    transactor = param["transactor"] == null ? "" : param["transactor"].ToString(),
                                    item = param["item"] == null ? "" : param["item"].ToString(),
                                    date = param["date"] == null ? "" : param["date"].ToString(),
                                    reserveSeq = param["reserveSeq"] == null ? "" : param["reserveSeq"].ToString()
                                }
                            };
                            this.SendWSMessage(target.ID, rateData.ToResultData());
                        }
                        var rData = new ResponseData { code = "0", request = requestData.method, result = targetList.Count == 0 ? "无此用户信息" : "已发送评价请求" };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    else
                    {
                        var rData = new ResponseData { code = "1001", request = requestData.method, result = "该用户无此操作权限" };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                case "rateoperate":
                    if (device.UserCode == "QueueService")
                    {
                        var param = requestData.param as Dictionary<string, object>;
                        var targetList = this.deviceList.Dictionary.Values.Where(p => p.WindowNumber == param["winNum"].ToString()).ToList();
                        foreach (var target in targetList)
                        {
                            var rateData = new RequestData
                            {
                                method = "RateOperate",
                                param = new
                                {
                                    operate = param["operate"].ToString()
                                }
                            };
                            this.SendWSMessage(target.ID, rateData.ToResultData());
                        }
                        var rData = new ResponseData { code = "0", request = requestData.method, result = targetList.Count == 0 ? "无此用户信息" : "已发送操作请求" };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    else
                    {
                        var rData = new ResponseData { code = "1001", request = requestData.method, result = "该用户无此操作权限" };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                #endregion
                case "getwindowlist":
                    {
                        var param = requestData.param as Dictionary<string, object>;
                        var rData = new ResponseData { code = "0", request = requestData.method, result = this.rateProcess.RS_GetWindowList() };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                case "getuserlist":
                    {
                        var param = requestData.param as Dictionary<string, object>;
                        var rData = new ResponseData { code = "0", request = requestData.method, result = this.rateProcess.RS_GetUserListByWindowNo(param["winNum"].ToString()) };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                case "getitemlist"://根据窗口获取事项列表
                    {
                        var param = requestData.param as Dictionary<string, object>;
                        var rData = new ResponseData { code = "0", request = requestData.method, result = this.rateProcess.RS_GetItemListByWindowNo(param["winNum"].ToString()) };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                case "getupdateinfo":
                    {
                        var rData = new ResponseData
                        {
                            code = "0",
                            request = requestData.method,
                            result = new
                            {
                                version = ini.ReadString("Config", "Version"),
                                url = ini.ReadString("Config", "Url")
                            }
                        };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                case "getuserphoto":
                    {
                        var param = requestData.param as Dictionary<string, object>;
                        var rData = new ResponseData { code = "0", request = requestData.method, result = this.rateProcess.GetUserPhoto(param["userCode"].ToString()) };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                case "login":
                    {
                        ResponseData rData = null;
                        var param = requestData.param as Dictionary<string, object>;
                        string winNum = param["winNum"].ToString(), userCode = param["userCode"].ToString();
                        string userId = rateProcess.GetUserIdByCode(userCode);
                        if (rateProcess.Login(winNum, userCode))
                        {
                            string ip = "";
                            ushort port = 0;
                            this.GetRemoteAddress(connId, ref ip, ref port);
                            this.deviceList.Set(connId, new DeviceInfo()
                            {
                                ID = connId,
                                IP = ip + ":" + port,
                                WindowNumber = winNum,
                                UserID = userId,
                                UserCode = userCode,
                                ConnTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            });
                            rData = new ResponseData { code = "0", request = requestData.method, result = "登录成功!" };
                            this.deviceList.Changed = true;
                        }
                        else
                            rData = new ResponseData { code = "1002", request = requestData.method, result = "窗口或用户信息不正确!" };
                        this.SendWSMessage(connId, rData.ToResultData());
                        this.client.SendMessage(new ClientChangedMessage()
                        {
                            ChangedType = ClientChangedType.Add,
                            UserID = userId,
                            WindowNumber = winNum
                        });
                    }
                    break;
                case "ratesubmit":
                    {
                        var param = requestData.param as Dictionary<string, object>;
                        ResponseData rData = null;
                        if (rateProcess.RateSubmit(device.UserID, device.WindowNumber, param["rateId"].ToString(), param["attitude"].ToString(), param["quality"].ToString(), param["efficiency"].ToString(), param["honest"].ToString()))
                            rData = new ResponseData { code = "0", request = requestData.method, result = "评价成功!" };
                        else
                            rData = new ResponseData { code = "1003", request = requestData.method, result = "评价失败!" };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
                default:
                    {
                        var rData = new ResponseData
                        {
                            code = "999",
                            request = requestData.method,
                            result = "未知指令"
                        };
                        this.SendWSMessage(connId, rData.ToResultData());
                    }
                    break;
            }
            return HandleResult.Ok;
        }

        #region IServiceUI 成员

        public Type UIType
        {
            get { return typeof(FrmService); }
        }

        #endregion
    }
}
