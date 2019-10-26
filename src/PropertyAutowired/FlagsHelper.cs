namespace PropertyAutowired
{
    /// <summary>
    /// for note, can not using when tag a attribute
    /// </summary>
    public static class FlagsHelper
    {
        /// <summary>
        /// All Property
        /// </summary>
        public static PropFlags[] All => new[]
        {
            PropFlags.Public,
            PropFlags.NonPublic
        };

        /// <summary>
        /// Public properties, whatever static or instance
        /// </summary>
        public static PropFlags[] Public => new[]
        {
            PropFlags.Public
        };

        /// <summary>
        /// NonPublic properties, whatever static or instance
        /// </summary>
        public static PropFlags[] NonPublic => new[]
        {
            PropFlags.NonPublic
        };

        /// <summary>
        /// Instance properties, whatever public or nonpublic
        /// </summary>
        public static PropFlags[] Instance => new[]
        {
            PropFlags.Instance
        };

        /// <summary>
        /// Static properties, whatever public or nonpublic
        /// </summary>
        public static PropFlags[] Static => new[]
        {
            PropFlags.Static
        };

        /// <summary>
        /// Public instance properties
        /// </summary>
        public static PropFlags[] InstancePublic => new[]
        {
            PropFlags.InstancePublic
        };

        /// <summary>
        /// NonPublic instance properties
        /// </summary>
        public static PropFlags[] InstanceNonPublic => new[]
        {
            PropFlags.InstanceNonPublic
        };

        /// <summary>
        /// Public static properties
        /// </summary>
        public static PropFlags[] StaticPublic => new[]
        {
            PropFlags.StaticPublic
        };

        /// <summary>
        /// NonPublic static properties
        /// </summary>
        public static PropFlags[] StaticNonPublic => new[]
        {
            PropFlags.StaticNonPublic
        };

        /// <summary>
        /// Public instance properties and nonPublic static properties
        /// </summary>
        public static PropFlags[] InstancePublicAndStaticNonPublic => new[]
        {
            PropFlags.InstancePublic,
            PropFlags.StaticNonPublic
        };

        /// <summary>
        /// NonPublic instance properties and public static properties
        /// </summary>
        public static PropFlags[] InstanceNonPublicAndStaticPublic => new[]
        {
            PropFlags.InstanceNonPublic,
            PropFlags.StaticPublic
        };

        /// <summary>
        /// Except nonPublic static properties
        /// </summary>
        public static PropFlags[] PublicAndInstance => new[]
        {
            PropFlags.Public,
            PropFlags.Instance
        };

        /// <summary>
        /// Except nonPublic instance properties
        /// </summary>
        public static PropFlags[] PublicAndStatic => new[]
        {
            PropFlags.Public,
            PropFlags.Static
        };

        /// <summary>
        /// Except public static properties
        /// </summary>
        public static PropFlags[] NonPublicAndInstance => new[]
        {
            PropFlags.NonPublic,
            PropFlags.Instance
        };

        /// <summary>
        /// Except public instance properties
        /// </summary>
        public static PropFlags[] NonPublicAndStatic => new[]
        {
            PropFlags.NonPublic,
            PropFlags.Static
        };
    }
}
