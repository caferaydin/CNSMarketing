namespace CNSMarketing.Service.Abstraction.ExternalService
{
    public interface IServiceManager<T>
    {
        Task<T> GetAsync(string url, Dictionary<string, string> headers = null);
        Task<T> PostAsync(string url, object data, Dictionary<string, string> headers = null);
        Task<T> PostFormEncodedResult(IEnumerable<KeyValuePair<string, string>> data, string url, Dictionary<string, string> headers);
        Task<T> PutAsync(string url, object data, Dictionary<string, string> headers = null);
        Task<bool> DeleteAsync(string url, Dictionary<string, string> headers = null);
    }
}
