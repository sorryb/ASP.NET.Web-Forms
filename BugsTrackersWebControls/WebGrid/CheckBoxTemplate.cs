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
    /// Create a template checkbox column.
    /// </summary>
    public class CheckBoxTemplate : ITemplate
    {
        private string chkName;
        private bool withOnclickEvent;
        private string _folderImageUrl;
        private bool _isWithOrderAfterCheckBoxes = true;

        #region constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folderImageUrl"></param>
        public CheckBoxTemplate(string name, string folderImageUrl)
        {
            chkName = name;
            _folderImageUrl = folderImageUrl;

        }
        /// <summary>
        /// Constructor overload
        /// </summary>
        /// <param name="name"></param>
        /// <param name="withOnClick"></param>
        /// <param name="folderImageUrl"></param>
        public CheckBoxTemplate(string name, bool withOnClick, string folderImageUrl)
        {
            chkName = name;
            withOnclickEvent = withOnClick;
            _folderImageUrl = folderImageUrl;
        }

        /// <summary>
        /// Constructor overload for checking if is with order after checkbox-es(see SQLJobLIst page.)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="withOnClick"></param>
        /// <param name="folderImageUrl"></param>
        /// <param name="withHeaderCheckImage"></param>
        public CheckBoxTemplate(string name, bool withOnClick, string folderImageUrl, bool withHeaderCheckImage)
        {
            chkName = name;
            withOnclickEvent = withOnClick;
            _folderImageUrl = folderImageUrl;
            _isWithOrderAfterCheckBoxes = withHeaderCheckImage;
        }
        /// <summary>
        /// General constructor.
        /// </summary>
        public CheckBoxTemplate()
        {

        }
        #endregion

        /// <summary>
        /// Set image url; some like ../../img/plus.gif
        /// </summary>
        public string FolderImageUrl
        {
            set
            {
                _folderImageUrl = value;
            }
            get
            {
                return _folderImageUrl;
            }
        }


        /// <summary>
        /// Instantiate controls.
        /// </summary>
        /// <param name="container"></param>
        public void InstantiateIn(Control container)
        {
            CreditRequestsCheckBox chk = new CreditRequestsCheckBox();
            ImageButton img = new ImageButton();

            img.ImageUrl = _folderImageUrl + "/bifa.gif";
            img.ToolTip = "Ordoneaza dupa articolele bifate(inclusiv in alte pagini!)";
            img.ID = "ImgCheck";
            img.Visible = _isWithOrderAfterCheckBoxes;

            chk.ID = chkName;
            chk.EnableViewState = true;

            if (withOnclickEvent)
            {
                chk.Attributes.Add("onclick", "CheckAll('chkItemChecked',this.checked)");
                img.Attributes.Add("onclick", "HiddenSortCheckedObj.value = 1;");
                //container.Controls.Add(img);
            }
            else
                chk.Attributes.Add("onclick", "CheckLine(this);");

            container.Controls.Add(chk);
        }




    }



}
