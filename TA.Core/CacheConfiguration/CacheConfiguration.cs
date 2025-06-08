namespace TA.Core.CacheConfiguration
{
    public static class CacheConfiguration
    {
        public static int DefaultExpirationInMinutes { get; set; } = 1;
        public static bool EnableCacheLogging { get; set; } = false;
        public static int MaxCacheSize { get; set; } = 1024; 
    
        public static DateTime AbsoluteExpiration { get; set; } = DateTime.Now.AddMinutes(DefaultExpirationInMinutes);
        public static TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(DefaultExpirationInMinutes);
}
}
