using JonnyGallo.Abstractions;
using ObjCRuntime;

namespace JonnyGallo.Common.iOS
{
    public class EnvironmentService : IEnvironmentService
    {
        #region IEnvironmentService implementation

        public bool IsRealDevice => Runtime.Arch == Arch.DEVICE;

        #endregion
    }
}

