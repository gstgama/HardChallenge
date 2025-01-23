using SmartVault.Library;
using System.Xml.Serialization;

namespace SmartVault.CodeGeneration
{
    [XmlRoot(ElementName = "oauthBusinessObject")]
    public class OAuthBusinessObject : BusinessObject
    {
        [XmlElement(ElementName = "clientId")]
        public string? ClientId { get; set; }

        [XmlElement(ElementName = "clientSecret")]
        public string? ClientSecret { get; set; }

        [XmlElement(ElementName = "redirectUri")]
        public string? RedirectUri { get; set; }

        [XmlElement(ElementName = "authorizationEndpoint")]
        public string? AuthorizationEndpoint { get; set; }

        [XmlElement(ElementName = "tokenEndpoint")]
        public string? TokenEndpoint { get; set; }

        [XmlElement(ElementName = "scope")]
        public string? Scope { get; set; }

        [XmlElement(ElementName = "providerName")]
        public string? ProviderName { get; set; }
    }
}