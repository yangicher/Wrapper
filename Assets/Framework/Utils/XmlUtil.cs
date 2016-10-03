using System.Xml;

namespace Assets.Framework.Utils
{
    public static class XmlUtil
    {
        public static XmlNode FindChildNode(XmlNode parent, string tagName)
        {
            XmlNode curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(tagName))
                {
                    return curr;
                }
                curr = curr.NextSibling;
            }
            return null;
        }

        public static XmlElement FindChildElement(XmlNode parent, string tagName)
        {
            XmlNode curr = parent.FirstChild;
            while (curr != null)
            {
                if (curr.Name.Equals(tagName))
                {
                    return curr as XmlElement;
                }
                curr = curr.NextSibling;
            }
            return null;
        }
    }
}