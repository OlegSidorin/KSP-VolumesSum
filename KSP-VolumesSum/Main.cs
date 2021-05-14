namespace KSP_VolumesSum
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Windows.Media.Imaging;
    using Autodesk.Revit.UI;
    using Autodesk.Revit.DB;
    using adWin = Autodesk.Windows;
    using System.Resources;

    class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            adWin.RibbonControl ribbon = adWin.ComponentManager.Ribbon;
            
            //List<RibbonPanel> panels = application.GetRibbonPanels();
            string str = "";
            //foreach (var pan in panels)
            //{
            //    str += pan.Name + Environment.NewLine;
            //}

            adWin.RibbonTab modifyTab = ribbon.Tabs.Where(xxx => xxx.Id == "Modify").FirstOrDefault();
            adWin.RibbonPanel geometryPanel = modifyTab.Panels.Where(xxx => xxx.Source.Id == "geometry_shr").FirstOrDefault();

            adWin.RibbonButton button = new adWin.RibbonButton();

            //var SumData = new PushButtonData("SumData", "Складывает\nобъемы", Assembly.GetExecutingAssembly().Location, "KSP_VolumesSum.VSum")
            //{
            //    ToolTipImage = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png")),
            //    ToolTip = "Суммирует объемы элементов модели, если они есть"
            //};
            //PushButton SumDataBtn = panelAnnotation.AddItem(SumData) as PushButton;
            //SumDataBtn.LargeImage = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png"));


            button.Name = "VSumButton";
            button.Image = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-16.png"));
            button.LargeImage = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png"));
            button.Id = "ID_VSumButton";
            button.AllowInStatusBar = true;
            button.AllowInToolBar = true;
            button.GroupLocation = Autodesk.Private.Windows.RibbonItemGroupLocation.Last;
            button.IsEnabled = true;
            button.IsToolTipEnabled = true;
            button.IsVisible = true;
            button.ShowImage = true; //  true;
            button.ShowText = false;
            button.ShowToolTipOnDisabled = true;
            button.Text = "Подсчет объемов";
            button.ToolTip = "Считает объемы выделенных элементов";
            //                button.MinHeight = 0;
            //                button.MinWidth = 0;
            button.Size = adWin.RibbonItemSize.Standard;
            button.ResizeStyle = adWin.RibbonItemResizeStyles.HideText;
            button.IsCheckable = true;
            button.KeyTip = "KEYVSUM";

            geometryPanel.Source.Items.Add(button);

            //adWin.ComponentManager.UIElementActivated +=
            //    new EventHandler<adWin.UIElementActivatedEventArgs>(CommandForButton);

            //foreach (adWin.RibbonTab tab in ribbon.Tabs) 
            //{
            //    foreach (adWin.RibbonPanel panel in tab.Panels)
            //    {
            //        str += tab.Id + " : " + panel.Source.Id + Environment.NewLine;
            //    }
            //    str += tab.Id + Environment.NewLine;
            //}

            //str += " -----> " + modifyTab.Id + geometryPanel.Source.Id;

            //TaskDialog.Show("123", str);
            //string tabName = "КСП_1";
            //string panelAnnotationName = "Методы";
            //application.CreateRibbonTab();

            //RibbonPanel panelAnnotation = application.CreateRibbonPanel("Изменить", "Посчитать");

            //var SumData = new PushButtonData("SumData", "Складывает\nобъемы", Assembly.GetExecutingAssembly().Location, "KSP_VolumesSum.VSum")
            //{
            //    ToolTipImage = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png")),
            //    ToolTip = "Суммирует объемы элементов модели, если они есть"
            //};
            //var SumDataBtn = panelAnnotation.AddItem(SumData) as PushButton;
            //SumDataBtn.LargeImage = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png"));






            //foreach (adWin.RibbonTab tab in ribbon.Tabs)
            //{
            //    if (tab.Name == "Архитектура")
            //    {
            //        foreach (adWin.RibbonPanel panel
            //          in tab.Panels)
            //        {
            //            if (panel.Source.Name == "Строительство")
            //            {
            //                adWin.RibbonButton button
            //                  = new adWin.RibbonButton();

            //                button.Name = "TbcButtonName";
            //                button.Image = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png"));
            //                button.LargeImage = new BitmapImage(new Uri(@"C:\Users\Sidorin_O\source\repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png"));
            //                button.Id = "ID_TBC_BUTTON";
            //                button.AllowInStatusBar = true;
            //                button.AllowInToolBar = true;
            //                button.GroupLocation = Autodesk.Private
            //                  .Windows.RibbonItemGroupLocation.Middle;
            //                button.IsEnabled = true;
            //                button.IsToolTipEnabled = true;
            //                button.IsVisible = true;
            //                button.ShowImage = true; //  true;
            //                button.ShowText = true;
            //                button.ShowToolTipOnDisabled = true;
            //                button.Text = "The Building Coder";
            //                button.ToolTip = "Open The Building "
            //                  + "Coder blog on the Revit API";
            //                button.MinHeight = 0;
            //                button.MinWidth = 0;
            //                button.Size = adWin.RibbonItemSize.Large;
            //                button.ResizeStyle = adWin.RibbonItemResizeStyles.HideText;
            //                button.IsCheckable = true;
            //                button.KeyTip = "TBC";


            //                adWin.ComponentManager.UIElementActivated += 
            //                    new EventHandler<adWin.UIElementActivatedEventArgs>(CommandForButton);

            //                return Result.Succeeded;
            //            }
            //        }
            //    }
            //}


            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        void CommandForButton(object sender, adWin.UIElementActivatedEventArgs e)
        {
            if (e != null
              && e.Item != null
              && e.Item.Id != null
              && e.Item.Id == "ID_VSumButton")
            {
                // Perform the button action

                // Local file

                string path = Assembly.GetExecutingAssembly().Location;
                path = "KSP_VolumesSum.VSum";

                // не понятно как создать метод

                //TaskDialog.Show("123", "Ghbdtn");



                //path = Path.Combine(
                //  Path.GetDirectoryName(path),
                //  "test.html");

                //// Internet URL

                //path = "http://thebuildingcoder.typepad.com";

                //Process.Start(path);
            }
        }
    }
}
