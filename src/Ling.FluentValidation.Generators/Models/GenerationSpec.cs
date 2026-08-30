using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Text;

namespace Ling.FluentValidation.Generators.Models;

/// <summary>
/// A model describing the generation spec for a validator.
/// </summary>
/// <param name="AssemblyName">The name of the assembly to generate for.</param>
/// <param name="ReferencedAssemblies">The referenced assemblies to include in the assembly.</param>
/// <param name="HierarchyInfo">The hierarchy info for the type to generate validator for.</param>
/// <param name="Namespace">The namespace of the validator.</param>
/// <param name="Keyword">The access modifier for the validator.</param>
/// <param name="IsSealed">Whether the validator is sealed.</param>
/// <param name="ClassLevelCascadeMode">The class-level cascade mode for the validator to set.</param>
/// <param name="RuleLevelCascadeMode">The rule-level cascade mode for the validator to set.</param>
/// <param name="SupportsSplitCascadeModes">Whether the referenced FluentValidation version exposes separate class-level and rule-level cascade modes.</param>
/// <param name="Includes">The validators to include in the validator.</param>
/// <param name="Members">All the members to generate rules for in the validator.</param>
/// <param name="MaybeWhenClauses">The possible when-clauses for the validator.</param>
/// <param name="Location">The location of the type to generate validator for.</param>
internal sealed record GenerationSpec(
    string AssemblyName,
    ImmutableArray<AssemblyIdentity> ReferencedAssemblies,
    HierarchyInfo HierarchyInfo,
    string Namespace,
    string Keyword,
    bool IsSealed,
    string? ClassLevelCascadeMode,
    string? RuleLevelCascadeMode,
    bool SupportsSplitCascadeModes,
    ImmutableArray<string> Includes,
    ImmutableArray<MemberInfo> Members,
    ImmutableDictionary<string, MemberKind> MaybeWhenClauses,
    Location Location)
{
    /// <summary>
    /// The class name of the validator to generate.
    /// </summary>
    public string GenerateClassName => $"{HierarchyInfo.MetadataName}Validator";

    /// <summary>
    /// The full-qualified name of the validator to generate.
    /// </summary>
    public string GenerateClassFullName => $"global::{Namespace}.{GenerateClassName}";

    /// <summary>
    /// The full-qualified name of the type to generate validator for.
    /// </summary>
    public string TargetClassFullName
    {
        get
        {
            var sb = new StringBuilder("global::");
            if (HierarchyInfo.Namespace.Length > 0)
            {
                sb.Append(HierarchyInfo.Namespace);
                sb.Append('.');
            }
            for (int i = HierarchyInfo.Hierarchy.Length - 1; i >= 0; i--)
            {
                if (i != HierarchyInfo.Hierarchy.Length - 1)
                {
                    sb.Append('.');
                }
                sb.Append(HierarchyInfo.Hierarchy[i].QualifiedName);
            }
            return sb.ToString();
        }
    }

}
