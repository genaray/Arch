using System.Text;
using Arch.Core.Extensions.Internal;
using Arch.Core.Utils;

namespace Arch.Core.Extensions;


public static class T4Helpers
{
    public static string Indent(this StringBuilder sb, int spaces)
    {
        var indent = new string(' ', spaces);
        return sb.ToString().Replace("\n", "\n" + indent);
    }
}

/// <summary>
///     The <see cref="WorldExtensions"/> class
///     adds several useful utility methods to the <see cref="World"/>.
/// </summary>
[WorldExtensions]
public static class WorldExtensions
{


}
