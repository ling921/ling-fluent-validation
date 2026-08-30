using System.Diagnostics;
using System.Text;

namespace Ling.FluentValidation.Generators.Helpers;

/// <summary>
/// Represents a builder for generating code with indentation and line breaks.
/// </summary>
[DebuggerDisplay("Length: {Length}, {ToString()}")]
internal sealed class CodeBuilder
{
    private int _indentLevel;
    private bool _newLine;
    private readonly StringBuilder _sb;

    /// <summary>
    /// Gets the size of the indentation.
    /// </summary>
    public int IndentSize { get; }

    /// <summary>
    /// Gets the length of the code builder.
    /// </summary>
    public int Length => _sb.Length;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeBuilder"/> class.
    /// </summary>
    /// <param name="indentSize">The size of the indentation. Default is 4.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="indentSize"/> is less than or equal to 0.</exception>
    public CodeBuilder(int indentSize = 4)
    {
        if (indentSize <= 0) throw new ArgumentOutOfRangeException(nameof(indentSize));

        _indentLevel = 0;
        _newLine = true;
        _sb = new StringBuilder();

        IndentSize = indentSize;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeBuilder"/> class with the specified value.
    /// </summary>
    /// <param name="value">The initial value of the code builder.</param>
    /// <param name="indentSize">The size of the indentation. Default is 4.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="indentSize"/> is less than or equal to 0.</exception>
    public CodeBuilder(string value, int indentSize = 4)
    {
        if (indentSize <= 0) throw new ArgumentOutOfRangeException(nameof(indentSize));

        _indentLevel = 0;
        _newLine = false;
        _sb = new StringBuilder(value);

        IndentSize = indentSize;
    }

    /// <summary>
    /// Appends an opening brace to the code builder.
    /// </summary>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder OpenBrace()
    {
        AppendLine("{");
        _indentLevel++;
        _newLine = true;
        return this;
    }

    /// <summary>
    /// Appends a closing brace to the code builder.
    /// </summary>
    /// <param name="textAfterBrace">Optional text to append after the closing brace, e.g. ')'.</param>
    /// <param name="appendSemicolon">Indicates whether to append a semicolon after the closing brace.</param>
    /// <returns>The same instance for chaining.</returns>
    /// <exception cref="InvalidOperationException">If the indent level is less than or equal to 0.</exception>
    public CodeBuilder CloseBrace(string? textAfterBrace = null, bool appendSemicolon = false)
    {
        if (_indentLevel <= 0) throw new InvalidOperationException();

        _indentLevel--;
        Append("}");
        if (textAfterBrace is not null)
        {
            Append(textAfterBrace);
        }
        if (appendSemicolon)
        {
            Append(";");
        }
        AppendLine();
        _newLine = true;
        return this;
    }

    /// <summary>
    /// Closes all open braces in the code builder.
    /// </summary>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder CloseAllBrace()
    {
        while (_indentLevel > 0)
        {
            CloseBrace();
        }
        return this;
    }

    /// <summary>
    /// Appends text to the code builder.
    /// </summary>
    /// <param name="text">The text to append.</param>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder Append(string text)
    {
        EnsureIndent();
        _sb.Append(text);
        return this;
    }

    /// <summary>
    /// Appends a line of text to the code builder.
    /// </summary>
    /// <param name="text">The text to append.</param>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder AppendLine(string text)
    {
        EnsureIndent();
        _sb.AppendLine(text);
        _newLine = true;
        return this;
    }

    /// <summary>
    /// Appends a line to the code builder.
    /// </summary>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder AppendLine()
    {
        _sb.AppendLine();
        _newLine = true;
        return this;
    }

    /// <summary>
    /// Appends a formatted text to the code builder.
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments.</param>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder AppendFormat(string format, params object?[] args)
    {
        EnsureIndent();
        _sb.AppendFormat(format, args);
        return this;
    }

    /// <summary>
    /// Appends a formatted line of text to the code builder.
    /// </summary>
    /// <param name="format">The format string.</param>
    /// <param name="args">The arguments.</param>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder AppendFormatLine(string format, params object?[] args)
    {
        EnsureIndent();
        _sb.AppendFormat(format, args);
        AppendLine();
        return this;
    }

    /// <summary>
    /// Increases the indent level by one.
    /// </summary>
    /// <returns>The same instance for chaining.</returns>
    public CodeBuilder IncreaseIndentLevel()
    {
        _indentLevel++;
        return this;
    }

    /// <summary>
    /// Decreases the indent level by one.
    /// </summary>
    /// <returns>The same instance for chaining.</returns>
    /// <exception cref="InvalidOperationException">If the indent level is less than or equal to 0.</exception>
    public CodeBuilder DecreaseIndentLevel()
    {
        if (_indentLevel <= 0) throw new InvalidOperationException();
        _indentLevel--;
        return this;
    }

    /// <summary>
    /// Clears the code builder.
    /// </summary>
    public void Clear()
    {
        _indentLevel = 0;
        _newLine = true;
        _sb.Clear();
    }

    /// <summary>
    /// Returns the code builder as a string.
    /// <para>
    /// If there are unclosed braces in the code builder, they will be all closed.
    /// </para>
    /// </summary>
    /// <returns>The code text.</returns>
    public override string ToString()
    {
        CloseAllBrace();
        return _sb.ToString();
    }

    /// <summary>
    /// Ensures current line has leading indent.
    /// </summary>
    private void EnsureIndent()
    {
        if (_newLine && _indentLevel > 0)
        {
            _sb.Append(' ', IndentSize * _indentLevel);
        }
        _newLine = false;
    }
}
