using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace RobinLib
{
    public class CompressEncoderFactory:MessageEncoderFactory
    {
        private MessageEncodingBindingElement innerMessageEncodingBindingElement;
        CompressEncoder messageEncoder;
        private CompressAlgorithm algorithm;
        public CompressEncoderFactory(MessageEncodingBindingElement innerMessageEncodingBindingElement, CompressAlgorithm algorithm)
        {
            this.innerMessageEncodingBindingElement = innerMessageEncodingBindingElement;
            this.algorithm = algorithm;
            messageEncoder = new CompressEncoder(this,algorithm);
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
