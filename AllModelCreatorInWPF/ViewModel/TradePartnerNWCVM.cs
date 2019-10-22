using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllModelCreatorInWPF
{
    /// <summary>
    /// VM for each trade partner model, ie NWD or NWC.
    /// </summary>
    public class TradePartnerModelVM : BaseVM
    {
        /// <summary>
        /// The directory info for the given trade partner model.
        /// </summary>
        private DirectoryInfo _directoryInfo;

        /// <summary>
        /// Name of the .nwc
        /// </summary>
        public string ModelName { get { return _directoryInfo.Name; } }

        /// <summary>
        /// Filepath of the .nwc
        /// </summary>
        public string ModelFilePath { get { return _directoryInfo.FullName; } }

        /// <summary>
        /// The type of the current navisworks model.
        /// </summary>
        public NavisworksModelType ModelType { get; private set; }

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="modelDirectory">Directory of this model</param>
        public TradePartnerModelVM(string modelDirectory, NavisworksModelType modelType)
        {
            // Set the directory info this class will reference.
            _directoryInfo = new DirectoryInfo(modelDirectory);

            ModelType = modelType;
        }
    }
}
