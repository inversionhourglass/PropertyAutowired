namespace TestImplAssembly.Logging
{
    public class DefaultLogger : ILogger
    {
        public DefaultLogger(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
