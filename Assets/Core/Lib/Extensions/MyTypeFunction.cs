namespace Lib
{
    internal static class IdStore
    {
        public static int IdCount;
    }

    public static class MyTypeFunction<T>
    {
        private static int _id;
        private static bool _isInitialized;

        public static int GetRuntimeID()
        {
            if (_isInitialized)
                return _id;
            
            _id = IdStore.IdCount++;
            _isInitialized = true;
            return _id;
        }
    }
}