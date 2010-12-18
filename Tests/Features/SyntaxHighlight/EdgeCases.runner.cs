using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Features.SyntaxHighlight 
{
    [TestClass]
    public partial class TestHiglightingEdgeCases 
    {
        
        [TestMethod]
        public void KeywordsAndCommentsInMultilineArgDisplayLikeArg()
        {         
            Given_the_Feature_contains(
@"
""
Scenario: Name
// Single Line Comment
/*
MultiLine Comment
*/
""
");        
            Raconteur_should_highlight_like_a("String", 
@"
""
Scenario: Name
// Single Line Comment
/*
MultiLine Comment
*/
""
");        
            Raconteur_should_not_highlight("Scenario:");        
            Raconteur_should_not_highlight("// Single Line Comment");        
            Raconteur_should_not_highlight(
@"
/*
MultiLine Comment
*/
");
        }

    }
}
