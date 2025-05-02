using APIDeveloperCodingCore.POCO;

namespace APIDeveloperCodingCore.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        Task<T?> GetAsync<T>(HttpRequest httpRequest) where T : ServiceResponse, new();
    }
}
