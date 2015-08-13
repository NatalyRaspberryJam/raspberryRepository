using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SBRte;
using SBLogon;
using Tenders.ui;
using System.Threading;

namespace Tenders.handlers
{
  static public class getDirectumTempFilesHandler
  {
    static public void getDirectumFiles()
    {
      string temp = createTempFolder();
        getFilesFromDirectum(temp);      
          parseDocxHandler.setDocFolder(temp);
          deleteTempFolder(temp);   
    }

    static private void getFilesFromDirectum(string temp)
    {
 	    var connectionParams = "SystemCode=DIRECTUM";
      SBLogon.LoginPoint logP = new SBLogon.LoginPoint();
      SBRte.Application dirApp = logP.GetApplication(connectionParams);
        IScriptFactory scriptFactory = dirApp.ScriptFactory;
        SBRte.IList directumDocs = scriptFactory.ExecuteByName("_getSpecialDocuments");  //вызов сценария получения файлов
      IEDocumentFactory docFactory = dirApp.EDocumentFactory;

      while (directumDocs.EOF == false)
      {
        //последняя версия дока
        var doc = docFactory.GetObjectByID(directumDocs.Value.ID);
        var versions = doc.versions;
        int versionNum = versions.Count;
          //куда сохранить
          string fileName = docFactory.GetObjectByID(directumDocs.Value.ID).Requisites("ISBEDocName").DisplayText;
          string saveFileDirectory = temp + "\\" + fileName + ".docx";
          docFactory.GetObjectByID(directumDocs.Value.ID).Export(versionNum, @saveFileDirectory, false, false, false, TExportedSignaturesType.estAll);
        directumDocs.Next();
      }
    }

    static private string createTempFolder()  //temp folder
    {
      string userDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
      string tempFolder = Path.Combine(userDirectory, "tempDirectumFolder");
        Directory.CreateDirectory(tempFolder);
      return tempFolder;
    }

    static private void deleteTempFolder(string folder)
    {
      Directory.Delete(folder, true);
    }

  }
}
