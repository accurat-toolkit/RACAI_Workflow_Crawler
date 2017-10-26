using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DiagramDesigner.Controls;
using Agora.Text.UI.Flow.Execution;
using System.Xml.Serialization;
using System.IO;

namespace DiagramDesigner {
    //These attributes identify the types of the named parts that are used for templating
    [TemplatePart(Name = "PART_DragThumb", Type = typeof(DragThumb))]
    [TemplatePart(Name = "PART_ResizeDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ConnectorDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public class DesignerItem : ContentControl, ISelectable, IGroupable {
        public void LoadSerializedDataBinding(string s) {
            s = s.Replace("\n", "\r\n");
            XmlSerializer ser = new XmlSerializer(typeof(ExecutionUnitDataBinding));
            MemoryStream ms=new MemoryStream();
            TextWriter tw=new StreamWriter(ms);
            tw.Write(s);
            tw.Flush();
            //tw.Close();
            ms.Position=0;
            TextReader tr=new StreamReader(ms);
            ExecutionUnitDataBinding tmp = (ExecutionUnitDataBinding)ser.Deserialize(tr);
            this.DataBinding = tmp;
            ms.Close();
            ms.Dispose();
        }

        public string GetSerializedDataBinding() {
            MemoryStream ms = new MemoryStream();
            XmlSerializer ser = new XmlSerializer(typeof(ExecutionUnitDataBinding));
            ser.Serialize(ms, DataBinding);
            //ser.Serialize(new FileStream("c:\\test.xml",FileMode.Create),DataBinding);
            ms.Position=0;
            TextReader tr=new StreamReader(ms);

            return tr.ReadToEnd();
        }
        public ExecutionUnitDataBinding DataBinding { get; set; }
        #region ID
        private Guid id;
        public Guid ID {
            get { return id; }
        }
        #endregion

        #region ParentID
        public Guid ParentID {
            get { return (Guid)GetValue(ParentIDProperty); }
            set { SetValue(ParentIDProperty, value); }
        }
        public static readonly DependencyProperty ParentIDProperty = DependencyProperty.Register("ParentID", typeof(Guid), typeof(DesignerItem));
        #endregion

        #region IsGroup
        public bool IsGroup {
            get { return (bool)GetValue(IsGroupProperty); }
            set { SetValue(IsGroupProperty, value); }
        }
        public static readonly DependencyProperty IsGroupProperty =
            DependencyProperty.Register("IsGroup", typeof(bool), typeof(DesignerItem));
        #endregion

        #region IsSelected Property

        public bool IsSelected {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty =
          DependencyProperty.Register("IsSelected",
                                       typeof(bool),
                                       typeof(DesignerItem),
                                       new FrameworkPropertyMetadata(false));

        #endregion

        #region DragThumbTemplate Property

        // can be used to replace the default template for the DragThumb
        public static readonly DependencyProperty DragThumbTemplateProperty =
            DependencyProperty.RegisterAttached("DragThumbTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetDragThumbTemplate(UIElement element) {
            return (ControlTemplate)element.GetValue(DragThumbTemplateProperty);
        }

        public static void SetDragThumbTemplate(UIElement element, ControlTemplate value) {
            element.SetValue(DragThumbTemplateProperty, value);
        }

        #endregion

        #region ConnectorDecoratorTemplate Property

        // can be used to replace the default template for the ConnectorDecorator
        public static readonly DependencyProperty ConnectorDecoratorTemplateProperty =
            DependencyProperty.RegisterAttached("ConnectorDecoratorTemplate", typeof(ControlTemplate), typeof(DesignerItem));

        public static ControlTemplate GetConnectorDecoratorTemplate(UIElement element) {
            return (ControlTemplate)element.GetValue(ConnectorDecoratorTemplateProperty);
        }

        public static void SetConnectorDecoratorTemplate(UIElement element, ControlTemplate value) {
            element.SetValue(ConnectorDecoratorTemplateProperty, value);
        }

        #endregion

        #region IsDragConnectionOver

        // while drag connection procedure is ongoing and the mouse moves over 
        // this item this value is true; if true the ConnectorDecorator is triggered
        // to be visible, see template
        public bool IsDragConnectionOver {
            get { return (bool)GetValue(IsDragConnectionOverProperty); }
            set { SetValue(IsDragConnectionOverProperty, value); }
        }
        public static readonly DependencyProperty IsDragConnectionOverProperty =
            DependencyProperty.Register("IsDragConnectionOver",
                                         typeof(bool),
                                         typeof(DesignerItem),
                                         new FrameworkPropertyMetadata(false));

        #endregion

        static DesignerItem() {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }

        public DesignerItem(Guid id) {
            this.id = id;
            this.Loaded += new RoutedEventHandler(DesignerItem_Loaded);
        }

        public DesignerItem()
            : this(Guid.NewGuid()) {
        }


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e) {
            base.OnPreviewMouseDown(e);
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

            // update selection
            if (designer != null) {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected) {
                        designer.SelectionService.RemoveFromSelection(this);
                    } else {
                        designer.SelectionService.AddToSelection(this);
                    } else if (!this.IsSelected) {
                    designer.SelectionService.SelectItem(this);
                }
                Focus();
            }

            e.Handled = false;
        }
        public Label Caption { get; set; }
        void DesignerItem_Loaded(object sender, RoutedEventArgs e) {
            if (base.Template != null) {
                //var tmp = (ControlTemplate)FindResource("DesignerItem");
                this.Caption = (Label)Template.FindName("lblCaption", this);
                Caption.Content = "N/A";
                if (this.DataBinding != null) {
                    this.DataBinding.SetMyLabel(this.Caption);
                    this.DataBinding.Name = this.DataBinding.Caption;
                    this.Caption.Content = this.DataBinding.Name;
                } else {
                    DataBinding = new ExecutionUnitDataBinding(Caption);
                }
                ContentPresenter contentPresenter =
                    this.Template.FindName("PART_ContentPresenter", this) as ContentPresenter;
                if (contentPresenter != null) {
                    UIElement contentVisual = VisualTreeHelper.GetChild(contentPresenter, 0) as UIElement;
                    if (contentVisual != null) {
                        DragThumb thumb = this.Template.FindName("PART_DragThumb", this) as DragThumb;
                        if (thumb != null) {
                            ControlTemplate template =
                                DesignerItem.GetDragThumbTemplate(contentVisual) as ControlTemplate;
                            if (template != null)
                                thumb.Template = template;
                        }
                    }
                }
            }
        }
    }
}
