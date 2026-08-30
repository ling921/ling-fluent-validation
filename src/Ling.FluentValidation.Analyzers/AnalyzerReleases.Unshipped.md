; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
LFV0011 | Usage | Error | DTOs with Ling validation rules require GenerateValidatorAttribute
LFV5164 |  Usage   |  Info   | LengthAttribute can use the exact-length constructor
LFV0010 |  Usage   |  Error   | Attribute target cannot be handled by the source generator
