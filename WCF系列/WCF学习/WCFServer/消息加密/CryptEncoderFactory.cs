using System.ServiceModel.Channels;

namespace WCFServer.消息加密
{
    public class CryptEncoderFactory : MessageEncoderFactory
    {
        private MessageEncodingBindingElement innerMessageEncodingBindingElement;
        CryptEncoder messageEncoder;
        string key;
        string iv;
        public CryptEncoderFactory(MessageEncodingBindingElement innerMessageEncodingBindingElement, string key, string iv)
        {
            this.innerMessageEncodingBindingElement = innerMessageEncodingBindingElement;
            this.key = key;
            this.iv = iv;
            messageEncoder = new CryptEncoder(this, key, iv);
        }
        public override MessageEncoder CreateSessionEncoder()
        {
            return base.CreateSessionEncoder();
        }
        public override MessageEncoder Encoder
        {
            get { return messageEncoder; }
        }
        public override MessageVersion MessageVersion
        {
            get { return innerMessageEncodingBindingElement.MessageVersion; }
        }
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get
            {
                return innerMessageEncodingBindingElement;
            }
        }
    }
}
