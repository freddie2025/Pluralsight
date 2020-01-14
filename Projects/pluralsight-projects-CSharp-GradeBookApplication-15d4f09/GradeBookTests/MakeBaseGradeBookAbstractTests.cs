using GradeBook.GradeBooks;
using Xunit;

namespace GradeBookTests
{
    public class MakeBaseGradeBookAbstractTests
    {
        /// <summary>
        ///     All tests related to the "Make BaseGradeBook Abstract" Task.
        /// </summary>
        [Fact(DisplayName = "Make BaseGradeBook Abstract @make-basegradebook-abstract")]
        public void MakeBaseGradeBookAbstract()
        {
            // Test if `BaseGradeBook` is abstract.
            Assert.True(typeof(BaseGradeBook).IsAbstract == true, "`GradeBook.GradeBooks.BaseGradeBook` is not abstract.");
        }
    }
}
