using System.Reflection;

namespace Taf.Utility{
    public interface IBuildStep{
        int Sequence{ get; }

        int Times{ get; }

        MethodInfo Handler{ get; set; }
    }
}
