using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using MyGameApplication.Item;

namespace MyGameApplication {
	public class ItemList_importer : AssetPostprocessor {
		private static readonly string filePath = "Assets/Terasurware/ExcelData/ItemList.xlsx";
		private static readonly string exportPath = "Assets/Terasurware/ExcelData/ItemList.asset";
		private static readonly string[] sheetNames = { "Prop", };

		static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
			foreach (string asset in importedAssets) {
				if (!filePath.Equals(asset))
					continue;

				ItemInfo data = (ItemInfo)AssetDatabase.LoadAssetAtPath(exportPath, typeof(ItemInfo));
				if (data == null) {
					data = ScriptableObject.CreateInstance<ItemInfo>();
					AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
					data.hideFlags = HideFlags.NotEditable;
				}

				data.sheets.Clear();
				using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
					IWorkbook book = null;
					if (Path.GetExtension(filePath) == ".xls") {
						book = new HSSFWorkbook(stream);
					}
					else {
						book = new XSSFWorkbook(stream);
					}

					foreach (string sheetName in sheetNames) {
						ISheet sheet = book.GetSheet(sheetName);
						if (sheet == null) {
							Debug.LogError("[QuestData] sheet not found:" + sheetName);
							continue;
						}

						ItemInfo.Sheet s = new ItemInfo.Sheet();
						s.name = sheetName;

						s.list.Add(null);
						for (int i = 2; i <= sheet.LastRowNum; i++) {
							IRow row = sheet.GetRow(i);
							ICell cell = null;

							Prop p = new Prop();

							cell = row.GetCell(0); p.id = (int)(cell == null ? 0 : cell.NumericCellValue);
							cell = row.GetCell(1); p.name = (cell == null ? "" : cell.StringCellValue);
							cell = row.GetCell(2); p.description = (cell == null ? "" : cell.StringCellValue);
							cell = row.GetCell(3); if (cell != null) p.capacity = (int)cell.NumericCellValue;
							cell = row.GetCell(4); p.consumable = (cell == null ? true : cell.BooleanCellValue);
							cell = row.GetCell(5); p.isCarItem = (cell == null ? false : cell.BooleanCellValue);
							s.list.Add(p);
						}
						data.sheets.Add(s);
					}
				}

				ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
				EditorUtility.SetDirty(obj);
			}
		}
	}
}
