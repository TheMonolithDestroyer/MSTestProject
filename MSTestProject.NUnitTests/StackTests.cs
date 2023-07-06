namespace MSTestProject.NUnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Fundamentals.Stack<string> _stack;
        [SetUp]
        public void SetUp()
        {
            _stack = new Fundamentals.Stack<string>();
        }

        [Test]
        public void Push_NullArgument_ThrowsArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArgument_AddTheObjectToTheStack()
        {
            _stack.Push("1");

            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_StaskIsEmpty_ReturnsZero()
        {
            var result = _stack.Count;

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackContainsFewObjects_ReturnsObjestOnTheTop()
        {
            _stack.Push("1");
            _stack.Push("2");

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void Pop_StackContainsFewObjects_RemovesObjestOnTheTop()
        {
            _stack.Push("1");
            _stack.Push("2");
            
            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Peek_WhenStackIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackContainsFewObject_ReturnsObjestOnTheTop()
        {
            _stack.Push("1");
            _stack.Push("2");

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo("2"));
        }

        [Test]
        public void Peek_StackContainsFewObject_DoestNotRemoveObjectOnTheTop()
        {
            _stack.Push("1");
            _stack.Push("2");

            _stack.Peek();

            Assert.That(_stack.Count, Is.EqualTo(2));
        }
    }
}
