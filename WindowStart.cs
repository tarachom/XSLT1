using Gtk;
using System.Xml.Xsl;

namespace XSLTTest
{
    class WindowStart : Window
    {
        TextView textXML;
        TextView textXSLT;
        TextView textTxt;

        public WindowStart() : base("XSLT")
        {
            SetDefaultSize(1600, 900);
            SetPosition(WindowPosition.Center);

            DeleteEvent += delegate { Program.Quit(); };

            VBox vbox = new VBox();
            Add(vbox);

            #region Кнопки

            //Кнопки
            HBox hBoxButton = new HBox();
            vbox.PackStart(hBoxButton, false, false, 10);

            Button bTransform = new Button("Transform");
            bTransform.Clicked += OnTransform;
            hBoxButton.PackStart(bTransform, false, false, 10);

            #endregion

            HPaned hPaned = new HPaned() { Position = 500 };

            vbox.PackStart(hPaned, true, true, 0);

            //XML
            HBox hboxXML = new HBox();

            ScrolledWindow scrollXML = new ScrolledWindow() { ShadowType = ShadowType.In };
            scrollXML.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            scrollXML.Add(textXML = new TextView());

            hboxXML.PackStart(scrollXML, true, true, 5);

            hPaned.Pack1(hboxXML, false, true);

            HPaned hPaned2 = new HPaned() { Orientation = Orientation.Vertical, Position = 400 };
            hPaned.Pack2(hPaned2, false, true);

            //XSLT
            HBox hBoxXSLT = new HBox();

            ScrolledWindow scrollXSLT = new ScrolledWindow();
            scrollXSLT.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            scrollXSLT.Add(textXSLT = new TextView());

            hBoxXSLT.PackStart(scrollXSLT, true, true, 5);

            hPaned2.Pack1(hBoxXSLT, false, true);

            //Txt
            HBox hBoxTxt = new HBox();

            ScrolledWindow scrollTxt = new ScrolledWindow();
            scrollTxt.SetPolicy(PolicyType.Automatic, PolicyType.Automatic);
            scrollTxt.Add(textTxt = new TextView());

            hBoxTxt.PackStart(scrollTxt, true, true, 5);

            hPaned2.Pack2(hBoxTxt, false, true);

            //statusBar
            Statusbar statusBar = new Statusbar();
            vbox.PackStart(statusBar, false, false, 0);

            ShowAll();
        }

        void OnTransform(object? sender, EventArgs args)
        {
            string patToDir = System.IO.Path.Combine(AppContext.BaseDirectory, "../../../");
            string pathToXML = System.IO.Path.Combine(patToDir, "Confa.xml");
            string pathToXSLT = System.IO.Path.Combine(patToDir, "Template.xslt");
            string pathToResult = System.IO.Path.Combine(patToDir, "Test.cs");

            textXML.Buffer.Text = File.ReadAllText(pathToXML);
            textXSLT.Buffer.Text = File.ReadAllText(pathToXSLT);
            textTxt.Buffer.Text = "text";

            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load(pathToXSLT);

            MemoryStream ms = new MemoryStream();

            transform.Transform(pathToXML, null, ms);

            textTxt.Buffer.Text = System.Text.Encoding.UTF8.GetString(ms.GetBuffer());
            File.WriteAllText(pathToResult, textTxt.Buffer.Text);

            ms.Close();
        }
    }
}
