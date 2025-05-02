using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using APIDeveloperCodingCore.POCO;

namespace APIDeveloperCodingCore.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDetailStoryBussines
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configurationServiceStory"></param>
        /// <param name="configurationServiceDetail"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        Task<T?> GetDetailStory<T>(ConfigurationService configurationServiceStory, ConfigurationService configurationServiceDetail, int limit);
    }
}
