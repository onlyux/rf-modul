/*
' Copyright (c) 2025 Alberti Adam
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

//using System.Xml;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;
using System.Collections.Generic;

namespace Tarhej.Dnn.Tarhej.Dnn.ContactModule.Components
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Controller class for Tarhej.Dnn.ContactModule
    /// 
    /// The FeatureController class is defined as the BusinessController in the manifest file (.dnn)
    /// DotNetNuke will poll this class to find out which Interfaces the class implements. 
    /// 
    /// The IPortable interface is used to import/export content from a DNN module
    /// 
    /// The ISearchable interface is used by DNN to index the content of a module
    /// 
    /// The IUpgradeable interface allows module developers to execute code during the upgrade 
    /// process for a module.
    /// 
    /// Below you will find stubbed out implementations of each, uncomment and populate with your own data
    /// </summary>
    /// -----------------------------------------------------------------------------

    //uncomment the interfaces to add the support.
    public class FeatureController //: IPortable, ISearchable, IUpgradeable
    {


        #region Optional Interfaces

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ExportModule implements the IPortable ExportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be exported</param>
        /// -----------------------------------------------------------------------------
        //public string ExportModule(int ModuleID)
        //{
        //string strXML = "";

        //List<Tarhej.Dnn.ContactModuleInfo> colTarhej.Dnn.ContactModules = GetTarhej.Dnn.ContactModules(ModuleID);
        //if (colTarhej.Dnn.ContactModules.Count != 0)
        //{
        //    strXML += "<Tarhej.Dnn.ContactModules>";

        //    foreach (Tarhej.Dnn.ContactModuleInfo objTarhej.Dnn.ContactModule in colTarhej.Dnn.ContactModules)
        //    {
        //        strXML += "<Tarhej.Dnn.ContactModule>";
        //        strXML += "<content>" + DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(objTarhej.Dnn.ContactModule.Content) + "</content>";
        //        strXML += "</Tarhej.Dnn.ContactModule>";
        //    }
        //    strXML += "</Tarhej.Dnn.ContactModules>";
        //}

        //return strXML;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// ImportModule implements the IPortable ImportModule Interface
        /// </summary>
        /// <param name="ModuleID">The Id of the module to be imported</param>
        /// <param name="Content">The content to be imported</param>
        /// <param name="Version">The version of the module to be imported</param>
        /// <param name="UserId">The Id of the user performing the import</param>
        /// -----------------------------------------------------------------------------
        //public void ImportModule(int ModuleID, string Content, string Version, int UserID)
        //{
        //XmlNode xmlTarhej.Dnn.ContactModules = DotNetNuke.Common.Globals.GetContent(Content, "Tarhej.Dnn.ContactModules");
        //foreach (XmlNode xmlTarhej.Dnn.ContactModule in xmlTarhej.Dnn.ContactModules.SelectNodes("Tarhej.Dnn.ContactModule"))
        //{
        //    Tarhej.Dnn.ContactModuleInfo objTarhej.Dnn.ContactModule = new Tarhej.Dnn.ContactModuleInfo();
        //    objTarhej.Dnn.ContactModule.ModuleId = ModuleID;
        //    objTarhej.Dnn.ContactModule.Content = xmlTarhej.Dnn.ContactModule.SelectSingleNode("content").InnerText;
        //    objTarhej.Dnn.ContactModule.CreatedByUser = UserID;
        //    AddTarhej.Dnn.ContactModule(objTarhej.Dnn.ContactModule);
        //}

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetSearchItems implements the ISearchable Interface
        /// </summary>
        /// <param name="ModInfo">The ModuleInfo for the module to be Indexed</param>
        /// -----------------------------------------------------------------------------
        //public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems(DotNetNuke.Entities.Modules.ModuleInfo ModInfo)
        //{
        //SearchItemInfoCollection SearchItemCollection = new SearchItemInfoCollection();

        //List<Tarhej.Dnn.ContactModuleInfo> colTarhej.Dnn.ContactModules = GetTarhej.Dnn.ContactModules(ModInfo.ModuleID);

        //foreach (Tarhej.Dnn.ContactModuleInfo objTarhej.Dnn.ContactModule in colTarhej.Dnn.ContactModules)
        //{
        //    SearchItemInfo SearchItem = new SearchItemInfo(ModInfo.ModuleTitle, objTarhej.Dnn.ContactModule.Content, objTarhej.Dnn.ContactModule.CreatedByUser, objTarhej.Dnn.ContactModule.CreatedDate, ModInfo.ModuleID, objTarhej.Dnn.ContactModule.ItemId.ToString(), objTarhej.Dnn.ContactModule.Content, "ItemId=" + objTarhej.Dnn.ContactModule.ItemId.ToString());
        //    SearchItemCollection.Add(SearchItem);
        //}

        //return SearchItemCollection;

        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// UpgradeModule implements the IUpgradeable Interface
        /// </summary>
        /// <param name="Version">The current version of the module</param>
        /// -----------------------------------------------------------------------------
        //public string UpgradeModule(string Version)
        //{
        //	throw new System.NotImplementedException("The method or operation is not implemented.");
        //}

        #endregion

    }

}
