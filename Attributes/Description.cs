using System;

namespace CFKC.VPV.Attributes
{
    public class Description : Attribute
    {

        public string Text;
        
        public Description(string text = null)
        {
            Text = text;
        }

        public static string GetDescription(Enum en)
        {
            var type = en.GetType();

            var memInfo = type.GetMember(en.ToString());

            if (memInfo == null || memInfo.Length <= 0) return en.ToString();

            var attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((Description)attrs[0]).Text;
            }

            return en.ToString();

        }
    }


}
