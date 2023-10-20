namespace ConfigurationsTestRepo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
#if Android
            Assert.Pass();
#endif
#if Windows
            Assert.Fail();
#endif
        }
    }
}