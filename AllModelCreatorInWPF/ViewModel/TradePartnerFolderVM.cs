using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllModelCreatorInWPF
{
    /// <summary>
    /// VM for each trade partner folder.
    /// </summary>
    public class TradePartnerFolderVM : BaseVM
    {
        /// <summary>
        /// The directory info for the given trade partner folder.
        /// </summary>
        private DirectoryInfo _directoryInfo;

        /// <summary>
        /// The name of the folder.
        /// </summary>
        public string DirectoryName { get { return _directoryInfo.Name; } }

        /// <summary>
        /// Returns the count of navisworks documents included in this folder.
        /// </summary>
        public int IncludedNWDs { get { return TradePartnerModels.Where(model => model.ModelType == NavisworksModelType.NWD).Count(); } }

        /// <summary>
        /// Returns the count of navisworks cache files in this folder.
        /// </summary>
        public int IncludedNWCs { get { return TradePartnerModels.Where(model => model.ModelType == NavisworksModelType.NWC).Count(); } }

        /// <summary>
        /// Returns the count of models that have been changed since the threshold date.
        /// </summary>
        public int ChangedModles { get;}

        /// <summary>
        /// Stores the number of times that Navisworks has failed to save the current file.
        /// </summary>
        public int FailedWriteAttempts { get; set; }

        /// <summary>
        /// Notifies the UI if the model update is successful.
        /// </summary>
        public UpdateStatus UpdateStatus { get; set; }

        /// <summary>
        /// The list of models within the folder
        /// </summary>
        public ObservableCollection<TradePartnerModelVM> TradePartnerModels { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="directory">The directory to enumerate and finde .nwc's within</param>
        public TradePartnerFolderVM(string directory)
        {
            // Set the directory info this class will reference.
            _directoryInfo = new DirectoryInfo(directory);

            EnumerateCurrentFolder();
        }

        /// <summary>
        /// Enumerates the folder specified in the application settings to find each trade partner folder.
        /// </summary>
        private void EnumerateCurrentFolder()
        {
            // Instantiate the collection.
            TradePartnerModels = new ObservableCollection<TradePartnerModelVM>();

            // Enumerate the specified folder to find nwcs. 
            foreach (string modelDirectory in Directory.GetFiles(_directoryInfo.FullName,"*.nwc", SearchOption.TopDirectoryOnly))
            {
                TradePartnerModels.Add(new TradePartnerModelVM(modelDirectory, NavisworksModelType.NWC));
            }

            // Enumerate the specified folder to find nwds.
            foreach (string modelDirectory in Directory.GetFiles(_directoryInfo.FullName, "*.nwd", SearchOption.TopDirectoryOnly))
            {
                TradePartnerModels.Add(new TradePartnerModelVM(modelDirectory, NavisworksModelType.NWD));
            }

        }
    }
}
