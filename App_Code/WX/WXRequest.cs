using System;
using System.Xml;
using WX.Tool;
using System.ComponentModel;
namespace WX.Request
{
    /*=========================== 微信开发工具类 ===========================*/
    /*========================= 开发时间2013-01-21 =========================*/
    /*=========================== 开发人员Human ===========================*/
    /*=========================== QQ：332345375 ===========================*/

    /// <summary>
    /// 微信请求类
    /// </summary>
    [Serializable]
    public class WXRequest
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public WXRequest()
        {
            //_getmodel = new GetWXRequest_Model();
        }
        /// <summary>
        /// 根据微信请求的类型
        /// </summary>
        public WXRequest(string msgType)
        {
            this.msgType = msgType;
            switch (msgType)
            {
                case "text":
                    _textmodel = new TextWXRequest_Model();
                    break;
                case "image":
                    _imagemodel = new ImageWXRequest_Model();
                    break;
                case "location":
                    _locationmodel = new LocationWXRequest_Model();
                    break;
                case "link":
                    _linkmodel = new LinkWXRequest_Model();
                    break;
                case "event":
                    _eventmodel = new EventWXRequest_Model();
                    break;
            }
        }
        /// <summary>
        /// 根据微信请求的类型
        /// </summary>
        public WXRequest(XmlDocument xmlObj)
        {
            XmlElement postElement = xmlObj.DocumentElement;
            ToUserName = postElement.SelectSingleNode("ToUserName").InnerText;
            FromUserName = postElement.SelectSingleNode("FromUserName").InnerText;
            CreateTime = postElement.SelectSingleNode("CreateTime").InnerText;
            MsgType = postElement.SelectSingleNode("MsgType").InnerText;

            switch (MsgType)//信息类型 文本消息:text,消息类型:image,地理位置:location,链接消息:link,事件推送:event
            {
                case "text":
                    textModel = new TextWXRequest_Model();
                    textModel.Content = postElement.SelectSingleNode("Content").InnerText;
                    textModel.MsgId = Convert.ToInt64(postElement.SelectSingleNode("MsgId").InnerText);
                    break;
                case "image":
                    imageModel = new ImageWXRequest_Model();
                    imageModel.PicUrl = postElement.SelectSingleNode("PicUrl").InnerText;
                    imageModel.MsgId = Convert.ToInt64(postElement.SelectSingleNode("MsgId").InnerText);
                    break;
                case "location":
                    locationModel = new LocationWXRequest_Model();
                    locationModel.Location_X = postElement.SelectSingleNode("Location_X").InnerText;
                    locationModel.Location_Y = postElement.SelectSingleNode("Location_Y").InnerText;
                    locationModel.Scale = Convert.ToInt32(postElement.SelectSingleNode("Scale").InnerText);
                    locationModel.Label = postElement.SelectSingleNode("Label").InnerText;
                    locationModel.MsgId = Convert.ToInt64(postElement.SelectSingleNode("MsgId").InnerText);
                    break;
                case "link":
                    linkModel = new LinkWXRequest_Model();
                    linkModel.Title = postElement.SelectSingleNode("Title").InnerText;
                    linkModel.Description = postElement.SelectSingleNode("Description").InnerText;
                    linkModel.Url = postElement.SelectSingleNode("Url").InnerText;
                    linkModel.MsgId = Convert.ToInt64(postElement.SelectSingleNode("MsgId").InnerText);
                    break;
                case "event":
                    eventModel = new EventWXRequest_Model();
                    eventModel.Event = postElement.SelectSingleNode("Event").InnerText;
                    eventModel.EventKey = postElement.SelectSingleNode("EventKey").InnerText;
                    break;
            }
        }
        private string toUserName;
        private string fromUserName;
        private string createTime;
        private string msgType;

        /// <summary>
        /// 消息接收方微信号，一般为公众平台账号微信号
        /// </summary>
        public string ToUserName
        {
            get { return toUserName; }
            set { toUserName = value; }
        }
        /// <summary>
        /// 消息发送方微信号
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
        /// 信息类型 文本消息:text,消息类型:image,地理位置:location,链接消息:link,事件推送:event
        /// </summary>
        public string MsgType
        {
            get { return msgType; }
            set { msgType = value; }
        }

        //private GetWXRequest_Model _getmodel;
        private TextWXRequest_Model _textmodel;
        private ImageWXRequest_Model _imagemodel;
        private LocationWXRequest_Model _locationmodel;
        private LinkWXRequest_Model _linkmodel;
        private EventWXRequest_Model _eventmodel;
        /// <summary>
        /// 签名信息请求类
        /// </summary>
        //public GetWXRequest_Model getModel
        //{
        //    get { return _getmodel; }
        //    set { _getmodel = value; }
        //}
        /// <summary>
        /// 文本信息请求类
        /// </summary>
        public TextWXRequest_Model textModel
        {
            get { return _textmodel; }
            set { _textmodel = value; }
        }
        /// <summary>
        /// 图片信息请求类
        /// </summary>
        public ImageWXRequest_Model imageModel
        {
            get { return _imagemodel; }
            set { _imagemodel = value; }
        }
        /// <summary>
        /// 地理位置信息请求类
        /// </summary>
        public LocationWXRequest_Model locationModel
        {
            get { return _locationmodel; }
            set { _locationmodel = value; }
        }
        /// <summary>
        /// 链接信息请求类
        /// </summary>
        public LinkWXRequest_Model linkModel
        {
            get { return _linkmodel; }
            set { _linkmodel = value; }
        }
        /// <summary>
        /// 事件推送信息请求类
        /// </summary>
        public EventWXRequest_Model eventModel
        {
            get { return _eventmodel; }
            set { _eventmodel = value; }
        }
    }
    ///// <summary>
    ///// 微信请求类-签名类
    ///// </summary>
    //[Serializable]
    //public class GetWXRequest_Model
    //{
    //    private string _signature = "";
    //    private string _timestamp = "";
    //    private string _nonce = "";
    //    private string _echostr = "";
    //    private bool _islogin = false;

    //    /// <summary>
    //    /// 微信加密签名
    //    /// </summary>
    //    public string signature
    //    {
    //        get { return _signature; }
    //        set { _signature = value; }
    //    }
    //    /// <summary>
    //    /// 时间戳
    //    /// </summary>
    //    public string timestamp
    //    {
    //        get { return _timestamp; }
    //        set { _timestamp = value; }
    //    }
    //    /// <summary>
    //    /// 随机数
    //    /// </summary>
    //    public string nonce
    //    {
    //        get { return _nonce; }
    //        set { _nonce = value; }
    //    }
    //    /// <summary>
    //    /// 随机字符串
    //    /// </summary>
    //    public string echostr
    //    {
    //        get { return _echostr; }
    //        set { _echostr = value; }
    //    }
    //    /// <summary>
    //    /// 是否签名成功
    //    /// </summary>
    //    public bool isLogin
    //    {
    //        get { return _islogin; }
    //        set { _islogin = value; }
    //    }
    //}
    /// <summary>
    /// 微信请求类-文本消息类
    /// </summary>
    [Serializable]
    public class TextWXRequest_Model
    {
        private string _content = "";
        private Int64 _msgid;

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId
        {
            get { return _msgid; }
            set { _msgid = value; }
        }
    }
    /// <summary>
    /// 微信请求类-图片消息类
    /// </summary>
    [Serializable]
    public class ImageWXRequest_Model
    {
        private string _picurl = "";
        private Int64 _msgid;

        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl
        {
            get { return _picurl; }
            set { _picurl = value; }
        }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId
        {
            get { return _msgid; }
            set { _msgid = value; }
        }
    }
    /// <summary>
    /// 微信请求类-地理位置消息类
    /// </summary>
    [Serializable]
    public class LocationWXRequest_Model
    {
        private string _location_x = "";
        private string _location_y = "";
        private int _scale;
        private string _label = "";
        private Int64 _msgid;

        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X
        {
            get { return _location_x; }
            set { _location_x = value; }
        }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y
        {
            get { return _location_y; }
            set { _location_y = value; }
        }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label
        {
            get { return _label; }
            set { _label = value; }
        }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId
        {
            get { return _msgid; }
            set { _msgid = value; }
        }
    }
    /// <summary>
    /// 微信请求类-链接消息类
    /// </summary>
    [Serializable]
    public class LinkWXRequest_Model
    {
        private string _title;
        private string _description;
        private string _url;
        private Int64 _msgid;

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public Int64 MsgId
        {
            get { return _msgid; }
            set { _msgid = value; }
        }
    }
    /// <summary>
    /// 微信请求类-事件推送类
    /// </summary>
    [Serializable]
    public class EventWXRequest_Model
    {
        private string _event;
        private string _eventkey;

        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件) 
        /// </summary>
        public string Event
        {
            get { return _event; }
            set { _event = value; }
        }
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey
        {
            get { return _eventkey; }
            set { _eventkey = value; }
        }
        //public enum Event
        //{
        //    [EnumItemDescription("subscribe(订阅)")]
        //    subscribe = 1,
        //    [EnumItemDescription("unsubscribe(取消订阅)")]
        //    unsubscribe = 2,
        //    [EnumItemDescription("CLICK(自定义菜单点击事件)")]
        //    CLICK = 3
        //}
        //public enum EventKey : int { one = 1, two = 2 };
    }
    //public enum Event : int { subscribe = 1, unsubscribe = 2, CLICK = 3 };
    //public enum EventKey : int { one = 1, two = 2 };
    public enum EnumEvent
    {
        [DescriptionAttribute("subscribe")]
        subscribe = 1,
        [DescriptionAttribute("unsubscribe")]
        unsubscribe = 2,
        [DescriptionAttribute("click")]
        CLICK = 3,
    }
    public enum EnumEventKey : int { one = 1, two = 2 };
}
