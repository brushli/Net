using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Xml;

namespace RobinLib
{
    public sealed class CompressEncodingBindingElement : MessageEncodingBindingElement
    { 
        private XmlDictionaryReaderQuotas readerQuotas;
        private MessageEncodingBindingElement innerMessageEncodingBindingElement;
        private CompressAlgorithm algorithm;
        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get
            {
                return innerMessageEncodingBindingElement;
            }
        }

        public CompressAlgorithm CompressAlgorithm
        {
            get
            {
                return algorithm;
            }
        }

        public CompressEncodingBindingElement(MessageEncodingBindingElement innerMessageEncodingBindingElement, CompressAlgorithm algorithm)
        {
            this.readerQuotas = new XmlDictionaryReaderQuotas();
            this.algorithm = algorithm;
            this.innerMessageEncodingBindingElement = innerMessageEncodingBindingElement;
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelFactory<TChannel>();
        }
        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new CompressEncoderFactory(innerMessageEncodingBindingElement,algorithm);
        }
        public override T GetProperty<T>(BindingContext context)  
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return this.readerQuotas as T;
            }
            return base.GetProperty<T>(context);

        }
        public override MessageVersion MessageVersion
        {
            get
            {
                return innerMessageEncodingBindingElement.MessageVersion;
            }
            set
            {
                innerMessageEncodingBindingElement.MessageVersion = value;
            }
        }
        
        public override BindingElement Clone()
        {
            return new CompressEncodingBindingElement(innerMessageEncodingBindingElement,algorithm);
        } 
    }
}
