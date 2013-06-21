using System;
using System.Collections.Generic;

using WX.Tool;
using System.Text;

namespace WX.Response
{
    /*=========================== 微信开发工具类 ===========================*/
    /*========================= 开发时间2013-01-21 =========================*/
    /*=========================== 开发人员Human ===========================*/
    /*=========================== QQ：332345375 ===========================*/

    /// <summary>
    /// 微信发送类
    /// </summary>
    [Serializable]
    public class WXResponse
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WXResponse()
		{
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public WXResponse(string msgType)
        {
            this.msgType = msgType;
            switch (msgType)
            {
                case "text":
                    _textmodel = new TextWXResponse_Model();
                    break;
                case "music":
                    _musicmodel = new MusicWXResponse_Model();
                    break;
                case "news":
                    _newsmodel = new NewsWXResponse_Model();
                    break;
            }
        }
        /// <summary>
        /// 发送XML信息
        /// </summary>
        public string ResponseXML()
        {
            StringBuilder result = new StringBuilder();
            funcFlag = (funcFlag == null || funcFlag.ToString() == "" ? 0 : funcFlag);

            result.Append("<xml><ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            result.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            result.Append("<CreateTime>" + WXTool.ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType>");
            result.Append("<MsgType><![CDATA[" + this.msgType + "]]></MsgType>");
            switch (this.msgType)
            {
                case "text":
                    result.Append("<Content><![CDATA[" + textModel.Content + "]]></Content>");
                    /*<xml>
                     <ToUserName><![CDATA[toUser]]></ToUserName>
                     <FromUserName><![CDATA[fromUser]]></FromUserName>
                     <CreateTime>12345678</CreateTime>
                     <MsgType><![CDATA[text]]></MsgType>
                     <Content><![CDATA[content]]></Content>
                     <FuncFlag>0</FuncFlag>
                     </xml>*/
                    break;
                case "music":
                    result.Append("<Music><Title><![CDATA[" + musicModel.Title + "]]></Title>");
                    result.Append("<Description><![CDATA[" + musicModel.Description + "]]></Description>");
                    result.Append("<MusicUrl><![CDATA[" + musicModel.MusicUrl + "]]></MusicUrl>");
                    result.Append("<HQMusicUrl><![CDATA[" + musicModel.HQMusicUrl + "]]></HQMusicUrl></Music>");
                    /*<xml>
                     <ToUserName><![CDATA[toUser]]></ToUserName>
                     <FromUserName><![CDATA[fromUser]]></FromUserName>
                     <CreateTime>12345678</CreateTime>
                     <MsgType><![CDATA[music]]></MsgType>
                     <Music>
                     <Title><![CDATA[TITLE]]></Title>
                     <Description><![CDATA[DESCRIPTION]]></Description>
                     <MusicUrl><![CDATA[MUSIC_Url]]></MusicUrl>
                     <HQMusicUrl><![CDATA[HQ_MUSIC_Url]]></HQMusicUrl>
                     </Music>
                     <FuncFlag>0</FuncFlag>
                     </xml>*/   
                    break;
                case "news":
                    result.Append("<ArticleCount>" + newsModel.ArticleCount + "</ArticleCount><Articles>");
                    foreach (NewsArticlesWXResponse_Model item in newsModel.Articles)
                    {
                        result.Append("<item><Title><![CDATA[" + item.Title + "]]></Title><Description><![CDATA[" + item.Description + "]]></Description>");
                        result.Append("<PicUrl><![CDATA[" + item.PicUrl + "]]></PicUrl><Url><![CDATA[" + item.Url + "]]></Url></item>");
                    }
                    result.Append("</Articles>");

                    /*<xml>
                     <ToUserName><![CDATA[toUser]]></ToUserName>
                     <FromUserName><![CDATA[fromUser]]></FromUserName>
                     <CreateTime>12345678</CreateTime>
                     <MsgType><![CDATA[news]]></MsgType>
                     <ArticleCount>2</ArticleCount>
                     <Articles>
                     <item>
                     <Title><![CDATA[title1]]></Title> 
                     <Description><![CDATA[description1]]></Description>
                     <PicUrl><![CDATA[picurl]]></PicUrl>
                     <Url><![CDATA[url]]></Url>
                     </item>
                     <item>
                     <Title><![CDATA[title]]></Title>
                     <Description><![CDATA[description]]></Description>
                     <PicUrl><![CDATA[picurl]]></PicUrl>
                     <Url><![CDATA[url]]></Url>
                     </item>
                     </Articles>
                     <FuncFlag>1</FuncFlag>
                     </xml> */
                    break;
            }
            result.Append("</Articles><FuncFlag>" + funcFlag + "</FuncFlag></xml>");
            return result.ToString();
        }
        private string toUserName;
        private string fromUserName;
        private string createTime;
        private string msgType;
        private int? funcFlag;

        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName
        {
            get { return fromUserName; }
            set { fromUserName = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }
        /// <summary>
        /// 信息类型 文本消息:text,音乐消息:music,图文消息:news
        /// </summary>
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }
        /// <summary>
        /// 星标类型 位0x0001被标志时，星标刚收到的消息。
        /// </summary>
        public int? FuncFlag
        {
            get { return funcFlag; }
            set { funcFlag = value; }
        } 

        private TextWXResponse_Model _textmodel;
        private MusicWXResponse_Model _musicmodel;
        private NewsWXResponse_Model _newsmodel;
        /// <summary>
        /// 文本信息发送类
        /// </summary>
        public TextWXResponse_Model textModel
        {
            get { return _textmodel; }
            set { _textmodel = value; }
        }
        /// <summary>
        /// 音乐信息发送类
        /// </summary>
        public MusicWXResponse_Model musicModel
        {
            get { return _musicmodel; }
            set { _musicmodel = value; }
        }
        /// <summary>
        /// 图文信息发送类
        /// </summary>
        public NewsWXResponse_Model newsModel
        {
            get { return _newsmodel; }
            set { _newsmodel = value; }
        }
    }
    /// <summary>
    /// 微信发送类-文本消息类
    /// </summary>
    [Serializable]
    public class TextWXResponse_Model
    {
        private string _content = "";

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
    }
    /// <summary>
    /// 微信发送类-音乐消息类
    /// </summary>
    [Serializable]
    public class MusicWXResponse_Model
    {
        private string _title;
        private string _description;
        private string _musicurl = "";
        private string _hqmusicurl = "";
        
        /// <summary>
        /// 音乐消息标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 音乐消息描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicUrl 
        {
            get { return _musicurl; }
            set { _musicurl = value; }
        }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl
        {
            get { return _hqmusicurl; }
            set { _hqmusicurl = value; }
        }
    }
    /// <summary>
    /// 微信发送类-图文消息类
    /// </summary>
    [Serializable]
    public class NewsWXResponse_Model
    {
        private int _articlecount;
        private List<NewsArticlesWXResponse_Model> _articles;

        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount
        {
            get { return _articlecount; }
            set { _articlecount = value; }
        }
        /// <summary>
        /// 正文
        /// </summary>
        public List<NewsArticlesWXResponse_Model> Articles
        {
            get { return _articles; }
            set { _articles = value; }
        }
    }

    /// <summary>
    /// 微信发送类-图文消息类[多条图文消息信息，默认第一个item为大图]
    /// </summary>
    [Serializable]
    public class NewsArticlesWXResponse_Model
    {
        private string _title;
        private string _description;
        private string _picurl;
        private string _url;

        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80，限制图片链接的域名需要与开发者填写的基本资料中的Url一致
        /// </summary>
        public string PicUrl
        {
            get { return _picurl; }
            set { _picurl = value; }
        }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
    }
}
