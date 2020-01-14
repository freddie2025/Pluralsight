using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConferenceTrackerTests.Helpers
{
    public class TestHelpers
    {
        private static readonly string _directory = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "ConferenceTracker" + Path.DirectorySeparatorChar;

        public static string GetUserCode(string fileName)
        {
            string userCode = "";
            using (FileStream fs = new FileStream(_directory + fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    String line = sr.ReadToEnd();
                    userCode += line;
                }
                return userCode;
            }
        }

        public static SyntaxNode GetUserAst(String path)
        {
            String fileContents = File.ReadAllText(_directory + path);
            return SyntaxFactory.ParseCompilationUnit(fileContents);
        }

        public static IEnumerable<AssignmentExpressionSyntax> GetAssignments(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<AssignmentExpressionSyntax>();
        }

        public static IEnumerable<FieldDeclarationSyntax> GetFields(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<FieldDeclarationSyntax>();
        }

        public static IEnumerable<ConstructorDeclarationSyntax> GetConstructors(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<ConstructorDeclarationSyntax>();
        }

        public static IEnumerable<MethodDeclarationSyntax> GetMethods(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<MethodDeclarationSyntax>();
        }

        public static IEnumerable<VariableDeclarationSyntax> GetVariables(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<VariableDeclarationSyntax>();
        }

        public static IEnumerable<IfStatementSyntax> GetIfStatements(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<IfStatementSyntax>();
        }

        public static IEnumerable<ElseClauseSyntax> GetElseClauses(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<ElseClauseSyntax>();
        }

        public static IEnumerable<ReturnStatementSyntax> GetReturnStatements(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<ReturnStatementSyntax>();
        }

        public static IEnumerable<TryStatementSyntax> GetTryStatements(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<TryStatementSyntax>();
        }

        public static IEnumerable<OrderByClauseSyntax> GetOrderByClauses(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<OrderByClauseSyntax>();
        }

        public static IEnumerable<GroupClauseSyntax> GetGroupClauses(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<GroupClauseSyntax>();
        }
        public static IEnumerable<SelectClauseSyntax> GetSelectClauses(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<SelectClauseSyntax>();
        }

        public static IEnumerable<ForEachStatementSyntax> GetForEachStatements(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<ForEachStatementSyntax>();
        }

        public static IEnumerable<ExpressionStatementSyntax> GetExpressions(SyntaxNode ast)
        {
            return ast.DescendantNodes().OfType<ExpressionStatementSyntax>();
        }
    }
}
