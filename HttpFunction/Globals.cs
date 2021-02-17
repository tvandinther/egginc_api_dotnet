using System;

namespace HttpFunction
{
    public struct Globals
    {
        public static readonly uint ClientVersion;
        public static readonly string DeviceId;
        public static readonly string UserId;

        static Globals()
        {
            var clientVersion = Environment.GetEnvironmentVariable("CLIENT_VERSION");
            var deviceId = Environment.GetEnvironmentVariable("DEVICE_ID");
            var userId = Environment.GetEnvironmentVariable("USER_ID");

            if (clientVersion is null && deviceId is null)
            {
                throw new Exception("Environment variables unset: CLIENT_VERSION, DEVICE_ID");
            }
            if (clientVersion is null)
            {
                throw new Exception("Environment variables unset: CLIENT_VERSION");
            }
            if (deviceId is null)
            {
                throw new Exception("Environment variables unset: DEVICE_ID");
            }
            
            ClientVersion = uint.Parse(clientVersion);
            DeviceId = deviceId;
            UserId = userId;
        }
    }
}