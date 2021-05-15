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
    using System.IO;

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    //[Autodesk.Revit.DB.Macros.AddInId("4378B252-DA00-4CE1-9462-4F3F81B644DD")]
    public class Main : IExternalApplication
    {
        public static string TabName { get; set; } = "КСП-ТИМ";
        public static string PanelName { get; set; } = "Посчитать";
        public static string PanelTransferring { get; set; } = "Посчитать";
        public static string ButtonName { get; set; } = "SumBtn";
        public static string ButtonText { get; set; } = "Сумма\nобъемов";
        public Result OnStartup(UIControlledApplication application)
        {
            #region Пример интересной реализации
            /*
            // метод который позволяет размстить кнопку на системной панели.. но метод на нее не привязать

            adWin.RibbonControl ribbon = adWin.ComponentManager.Ribbon;
            
            adWin.RibbonTab modifyTab = ribbon.Tabs.Where(xxx => xxx.Id == "Modify").FirstOrDefault();
            adWin.RibbonPanel geometryPanel = modifyTab.Panels.Where(xxx => xxx.Source.Id == "geometry_shr").FirstOrDefault();

            adWin.RibbonButton button = new adWin.RibbonButton();
            button.Name = "VSumButton";
            button.Image = new BitmapImage(new Uri(@"C:\Users\Sidorin\Source\Repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-16.png"));
            button.LargeImage = new BitmapImage(new Uri(@"C:\Users\Sidorin\Source\Repos\KSP-VolumesSum\KSP-VolumesSum\res\sum-32.png"));
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
            adWin.ComponentManager.UIElementActivated +=
                new EventHandler<adWin.UIElementActivatedEventArgs>(CommandForButton);

            adWin.RibbonControl ribbon = adWin.ComponentManager.Ribbon;
            foreach (adWin.RibbonTab tab in ribbon.Tabs)
            {
                foreach (adWin.RibbonPanel panel in tab.Panels)
                {
                    foreach (adWin.RibbonItem ribbonItem in panel.Source.Items)
                    {
                        str += tab.Id + " : " + panel.Source.Id + " : " + ribbonItem.Id + Environment.NewLine;
                    }
                    
                }
                str += tab.Id + Environment.NewLine;
            }
            TaskDialog.Show("123", str);

            //str += " -----> " + modifyTab.Id + geometryPanel.Source.Id;

            //TaskDialog.Show("123", str);

             */
            #endregion

            List<RibbonPanel> panels = application.GetRibbonPanels();
            string str = "";

            application.CreateRibbonTab(TabName);
            RibbonPanel panelVS = application.CreateRibbonPanel(TabName, PanelName);

            string path = Assembly.GetExecutingAssembly().Location;

            PushButtonData SumBtnData = new PushButtonData(ButtonName, ButtonText, path, "KSP_VolumesSum.VSum")
            {
                ToolTipImage = new BitmapImage(new Uri(Path.GetDirectoryName(path) + "\\res\\sum-32.png", UriKind.Absolute)),
                ToolTip = "Суммирует объемы элементов модели, если они есть"
            };
            PushButton SumBtn = panelVS.AddItem(SumBtnData) as PushButton;
            SumBtn.LargeImage = new BitmapImage(new Uri(Path.GetDirectoryName(path) + "\\res\\sum-32.png", UriKind.Absolute));

            PlaceButtonOnModifyRibbon();

            

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public void PlaceButtonOnModifyRibbon()
        {

            try
            {
                String SystemTabId = "Modify";
                String SystemPanelId = "modify_shr";

                adWin.RibbonControl adWinRibbon = adWin.ComponentManager.Ribbon;

                adWin.RibbonTab adWinSysTab = null;
                adWin.RibbonPanel adWinSysPanel = null;

                adWin.RibbonTab adWinApiTab = null;
                adWin.RibbonPanel adWinApiPanel = null;
                adWin.RibbonItem adWinApiItem = null;

                foreach (adWin.RibbonTab ribbonTab in adWinRibbon.Tabs)
                {
                    // Look for the specified system tab

                    if (ribbonTab.Id == SystemTabId)
                    {
                        adWinSysTab = ribbonTab;

                        foreach (adWin.RibbonPanel ribbonPanel in ribbonTab.Panels)
                        {
                            // Look for the specified panel 
                            // within the system tab

                            if (ribbonPanel.Source.Id == SystemPanelId)
                            {
                                adWinSysPanel = ribbonPanel;
                            }
                        }
                    }
                    else
                    {
                        // Look for our API tab

                        if (ribbonTab.Id == Main.TabName)
                        {
                            adWinApiTab = ribbonTab;

                            foreach (adWin.RibbonPanel ribbonPanel in ribbonTab.Panels)
                            {
                                if (ribbonPanel.Source.Id == "CustomCtrl_%" + Main.TabName + "%" + Main.PanelName)
                                {
                                    foreach (adWin.RibbonItem ribbonItem in ribbonPanel.Source.Items)
                                    {
                                        if (ribbonItem.Id == "CustomCtrl_%CustomCtrl_%" + Main.TabName + "%" + Main.PanelName + "%" + Main.ButtonName)
                                        {
                                            adWinApiItem = ribbonItem;
                                        }
                                    }
                                }

                                if (ribbonPanel.Source.Id == "CustomCtrl_%" + Main.TabName + "%" + Main.PanelName)
                                {
                                    adWinApiPanel = ribbonPanel;
                                }
                            }
                        }
                    }
                }


                if (adWinSysTab != null
                  && adWinSysPanel != null
                  && adWinApiTab != null
                  && adWinApiPanel != null
                   && adWinApiItem != null)
                {
                    adWinSysTab.Panels.Add(adWinApiPanel);
                    adWinApiTab.IsVisible = false;
                    //adWinApiPanel.Source.Items.Add(adWinApiItem);
                    //adWinApiTab.Panels.Remove(adWinApiPanel);
                }


            }

            #region catch and finally
            catch (Exception ex)
            {
                TaskDialog.Show("me", ex.Message + Environment.NewLine + ex.InnerException);
            }
            finally
            {
            }
            #endregion
        }

        #region Просто интересный метод
        //void CommandForButton(object sender, adWin.UIElementActivatedEventArgs e)
        //{
        //    if (e != null
        //      && e.Item != null
        //      && e.Item.Id != null
        //      && e.Item.Id == "ID_VSumButton")
        //    {
        //        /*
        //         Perform the button action

        //         Local file

        //         string path = Assembly.GetExecutingAssembly().Location;
        //         path = "KSP_VolumesSum.VSum";

        //         // не понятно как создать метод

        //        TaskDialog.Show("123", "Ghbdtn");



        //        path = Path.Combine(
        //          Path.GetDirectoryName(path),
        //          "test.html");

        //        // Internet URL

        //        path = "http://thebuildingcoder.typepad.com";

        //        Process.Start(path);
        //        */
        //    }
        //}
        #endregion
    }
}
