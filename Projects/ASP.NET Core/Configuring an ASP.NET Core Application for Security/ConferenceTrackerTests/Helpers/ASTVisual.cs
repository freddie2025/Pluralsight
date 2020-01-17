using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConferenceTrackerTests.Helpers
{
    public class ConvertOption
    {
        public bool IsScript = false;
        public bool IsIncludeToken = false;
        public string GraphName = "syntaxtree";
    }

    public static class ASTVisual
    {
        public static void Convert(string code, TextWriter output, ConvertOption opts = default(ConvertOption))
        {
            opts = opts ?? new ConvertOption();
            var csopt = new CSharpParseOptions().WithKind(opts.IsScript ? SourceCodeKind.Script : SourceCodeKind.Regular);
            var rootNode = CSharpSyntaxTree.ParseText(code, csopt);
            WriteDotPrefix(output, opts.GraphName);
            OutputInfo(rootNode.GetRoot(), output, 1, opts.IsIncludeToken, null);
            WriteDotSuffix(output);
        }

        static void WriteDotPrefix(TextWriter tw, string name)
        {
            tw.WriteLine($"graph {name} {{");
        }
        static void WriteDotSuffix(TextWriter tw)
        {
            tw.WriteLine("}");
        }
        static string DotLabelFilter(string txt)
        {
            return txt.Replace("\n", "\\l").Replace("\"", "\\\"").Replace("\r", "");
        }

        static void OutputInfo(SyntaxToken token, TextWriter output, int depth)
        {
            var prefix = new string(Enumerable.Range(0, depth).SelectMany(x => "  ").ToArray());
            output.WriteLine($"{prefix}tokenkind={token.Kind()},{DotLabelFilter(token.ValueText)}");
        }

        static void OutputInfo(SyntaxNode node, TextWriter output, int depth, bool includeToken, Dictionary<SyntaxKind, int> syntaxCount)
        {
            if (syntaxCount == null)
            {
                syntaxCount = new Dictionary<SyntaxKind, int>();
            }
            var prefix = new string(Enumerable.Range(0, depth).SelectMany(x => "  ").ToArray());
            if (!syntaxCount.ContainsKey(node.Kind()))
            {
                syntaxCount[node.Kind()] = 0;
            }
            var pnodeName = DotLabelFilter($"{node.Kind()}_{syntaxCount[node.Kind()]}");
            var pnodeLabel = DotLabelFilter(node.ToString());
            var pnodeDecl = $"{prefix}{pnodeName} [label=\"{node.Kind()}\\l{pnodeLabel}\\l\",shape=box];";
            output.WriteLine($"{pnodeDecl}");
            foreach (var child in node.ChildNodesAndTokens())
            {
                if (!syntaxCount.ContainsKey(child.Kind()))
                {
                    syntaxCount[child.Kind()] = 0;
                }
                if (child.IsNode)
                {
                    var cnode = (SyntaxNode)child;
                    var cnodeText = DotLabelFilter($"{cnode.Kind()}_{syntaxCount[child.Kind()]}");
                    output.WriteLine($"{prefix}\"{pnodeName}\" -- \"{cnodeText}\";");
                    OutputInfo((SyntaxNode)child, output, depth + 1, includeToken, syntaxCount);
                }
                else if (includeToken)
                {
                    var ctoken = (SyntaxToken)child;
                    var ctokenText = DotLabelFilter($"{ctoken.Kind()}_{syntaxCount[child.Kind()]}");
                    output.WriteLine($"{prefix}\"{ctokenText}\" [label=\"{ctoken.Kind()}\\n{DotLabelFilter(ctoken.ValueText)}\",shape=circle];");
                    output.WriteLine($"{prefix}\"{pnodeName}\" -- \"{ctokenText}\";");
                }
                syntaxCount[child.Kind()] += 1;
            }
        }
    }
}
