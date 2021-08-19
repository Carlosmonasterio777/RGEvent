/* Copyright © 2014 Annpoint, s.r.o.
   Use of this software is subject to license terms. 
   http://www.daypilot.org/

   If you have purchased a DayPilot Pro license, you are allowed to use this 
   code under the conditions of DayPilot Pro License Agreement:

   http://www.daypilot.org/files/LicenseAgreement.pdf

   Otherwise, you are allowed to use it for evaluation purposes only under 
   the conditions of DayPilot Pro Trial License Agreement:
   
   http://www.daypilot.org/files/LicenseAgreementTrial.pdf
   
*/

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;
using DayPilot.Utils;
using DayPilot.Web.Ui;
using DayPilot.Web.Ui.Data;
using DayPilot.Web.Ui.Enums;
using DayPilot.Web.Ui.Events.Scheduler;
using BeforeEventRenderEventArgs = DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs;
using CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs;

public partial class _Print : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime start = Convert.ToDateTime(Request.QueryString["start"]);
        int scroll = Convert.ToInt32(Request.QueryString["scroll"]);
        ImageCalendar.ImageUrl = "Png.aspx?start=" + start.ToString("s") + "&scroll=" + scroll;

    }

}
