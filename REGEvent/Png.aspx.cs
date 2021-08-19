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
using DayPilot.Web.Ui.Enums.Calendar;
using DayPilot.Web.Ui.Events.Scheduler;
using BeforeEventRenderEventArgs = DayPilot.Web.Ui.Events.Calendar.BeforeEventRenderEventArgs;
using CommandEventArgs = DayPilot.Web.Ui.Events.CommandEventArgs;

public partial class _Png : System.Web.UI.Page 
{
    DayPilotCalendar DayPilotCalendar1 = new DayPilotCalendar();

    protected void Page_Load(object sender, EventArgs e)
    {
        DayPilotCalendar1.ViewType = ViewTypeEnum.Week;
        DayPilotCalendar1.StartDate = Convert.ToDateTime(Request.QueryString["start"]);

        SetDataSourceAndBind();
        ExportToPng();
    }


    private void SetDataSourceAndBind()
    {
        DayPilotCalendar1.DataSource = GetData(DayPilotCalendar1.StartDate, DayPilotCalendar1.EndDate);
        DayPilotCalendar1.DataStartField = "fecha_inicial";
        DayPilotCalendar1.DataEndField = "fecha_final";
        DayPilotCalendar1.DataIdField = "id_servicio_cliente";
        DayPilotCalendar1.DataTextField = "descripcion";
        DayPilotCalendar1.DataBind();

    }

    private DataTable GetData(DateTime start, DateTime end)
    {
        SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM [servicio_cliente] WHERE NOT (([fecha_inicial] <= @start) OR ([fecha_final] >= @end))", ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString);
        da.SelectCommand.Parameters.AddWithValue("start", start);
        da.SelectCommand.Parameters.AddWithValue("end", end);

        DataTable dt = new DataTable();
        da.Fill(dt);

        return dt;
    }



    private void ExportToPng()
    {
        SetDataSourceAndBind();
        SetExportProperties();

        int scroll = Convert.ToInt32(Request.QueryString["scroll"]);

        Response.Clear();
        Response.ContentType = "image/png";
        Response.AddHeader("content-disposition", "attachment;filename=print.png");
        MemoryStream img = DayPilotCalendar1.Export(ImageFormat.Png, scroll);
        img.WriteTo(Response.OutputStream);
        Response.End();
    }



    private void SetExportProperties()
    {
        //DayPilotCalendar1.Width = Unit.Percentage(100);
        //DayPilotCalendar1.HeightSpec = HeightSpecEnum.Full;

        // match the theme
        
        DayPilotCalendar1.HourNameBackColor = ColorTranslator.FromHtml("#eee");
        DayPilotCalendar1.BackColor = Color.White;
        DayPilotCalendar1.NonBusinessBackColor = Color.White;
        DayPilotCalendar1.BorderColor = ColorTranslator.FromHtml("#999");
        DayPilotCalendar1.HeaderFontColor = ColorTranslator.FromHtml("#666");
        DayPilotCalendar1.CellBorderColor = ColorTranslator.FromHtml("#eee");
        DayPilotCalendar1.EventFontColor = ColorTranslator.FromHtml("#666");
        DayPilotCalendar1.EventCorners = CornerShape.Rounded;
        DayPilotCalendar1.EventFontSize = "10pt";
        DayPilotCalendar1.EventBorderColor = ColorTranslator.FromHtml("#999");
        DayPilotCalendar1.EventBackColor = ColorTranslator.FromHtml("#fafafa");
        DayPilotCalendar1.HourBorderColor = ColorTranslator.FromHtml("#eee");
        DayPilotCalendar1.HourHalfBorderColor = ColorTranslator.FromHtml("#eee");
        DayPilotCalendar1.DurationBarVisible = false;

    }


}
