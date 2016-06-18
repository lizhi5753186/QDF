using System.Collections.Generic;
using System.Reflection;

namespace QDF.Reflection
{
    /// <summary>
    /// This interface is used to get all assemblies to 
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// This method is used to get all assemblies used by application.
        /// </summary>
        /// <returns>List of assemblies</returns>
        List<Assembly> GetAllAssemblies(); 
    }
}