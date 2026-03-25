using System.Collections.Generic;
using System.Linq;

namespace sample
{
    public static class CDKManager
    {
        public static List<CDK> GetAllCDKs()
        {
            return ApiClient.Get<List<CDK>>("cdks.php");
        }

        public static void SaveCDKs(List<CDK> cdks)
        {
            foreach (var cdk in cdks)
            {
                ApiClient.Post<dynamic>("update_cdk.php", cdk);
            }
        }
    }

    public class CDK
    {
        public string Cdk { get; set; }
        public bool IsActivated { get; set; }
    }
}
