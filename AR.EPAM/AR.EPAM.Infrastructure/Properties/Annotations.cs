using System;
#pragma warning disable 1584,1711,1572,1581,1580
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedParameter.Local
namespace AR.EPAM.Infrastructure
{
    /// <summary>
    /// Indicates that the function argument should be string literal and match one of the parameters
    /// of the caller function.
    /// For example, ReSharper annotates the parameter of <see cref="ArgumentNullException"/>.
    /// </summary>
    /// <example>
    /// <code>
    /// public void Foo(string param)
    /// {
    ///   if (param == null)
    ///     throw new ArgumentNullException("par"); // Warning: Cannot resolve symbol
    /// }
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class InvokerParameterNameAttribute : Attribute { }

    /// <summary>
    /// Indicates that the value of the marked element could never be <c>null</c>
    /// </summary>
    /// <example>
    /// <code>
    /// [NotNull]
    /// public object Foo()
    /// {
    ///   return null; // Warning: Possible 'null' assignment
    /// } 
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute { }
}