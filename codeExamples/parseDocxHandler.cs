using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Novacode;
using Tenders.handlers;

namespace Tenders.handlers
{
  static public class parseDocxHandler
  {
    static public void setDocFolder(string folder)
    {
      string[] files = Directory.GetFiles(folder);
        foreach (string path in files)
        {
          DocX file = DocX.Load(@path);
          parseFile(file);
        }
    }

    static private void parseFile(DocX file)
    {
      string initiator = file.CustomProperties.Where(x => x.Value.Name == "ФИО инициатора").First().Value.Value.ToString();
      string phone = file.CustomProperties.Where(x => x.Value.Name == "Телефон инициатора").First().Value.Value.ToString();
      string date = file.CustomProperties.Where(x => x.Value.Name == "Текущая дата").First().Value.Value.ToString();
      string bankroll = file.CustomProperties.Where(x => x.Value.Name == "Средства").First().Value.Value.ToString();
      string department = file.CustomProperties.Where(x => x.Value.Name == "Р*Подразделение...*Наименование").First().Value.Value.ToString();
      string content = file.CustomProperties.Where(x => x.Value.Name == "Аннотация").First().Value.Value.ToString();
      string financing = file.CustomProperties.Where(x => x.Value.Name == "Финансирование").First().Value.Value.ToString();

        List<item> items = new List<item>();
          var equipmentTable = file.Tables[0];
            for (int i = 1; i < equipmentTable.Rows.Count; i++)
            {
              items.Add(manageNotesHandler.createDirItem(equipmentTable.Rows[i].Cells[0].Paragraphs.First().Text, Convert.ToInt32(equipmentTable.Rows[i].Cells[1].Paragraphs.First().Text), equipmentTable.Rows[i].Cells[2].Paragraphs.First().Text));
            }
        manageNotesHandler.addDirNote(content, department, initiator, phone, date, bankroll, financing, items);      
    }
  }
}
