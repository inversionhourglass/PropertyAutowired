namespace TestImplAssembly.Logging
{
    public class LogFactory
    {
        public static ILogger GetLogger(string name)
        {
            return new DefaultLogger(name);
        }
    }
}
