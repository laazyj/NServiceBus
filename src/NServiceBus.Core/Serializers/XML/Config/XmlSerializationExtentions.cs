﻿namespace NServiceBus
{
    using System.Configuration;
    using NServiceBus.Serialization;
    using NServiceBus.Serializers.XML;

    /// <summary>
    /// Custom extentions for the <see cref="XmlSerializer"/> serializer.
    /// </summary>
    public static class XmlSerializationExtentions
    {
        /// <summary>
        /// Tells the serializer to not wrap properties which have either XDocument or XElement with a "PropertyName" element.
        /// By default the xml serializer serializes the following message
        /// </summary>
        /// <code>
        /// interface MyMessage { XDocument Property { get; set; } }
        /// </code>
        /// into the following structure
        /// <code>
        /// <MyMessage>
        ///     <Property>
        ///       ... Content of the XDocument
        ///     </Property>
        /// </MyMessage>
        /// </code>
        /// This flag allows to omit the property tag wrapping. Which results to
        /// <code>
        /// <MyMessage>
        ///       ... Content of the XDocument
        /// </MyMessage>
        /// </code>
        /// When this feature is enable the root element of the XDocument must match the name of the property. The following would not work and lead to deserialization error:
        /// <code>
        /// <MyMessage>
        ///       <Root>
        ///         ...
        ///       </Root>
        /// </MyMessage>
        /// </code>
        public static SerializationExtentions<XmlSerializer> DontWrapRawXml(this SerializationExtentions<XmlSerializer> config)
        {
            config.Settings.SetProperty<XmlMessageSerializer>(s => s.SkipWrappingRawXml, true);

            return config;
        }
        /// <summary>
        /// Configures the serializer to use a custom namespace. (http://tempuri.net) is the default.
        /// <para>If the provided namespace ends with trailing forward slashes, those will be removed on the fly.</para>
        /// </summary>
        public static SerializationExtentions<XmlSerializer> Namespace(this SerializationExtentions<XmlSerializer> config, string namespaceToUse)
        {
            if (string.IsNullOrEmpty(namespaceToUse))
            {
                throw new ConfigurationErrorsException("Can't use a null or empty string as the xml namespace");
            }

            config.Settings.SetProperty<XmlMessageSerializer>(s => s.Namespace, namespaceToUse);

            return config;
        }

        /// <summary>
        /// Tells the serializer to sanitize the input data from illegal characters
        /// </summary>
        public static SerializationExtentions<XmlSerializer> SanitizeInput(this SerializationExtentions<XmlSerializer> config)
        {
            config.Settings.SetProperty<XmlMessageSerializer>(s => s.SanitizeInput, true);

            return config;
        }
    }
}