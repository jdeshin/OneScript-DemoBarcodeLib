using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.Binary;

using ScriptEngine;
using ScriptEngine.Environment;
using ScriptEngine.HostedScript;

namespace DemoBarcodeLib
{
    [ContextClass("ДемоГенераторШтрихкодов", "DemoBarcodeGenerator")]
    public class DemoBarcodeGenerator : AutoContext<DemoBarcodeGenerator>
    {
        public DemoBarcodeGenerator()
        {

        }

        // Метод платформы
        [ContextMethod("Создать", "Create")]
        public IValue Create()
        {
            // Создаем объект из модуля объекта

            return new DemoBarcodeLibImpl();
        }
    }

    [ContextClass("ДемоГенераторШтрихкодовОбъект", "DemoBarcodeGeneratorObject")]
    public class DemoBarcodeLibImpl : AutoContext<DemoBarcodeLibImpl>
    {
        BarcodeLib.Barcode _barcode; 

        public DemoBarcodeLibImpl()
        {
            _barcode = new BarcodeLib.Barcode();
            _barcode.IncludeLabel = true;
            _barcode.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER;
            _barcode.RotateFlipType = System.Drawing.RotateFlipType.RotateNoneFlipNone;
            _barcode.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            _barcode.ForeColor = System.Drawing.Color.Black;
            _barcode.BackColor = System.Drawing.Color.White;
        }

        [ContextMethod("СгенерироватьШтрихкод", "GenerateBarcode")]
        public void Add(string text, int width, int height)
        {
            _barcode.Encode(BarcodeLib.TYPE.CODE128C, text, width, height);
            if (_barcode.Errors.Count > 0)
            {
                throw new Exception(_barcode.Errors[0]);
            }
        }

        [ContextMethod("СохранитьИзображение", "SaveImage")]
        public void Save(string fileName)
        {
            _barcode.SaveImage(fileName, BarcodeLib.SaveTypes.PNG);
        }
    }
}
