namespace Ling.FluentValidation.Test.Common;

internal static class Defaults
{
    public static class SystemAttribute
    {
        public const string CodeTemplate = """
            using System.ComponentModel.DataAnnotations;
        
            namespace TestProject
            {{
                public class Sample
                {{
                    [{0}]
                    public {1} Property {{ get; set; }}
                }}
            }}
            """;

        public const int CodeLine = 7;
        public const int CodeColumn = 10;
    }
}
