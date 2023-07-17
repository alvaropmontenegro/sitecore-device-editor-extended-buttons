using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Sitecore.Shell.Applications.Layouts.DeviceEditor;
using Sitecore.Web;
using Sitecore.Web.UI.HtmlControls;
using Sitecore.Web.UI.Sheer;

namespace Sitecore.DeviceEditorExtendedButtons.Buttons
{
    public class CopyTo : DeviceEditorForm
    {
        protected Button btnCopyTo { get; set; }

        [HandleMessage("device:copyTo", true)]
        protected void Execute(ClientPipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            if (SelectedIndex < 0)
            {
                SheerResponse.Alert("Please, select a rendering!");
                return;
            }

            if (args.IsPostBack)
            {
                if (args.HasResult)
                {
                    try
                    {
                        var layoutDefinition = GetLayoutDefinition();
                        var device = layoutDefinition.GetDevice(DeviceID);
                        var renderings = device?.Renderings;

                        if (renderings == null)
                            return;

                        var renderingDefinition = renderings[SelectedIndex] as RenderingDefinition;

                        if (renderingDefinition == null || string.IsNullOrEmpty(renderingDefinition.ItemID))
                            return;

                        var targetItemId = ID.Parse(args.Result);
                        var targetItem = Client.ContentDatabase.GetItem(targetItemId);

                        if (targetItem == null)
                        {
                            SheerResponse.Alert("Target item not found.");
                            return;
                        }

                        if (Sitecore.Context.Item.ID == targetItemId)
                        {
                            SheerResponse.Alert("The target item cannot be the same as the actual item!");
                            return;
                        }                        

                        var targetLayoutField = new LayoutField(targetItem.Fields[FieldIDs.LayoutField]);
                        if (targetLayoutField == null || targetLayoutField.InnerField == null)
                        {
                            SheerResponse.Alert("Target item does not have a valid layout field.");
                            return;
                        }

                        var targetLayoutDefinition = LayoutDefinition.Parse(targetLayoutField.Value);
                        var targetDevice = targetLayoutDefinition.GetDevice(DeviceID);
                        var targetRenderings = targetDevice?.Renderings;

                        targetRenderings?.Add(renderingDefinition);

                        using (new EditContext(targetItem))
                        {
                            targetLayoutField.Value = targetLayoutDefinition.ToXml();
                        }

                        SheerResponse.Alert("Successfully cloned the Rendering!");
                    }
                    catch (System.Exception ex)
                    {
                        Log.Error("An error occurred while copying the rendering.", ex, this);
                        SheerResponse.Alert("An error occurred while copying the rendering. Please try again or contact the administrator.");
                    }
                }
            }
            else
            {
                string url = Sitecore.UIUtil.GetUri("control:RenderingCopyTo");
                var options = new ModalDialogOptions(url)
                {
                    Response = true
                };

                SheerResponse.ShowModalDialog(options);
                args.WaitForPostBack();
            }
        }

        private static LayoutDefinition GetLayoutDefinition()
        {
            string sessionString = WebUtil.GetSessionString(GetSessionHandle());
            Assert.IsNotNull(sessionString, "Layout definition session string is null.");
            return LayoutDefinition.Parse(sessionString);
        }

        private static string GetSessionHandle()
        {
            return "SC_DEVICEEDITOR";
        }
    }
}
