using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace BugTrackerWebControls
{
    /// <summary>
    /// Image template column for grid view.
    /// </summary>
    public class ImageTemplate : ITemplate
    {
        ListItemType templateType;
        string columnName;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="colname"></param>
        public ImageTemplate(ListItemType type, string templateColumnName)
        {
            templateType = type;
            columnName = templateColumnName;
        }

        /// <summary>
        /// Instantiate template column.
        /// </summary>
        /// <param name="container"></param>
        public void InstantiateIn(System.Web.UI.Control container)
        {
            Image img = new Image();
            img.ID = "Img_" + columnName;
            img.CssClass = "imgcursor";
            img.SkinID = "skin";

            BoundField field = new BoundField();
            field.DataField = "NrDosar";
            field.SortExpression = "NrDosar";

            switch (templateType)
            {
                case ListItemType.Item:
                    container.Controls.Add(img);
                    break;
            }
        }
    }
}
