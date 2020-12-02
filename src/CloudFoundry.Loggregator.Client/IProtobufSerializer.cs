namespace CloudFoundry.Loggregator.Client
{
    public interface IProtobufSerializer
    {
        ApplicationLog DeserializeApplicationLog(byte[] data);
    }
}
