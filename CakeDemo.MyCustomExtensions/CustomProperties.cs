using Cake.Core;
using Cake.Core.Annotations;

namespace CakeDemo.MyCustomExtensions
{
    public static class CustomProperties
    {
        [CakePropertyAlias]
        public static int FourtySix(this ICakeContext context)
        {
            return 46;
        }

        [CakePropertyAlias]
        public static string CurrentVersion(this ICakeContext context)
        {
            return "1.0.0.1";
        }
    }
}
