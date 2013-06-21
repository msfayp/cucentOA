using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Web.Security;

using WX.Request;
using WX.Response;
using WX.Tool;

public partial class _Default : System.Web.UI.Page
{
    const string Token = "CucentOA";		//token
    protected void Page_Load(object sender, EventArgs e)
    {
        string postStr = "";
        Response.Write("ssata"); 

        return;
        //Test();//Debug模式使用，用来模拟微信发送的数据
        Valid();//校验签名
        if (Request.HttpMethod.ToLower() == "post")
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            postStr = Encoding.UTF8.GetString(b);
            if (!string.IsNullOrEmpty(postStr))
            {
                WXTool.WriteLog("二、微信平台接收信息postStr=" + postStr);
                ResponseMsg(postStr);
            }
        }
    }
    //验证是否是微信用户
    private void Valid()
    {
        string echoStr = Request.QueryString["echoStr"] as string;
        if (CheckSignature())
        {
            if (!string.IsNullOrEmpty(echoStr))
            {
                Response.Write(echoStr);
                Response.End();
            }
        }
    }
    /// <summary>
    /// 验证微信签名
    /// </summary>
    /// * 将token、timestamp、nonce三个参数进行字典序排序
    /// * 将三个参数字符串拼接成一个字符串进行sha1加密
    /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
    /// <returns></returns>
    private bool CheckSignature()
    {
        string signature = Request.QueryString["signature"] as String;
        string timestamp = Request.QueryString["timestamp"] as String;
        string nonce = Request.QueryString["nonce"] as String;
        string echostr = Request.QueryString["echostr"] as String;
        string[] ArrTmp = { Token, timestamp, nonce };
        Array.Sort(ArrTmp);     //字典排序
        string tmpStr = string.Join("", ArrTmp);
        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        tmpStr = tmpStr.ToLower();
        WXTool.WriteLog("一、微信签名认证：signature=" + signature + "；Token=" + Token + "；timestamp=" + timestamp + "；nonce=" + nonce + "；tmpStr=" + tmpStr);
        if (tmpStr == signature)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 发送微信
    /// </summary>
    /// <param name="postStr">XML信息</param>
    private void ResponseMsg(string postStr)
    {
        //创建属性文件
        WXRequest wxRequest = new WXRequest();
        getWXRequestXML(postStr, ref wxRequest);//获取微信消息

        string sendStr = "";
        sendStr = sendText(wxRequest);//发送微信信息

        Response.Write(sendStr);
        Response.End();
    }
    /// <summary>
    /// 微信获取文本信息
    /// </summary>
    /// <param name="postObj">xml文档</param>
    private void getWXRequestXML(string postStr, ref WXRequest wxRequest)
    {
        //回复消息部分
        System.Xml.XmlDocument postObj = new System.Xml.XmlDocument();
        postObj.LoadXml(postStr);
        wxRequest = new WXRequest(postObj);
    }

    //测试信息
    private void Test()
    {
        WXRequest wxRequest = new WXRequest("event");
        wxRequest.FromUserName = "ozBS9jra6zFKz_lJayQUNW9TvlHk";
        wxRequest.ToUserName = "gh_0e3834391741";
        wxRequest.eventModel.Event = "subscribe";
        //wxRequest.textModel.Content = "?";

        Response.Write(wxRequest.FromUserName);
        sendText(wxRequest);
    }
    /// <summary>
    /// 微信发送文本信息
    /// </summary>
    /// <param name="wxRequestXMLModel">消息发送方微信号</param>
    /// <param name="ToUserName">消息接收方微信号，一般为公众平台账号微信号</param>
    /// <param name="Content">消息内容</param>
    private string sendText(WXRequest wxRequest)
    {
        string Result = "";
        string FromUserName = wxRequest.FromUserName;
        string ToUserName = wxRequest.ToUserName;
        string Content = "";

        //创建用户对象
        //非员工信息
        //if ()
        //{
            //if ("ZC")//注册校验，自己可以写自己的用户注册功能
            //{
        //}
            if (wxRequest.MsgType.ToLower() == "event")
            {
                //EnumEvent ee=(EnumEvent)Enum.Parse(typeof(EnumEvent), wxRequest.eventModel.Event);
                if (wxRequest.eventModel.Event == EnumMapHelper.GetStringFromEnum(EnumEvent.subscribe))
                {
                    Result = "感谢新的关注！";
                }
                else
                {
                    Result = wxRequest.eventModel.EventKey;
                }
            }
            else if (wxRequest.MsgType.ToLower() == "text")
            {
                //EnumEvent ee=(EnumEvent)Enum.Parse(typeof(EnumEvent), wxRequest.eventModel.Event);
                Content = wxRequest.textModel.Content;
            }
            //else
            //{
            //    Result = "您好，感谢查看XX微信平台！您当前状态是“未注册”。注册方法是：发送：ZC#张三";
            //}
        //}
        //else//公司员工
        //{
        //    //属于员工的自己内部业务
        //}
        
        
        //创建属性文件
        WXResponse wxResponse = new WXResponse("text");
        Result = (Result == "" ? "内容[" + Content + "]" : Result);
        //FromUserName = "ozBS9jra6zFKz_lJayQUNW9TvlHk";//模拟发送人，不支持
        wxResponse.ToUserName = FromUserName;
        wxResponse.FromUserName = ToUserName;
        wxResponse.textModel.Content = Result;
        WXTool.WriteLog("三、微信平台发送信息sendStr=" + Result);
        return wxResponse.ResponseXML();
    }


}
