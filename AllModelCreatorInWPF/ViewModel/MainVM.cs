using Autodesk.Navisworks.Api.Automation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AllModelCreatorInWPF
{
    /// <summary>
    /// The data context of the M-V-VM application. 
    /// </summary>
    public class MainVM : BaseVM
    {
        public bool IsConnectedToLicenseServer { get { return TestLicenseServerConnection(); } }
        /// <summary>
        /// The list of folders in the specified directory.
        /// </summary>
        public ObservableCollection<TradePartnerFolderVM> TradePartnerFolders { get; set; }

        /// <summary>
        /// Command bound to the RunNavisworksAutomationMethod.
        /// </summary>
        public ICommand RunNavisworksAutomation { get; set; }

        /// <summary>
        /// Command bound to the refresh folders method.
        /// </summary>
        public ICommand RefreshFolders { get; set; }

        /// <summary>
        /// Base Constructor
        /// </summary>
        public MainVM()
        {
            // Set the commands to their corresponding methods.
            RunNavisworksAutomation = new RelayCommand(RunNavisworksAutomationMethod);
            RefreshFolders = new RelayCommand(RefreshFoldersMethod);

            // Enumerate the current folder. The current folder is stored in settings.
            EnumerateCurrentFolder();
        }

        /// <summary>
        /// Enumerates the folder specified in the application settings to find each trade partner folder.
        /// </summary>
        private void EnumerateCurrentFolder()
        {
            // Instantiate the collection.
            TradePartnerFolders = new ObservableCollection<TradePartnerFolderVM>();

            // Enumerate the specified folder. 
            foreach (string directory in Directory.EnumerateDirectories(Properties.Settings.Default.InputRootFolder))
            {
                // If the folder name contains the settings stored name qualifier "_".
                if (directory.Contains(Properties.Settings.Default.NWDFolderQualifier))
                {
                    TradePartnerFolders.Add(new TradePartnerFolderVM(directory));
                }
            }

            // Sort the list
            TradePartnerFolders = new ObservableCollection<TradePartnerFolderVM>(TradePartnerFolders.OrderBy(i => i.DirectoryName));
        }

        /// <summary>
        /// Test if the user is connected to the VPN/License server.
        /// </summary>
        /// <returns></returns>
        private bool TestLicenseServerConnection()
        {
            using (var ping = new System.Net.NetworkInformation.Ping())
            {
                // Ping the license server
                var result = ping.Send(Properties.Settings.Default.LicenseServer);

                // Return true if the ping is successful.
                if (result.Status == System.Net.NetworkInformation.IPStatus.Success) return true;

                return false;
            }
        }

        /// <summary>
        /// Refresh the view if there were changes to the underlying folder structure.
        /// </summary>
        private void RefreshFoldersMethod()
        {
            EnumerateCurrentFolder();
        }

        /// <summary>
        /// Runs the navisworks automated publishing.
        /// </summary>
        private void RunNavisworksAutomationMethod()
        {
            foreach (TradePartnerFolderVM folderVM in TradePartnerFolders)
            {
                // Logic that gets the folder prefix ie 01_ and body ie Architecture. 
                string folderPrefix = folderVM.DirectoryName.Substring(0, 3);
                string folderBody = folderVM.DirectoryName.Substring(3);

                // String-formatted output .nwd file name.
                string nwdName = $"{Properties.Settings.Default.OutputFolder}{folderPrefix}US-MFA-BV0-{folderBody}.nwd";

                // If the folder contains only 1 nwd, copy it to the output folder.
                if (folderVM.IncludedNWDs == 1 && folderVM.IncludedNWCs == 0)
                {
                    // Copy the .nwd to the output folder
                    File.Copy(folderVM.TradePartnerModels.First().ModelName, nwdName, true);

                    // Notify the UI that the update is successful
                    folderVM.UpdateStatus = UpdateStatus.Succeeded;
                }
                // Open a new nwd and append in each nwc, then save it.
                else
                {
                    // Tries to save the file 5 times and if the user is connected to the license server, if that fails then skip the file.
                    while (folderVM.FailedWriteAttempts < 5 && IsConnectedToLicenseServer)
                    {
                        try
                        {
                            // Create a new .nwd where a .nwc has been changed
                            using (NavisworksApplication navisworks = new NavisworksApplication())
                            {
                                // Do not show that pesky progress bar when completing the automation. 
                                navisworks.DisableProgress();

                                // Append each nwc
                                foreach (TradePartnerModelVM modelVM in folderVM.TradePartnerModels)
                                {
                                    navisworks.AppendFile(modelVM.ModelFilePath);
                                }

                                // Save the .nwd
                                navisworks.SaveFile(nwdName);

                                // Notify the UI that the update is successful
                                folderVM.UpdateStatus = UpdateStatus.Succeeded;

                                // Re-enable progress when the update is complete.
                                navisworks.EnableProgress();
                            }
                        }
                        catch (Exception)
                        {
                            // Add a failed write attempt to the counter
                            folderVM.FailedWriteAttempts++;
                        }
                    }
                }
            }
        }
    }
}
