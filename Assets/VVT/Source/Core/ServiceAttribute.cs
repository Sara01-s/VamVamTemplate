using System;

namespace VVT {

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ServiceAttribute : Attribute { }
}
